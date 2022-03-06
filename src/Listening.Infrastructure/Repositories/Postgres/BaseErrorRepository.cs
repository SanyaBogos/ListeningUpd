using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Listening.Server.Entities.Specialized.Result;
using Microsoft.Extensions.Configuration;

namespace Listening.Server.Repositories.Postgres
{
    public class BaseErrorRepository<T, Y> : BasePostgresRepository<T, Y>
    {
        protected const string ResultId = nameof(ErrorForJoined.ResultId);

        public BaseErrorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<ErrorCount>> ErrorsCount(long[] resultIds)
        {
            var query = $@"select ""{ResultId}"", count(""{ID}"") as ""{nameof(ErrorCount.Count)}""
                                from public.""{TableName}""                                
                                where ""{ResultId}"" in ({string.Join(',', resultIds)})
                                group by ""{ResultId}""";

            using (var connection = Connection)
            {
                var errorsCount = await connection.QueryAsync<ErrorCount>(query);
                return errorsCount;
            }
        }
    }
}
