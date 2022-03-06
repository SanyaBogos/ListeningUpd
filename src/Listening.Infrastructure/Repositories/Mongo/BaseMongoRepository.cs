using Listening.Server.Entities.Specialized;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Listening.Server.Repositories.Mongo
{
    public class BaseMongoRepository<T> where T : MongoBaseEntity
    {
        protected const string ID = "_id";
        private const string ANY_SYMBOLS = @"[\s\S]*";
        private readonly char[] _specialCharacters =
            { '!','@','#','$','%','^','&','*','(',')','[',']' };
        private readonly string[] _unmodifiebleFields =
            { nameof(LogInfo.CreatedBy), nameof(LogInfo.CreatedDate) };

        protected IMongoClient _client;
        protected IMongoDatabase _dataBase;
        protected IMongoCollection<T> _collection;

        public BaseMongoRepository(IConfiguration configuration)
        {
            var collectionName = configuration["Data:MongoDB:CollectionName"];
            _client = new MongoClient(configuration["Data:MongoDB:Url"]);
            _dataBase = _client.GetDatabase(configuration["Data:MongoDB:DataBaseName"]);
            _collection = _dataBase.GetCollection<T>(collectionName);
        }

        public virtual async Task Delete(IEnumerable<ObjectId> ids)
        {
            var filter = Builders<T>.Filter.In(ID, ids);
            await _collection.DeleteManyAsync(filter);
        }

        public virtual IQueryable<T> Get()
        {
            return _collection.AsQueryable();
        }

        public virtual async Task<PagedData<T>> GetPaged(QueryViewModel query)
        {
            var find = Filter(query);

            if (!string.IsNullOrEmpty(query.SortingName))
                find = query.IsAscending
                    ? find.Sort(Builders<T>.Sort.Ascending(query.SortingName))
                    : find.Sort(Builders<T>.Sort.Descending(query.SortingName));

            var totalTask = find.CountDocumentsAsync();
            var itemsTask = find.Skip((query.Page - 1) * query.ElementsPerPage)
                .Limit(query.ElementsPerPage).ToListAsync();

            await Task.WhenAll(totalTask, itemsTask);

            return new PagedData<T>
            {
                Count = totalTask.Result,
                Data = itemsTask.Result.ToArray()
            };
        }

        public virtual async Task<T> GetById(ObjectId id)
        {
            var filter = Builders<T>.Filter.Eq(ID, id);
            var texts = await (await _collection.FindAsync(filter)).ToListAsync();
            return texts.FirstOrDefault();
        }

        public virtual async Task Insert(IEnumerable<T> items)
        {
            if (items != null && items.Count() > 0)
                await _collection.InsertManyAsync(items);
        }

        public virtual async Task Update(IEnumerable<T> items)
        {
            if (items == null || items.Count() == 0)
                return;

            var update = Builders<T>.Update;
            UpdateDefinition<T> updateDefinition = null;

            var props = typeof(T).GetProperties()
                .Where(x => !_unmodifiebleFields.Contains(x.Name));

            foreach (var item in items)
            {
                var filter = Builders<T>.Filter.Eq(ID, item.Id);

                foreach (var prop in props)
                    if (updateDefinition == null)
                        updateDefinition = update.Set(prop.Name, prop.GetValue(item));
                    else
                        updateDefinition = updateDefinition.Set(prop.Name, prop.GetValue(item));

                await _collection.UpdateOneAsync(filter, updateDefinition);
            }
        }

        public virtual async Task UpdateBackup(IEnumerable<T> items)
        {
            if (items == null || items.Count() == 0)
                return;

            var update = Builders<T>.Update;
            UpdateDefinition<T> updateDefinition = null;

            var props = typeof(T).GetProperties();

            foreach (var item in items)
            {
                var filter = Builders<T>.Filter.Eq(ID, item.Id);

                foreach (var prop in props)
                    if (updateDefinition == null)
                        updateDefinition = update.Set(prop.Name, prop.GetValue(item));
                    else
                        updateDefinition = updateDefinition.Set(prop.Name, prop.GetValue(item));

                await _collection.UpdateOneAsync(filter, updateDefinition);
            }
        }

        protected virtual FilterDefinition<T> SpecificFilter(QueryViewModel query, FilterDefinitionBuilder<T> builder, FilterDefinition<T> filter)
        {
            return filter;
        }

        private IFindFluent<T, T> Filter(QueryViewModel query, bool isDeleted = false)
        {
            var builder = Builders<T>.Filter;
            var filter = builder.Empty;

            filter = SpecificFilter(query, builder, filter);

            if (query.FilteringProperties != null && query.FilteringProperties.Count != 0)
            {
                foreach (var key in query.FilteringProperties.Keys)
                    filter &= builder.Regex(key, BsonRegularExpression.Create(
                        new Regex(GetStringContainsWordsPattern(query.FilteringProperties[key].Trim()),
                            RegexOptions.IgnoreCase)));
            }

            filter &= builder.Eq(x => x.IsDeleted, isDeleted);

            return _collection.Find(filter);
        }

        private string GetStringContainsWordsPattern(string searchedWord)
        {
            if (searchedWord.Any(_specialCharacters.Contains))
            {
                var strings = searchedWord.Select(
                        c => _specialCharacters.Contains(c) ? $@"\{c}" : c.ToString())
                    .ToArray();

                searchedWord = string.Join("", strings);
            }

            if (!searchedWord.Contains(' '))
                return searchedWord;

            var innerPattern = string.Join(ANY_SYMBOLS,
                        searchedWord.Split(new string[] { " " },
                            StringSplitOptions.RemoveEmptyEntries));

            return $"{ANY_SYMBOLS}{innerPattern}{ANY_SYMBOLS}";
        }
    }
}
