using Dapper;
using Listening.Core.Entities.Custom;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.Feedback;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Server.Repositories.Postgres;
using Listening.Core.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class FeedbackRepository : BasePostgresRepository<Feedback, long>, IFeedbackRepository
    {
        private const string UserTableName = "AspNetUsers";

        public FeedbackRepository(IConfiguration configuration)
            : base(configuration) { }


        public async Task<PagedData<FeedbackDto>> GetPaged(FeedbackQueryViewModel query, long userId, bool isSuper)
        {
            var orderString = query.IsAscending ? "" : "desc";
            var whereConstraint = "";

            if (query.FilteringProperties != null && query.FilteringProperties.Count > 0)
            {
                var joinedProperties = query.FilteringProperties.Select(x => $@"""{x.Key}"" like '%{x.Value}%'");
                whereConstraint = $@"WHERE {string.Join(',', joinedProperties)}";
            }

            if (!isSuper)
            {
                var internalContraint = $@" ""{nameof(Feedback.IsVisible)}"" = true or U.""{nameof(ApplicationUser.Id)}"" = {userId}";
                whereConstraint = string.IsNullOrEmpty(whereConstraint) ? $@"WHERE {internalContraint}" : $@"and ({internalContraint})";
            }

            var tables = $@" ""{TableName}"" F join ""{UserTableName}"" U
                              on F.""{nameof(Feedback.UserId)}"" = U.""{nameof(ApplicationUser.Id)}""";

            var queryStr = $@"SELECT F.""{nameof(Feedback.Id)}"", ""{nameof(Feedback.Topic)}"", 
                                ""{nameof(Feedback.Details)}"", ""{nameof(Feedback.CreatedTime)}"", 
                                ""{nameof(Feedback.IsVisible)}"", ""{nameof(ApplicationUser.Email)}""
                              FROM {tables}
                                    {whereConstraint}
                                    ORDER BY ""{nameof(Feedback.CreatedTime)}"" {orderString}
                                   OFFSET {(query.Page - 1) * query.ElementsPerPage} 
                                   LIMIT {query.ElementsPerPage};

                            SELECT count (F.""{nameof(Feedback.Id)}"") FROM {tables} {whereConstraint}";

            using (var connection = Connection)
            using (var results = await connection.QueryMultipleAsync(queryStr))
            {
                var feedbacks = results.Read<FeedbackDto>().ToArray();
                var totalCount = results.ReadSingle<long>();

                return new PagedData<FeedbackDto> { Count = totalCount, Data = feedbacks };
            }
        }
    }
}
