using Listening.Server.Entities.Specialized.Text;
using Listening.Infrastructure.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listening.Core.ViewModels.Text;
using System.Linq.Expressions;
using System.Data;
using Listening.Core.Entities.Custom;
using Listening.Core.ViewModels;

namespace Listening.Server.Repositories.Mongo
{
    public class TextsMongoRepository : BaseMongoRepository<Text>, ITextsMongoRepository
    {
        public TextsMongoRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task<List<Text>> GetAndCheckUpdatePossiblity(TextDto[] textDtos, ApplicationUser user, bool isAdmin)
        {
            var defaultDate = GlobalConstats.DefaultDate;
            var ids = textDtos.Select(y => ObjectId.Parse(y.Id)).ToArray();
            var texts = await _collection.Find(x => ids.Contains(x.Id)).ToListAsync();

            for (int i = 0; i < texts.Count; i++)
                if (texts[i].LastModifiedDate.HasValue)
                    texts[i].LastModifiedDate = texts[i].LastModifiedDate.Value.ToLocalTime();

            CheckAssignment(textDtos, user, isAdmin, texts);
            CheckModifiedDate(textDtos, defaultDate, texts);

            return texts;
        }

        public async Task<List<Text>> Get(string[] ids)
        {
            var idsConverted = ids.Select(y => ObjectId.Parse(y)).ToArray();
            var texts = await _collection.Find(x => idsConverted.Contains(x.Id)).ToListAsync();
            return texts;
        }

        public virtual async Task MarkAsDeleted(IEnumerable<ObjectId> ids)
        {
            var filter = Builders<Text>.Filter.In(ID, ids);
            var options = new UpdateOptions { IsUpsert = true };
            var builder = Builders<Text>.Update.Set(x => x.IsDeleted, true);

            await _collection.UpdateManyAsync(filter, builder, options);
        }

        protected override FilterDefinition<Text> SpecificFilter(QueryViewModel query, FilterDefinitionBuilder<Text> builder, FilterDefinition<Text> filter)
        {
            var complexity = nameof(TextDescriptionEnhancedDto.Complexity);
            var assignee = nameof(TextDescriptionEnhancedDto.Assignee);

            if (query.FilteringProperties.ContainsKey(complexity))
            {
                var val = query.FilteringProperties[complexity];

                if (!string.IsNullOrEmpty(val))
                {
                    var complexityValue = Convert.ToInt32(val);
                    filter &= builder.Eq(x => x.Complexity, complexityValue);
                }

                query.FilteringProperties.Remove(complexity);
            }

            if (query.FilteringProperties.ContainsKey(assignee))
            {
                var val = query.FilteringProperties[assignee];

                if (!string.IsNullOrEmpty(val))
                {
                    var assigneeValue = Convert.ToInt32(val);
                    filter &= builder.Eq(x => x.Assignee, assigneeValue);
                }

                query.FilteringProperties.Remove(assignee);
            }

            var createdName = GlobalConstats.CREATED_NAME;
            var updatedName = GlobalConstats.UPDATED_NAME;
            var dateKey = query.FilteringProperties.Keys
                .FirstOrDefault(x => x.Contains(createdName) || x.Contains(updatedName));

            if (dateKey != null)
            {
                var val = query.FilteringProperties[dateKey];

                if (!string.IsNullOrEmpty(val))
                {
                    var dateValue = Convert.ToDateTime(val);

                    switch (dateKey.Split(' ').Last())
                    {
                        case "<":
                            filter &= dateKey.Contains(createdName)
                                ? builder.Lt(x => x.CreatedDate, dateValue)
                                : builder.Lt(x => x.LastModifiedDate, dateValue);
                            break;
                        case ">":
                            filter &= dateKey.Contains(createdName)
                                ? builder.Gt(x => x.CreatedDate, dateValue)
                                : builder.Gt(x => x.LastModifiedDate, dateValue);
                            break;
                        case "=":
                            filter &= dateKey.Contains(createdName)
                                ? builder.Lt(x => x.CreatedDate, dateValue.AddDays(1)) & builder.Gt(x => x.CreatedDate, dateValue.AddDays(-1))
                                : builder.Lt(x => x.LastModifiedDate, dateValue.AddDays(1)) & builder.Gt(x => x.LastModifiedDate, dateValue.AddDays(-1));
                            break;
                        default:
                            break;
                    }
                }

                query.FilteringProperties.Remove(dateKey);
            }

            return base.SpecificFilter(query, builder, filter);
        }

        private void CheckModifiedDate(TextDto[] textDtos, DateTime defaultDate, List<Text> texts)
        {
            var coincidence = 0;

            foreach (var text in texts)
            {
                var textDtoUpdated = textDtos.First(x => x.Id == text.Id.ToString()).LastModifiedDate;
                var isBothNull = textDtoUpdated == null && text.LastModifiedDate == null;

                if (isBothNull || !text.LastModifiedDate.HasValue
                    || (text.LastModifiedDate.HasValue && textDtoUpdated.HasValue && (text.LastModifiedDate.Value - defaultDate).TotalMilliseconds - (textDtoUpdated.Value - defaultDate).TotalMilliseconds < 1000))
                    coincidence++;
            }

            if (coincidence != textDtos.Length)
                throw new DataException(GlobalConstats.TEXTS_MODIFIED);
        }

        private void CheckAssignment(TextDto[] textDtos, ApplicationUser user, bool isAdmin, List<Text> texts)
        {
            //var isAdmin = userRoles.Contains("Admin");
            var isAllAssigned = texts.All(x => x.Assignee != 0);
            var isLackOfAssignment = texts.Any(x => x.Assignee != user.Id);

            if (!isAdmin && isAllAssigned && isLackOfAssignment)
            {
                var textIds = string.Join(',', textDtos.Select(x => x.Id));
                throw new Exception(
                    $"Access denied. User with id {user.Id} tries to modify at least one text (from list of id: {textIds}) which doesn't below him (her)");
            }
        }
    }
}
