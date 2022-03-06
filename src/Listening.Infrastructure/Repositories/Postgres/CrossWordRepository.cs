using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Listening.Server.Repositories.Abstract;
using Listening.Core.Entities.Specialized.Crossword;

namespace Listening.Server.Repositories.Postgres
{
    public class CrossWordRepository : BasePostgresRepository<Crossword, long>, ICrossWordRepository
    {
        public CrossWordRepository(IConfiguration configuration) : base(configuration) { }


        // public async Task<IEnumerable<Result>> GetCrossword(long id)
        // {
        //     var query = $@"SELECT ""{nameof(Result.Id)}"",
        //                         ""{nameof(Result.Finished)}"", ""{nameof(Result.IsCompleted)}"", 
        //                         ""{nameof(Result.IsStarted)}"", ""{nameof(Result.Mode)}"", 
        //                         ""{nameof(Result.ResultsEncodedString)}"", ""{nameof(Result.Started)}"", 
        //                             ""{nameof(Result.TextId)}""
        //                         FROM public.""{TableName}""
        //                         where ""{nameof(Result.UserId)}"" = @userId
        //                                 and ""{nameof(Result.Finished)}"" is not null
        //                         ORDER BY ""{nameof(Result.Id)}"" DESC LIMIT 100";

        //     using (var connection = Connection)
        //     {
        //         var result = await connection.QueryAsync<Result>(query, new { userId = user });
        //         return result;
        //     }
        // }

    }
}