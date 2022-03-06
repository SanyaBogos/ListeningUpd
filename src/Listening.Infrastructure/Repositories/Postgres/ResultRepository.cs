using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Repositories.Abstract;
using Listening.Core.ViewModels.ListeningResult;

namespace Listening.Server.Repositories.Postgres
{
    public class ResultRepository : BasePostgresRepository<Result, long>, IResultRepository
    {
        private readonly string _delay;

        public ResultRepository(IConfiguration configuration) : base(configuration)
        {
            var number = configuration["Data:Result:Delay:Number"];
            var type = configuration["Data:Result:Delay:Type"];
            _delay = $@"{number} {type}";
        }

        public async Task<Result> GetNonCompletedResult(Result templateResult)
        {
            var query = $@"select * from public.""{TableName}"" 
                        where ""{nameof(Result.UserId)}""=@{nameof(Result.UserId)}
                            and ""{nameof(Result.TextId)}""=@{nameof(Result.TextId)} 
                            and ""{nameof(Result.Mode)}""=@{nameof(Result.Mode)} 
                            and ""{nameof(Result.Finished)}"" is null";

            using (var connection = Connection)
            {
                var result = await connection.QueryAsync<Result>(query,
                    new { templateResult.UserId, templateResult.TextId, templateResult.Mode });
                return result.FirstOrDefault();
            }
        }

        public async Task UpdateTimeSpentForNonCompletedResult(ResultUpdateTimeDto result)
        {
            var query = $@"update public.""{TableName}"" 
                            set ""{nameof(Result.TimeSpentMiliSeconds)}"" = ""{nameof(Result.TimeSpentMiliSeconds)}"" + @{nameof(Result.TimeSpentMiliSeconds)}  
                        where ""{nameof(Result.UserId)}""=@{nameof(Result.UserId)}
                            and ""{nameof(Result.TextId)}""=@{nameof(Result.TextId)}
                            and ""{nameof(Result.Mode)}""=@{nameof(Result.Mode)}
                            and ""{nameof(Result.Finished)}"" is null or (""{nameof(Result.Finished)}"" + interval '{_delay}') > now()
                            and ""{nameof(Result.TimeSpentMiliSeconds)}"" is not null
                            ";

            using (var connection = Connection)
            {
                await connection.QueryAsync(query,
                    new { result.UserId, result.TextId, result.Mode, TimeSpentMiliSeconds = result.TimeSpent });
            }
        }

        public async Task<IEnumerable<Result>> GetResults(long user)
        {
            var query = $@"SELECT ""{nameof(Result.Id)}"",
                                ""{nameof(Result.Finished)}"", ""{nameof(Result.IsCompleted)}"", 
                                ""{nameof(Result.IsStarted)}"", ""{nameof(Result.Mode)}"", 
                                ""{nameof(Result.ResultsEncodedString)}"", ""{nameof(Result.Started)}"", 
                                ""{nameof(Result.TimeSpentMiliSeconds)}"", ""{nameof(Result.TextId)}""
                                FROM public.""{TableName}""
                                where ""{nameof(Result.UserId)}"" = @userId
                                        and ""{nameof(Result.Finished)}"" is not null
                                ORDER BY ""{nameof(Result.Id)}"" DESC LIMIT 100";

            using (var connection = Connection)
            {
                var result = await connection.QueryAsync<Result>(query, new { userId = user });
                return result;
            }
        }

        // should be checked (it hasn't been tested yet)
        public async Task<bool> IsReferenceExists(string[] ids)
        {
            var query = $@"
IF EXISTS (select Id from public.""{TableName}"" where ""{nameof(Result.TextId)}"" IN @ids)
THEN
    RETURN QUERY
        SELECT 1;
else 
    RETURN QUERY
        SELECT 0;
";
            using (var connection = Connection)
            {
                var result = await connection.QuerySingleAsync<bool>(query, new { ids });
                return result;
            }
        }
    }
}