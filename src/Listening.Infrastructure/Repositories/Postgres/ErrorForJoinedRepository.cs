using Dapper;
using Listening.Server.Entities.Specialized.Result;
using Listening.Infrastructure.Repositories.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Repositories.Postgres
{
    public class ErrorForJoinedRepository : BaseErrorRepository<ErrorForJoined, long>,
                                                IErrorsForJoinedRepository
    {
        private const string ErrorValue = nameof(ErrorForJoined.ErrorValue);

        public ErrorForJoinedRepository(IConfiguration configuration)
            : base(configuration)
        {
            TableName = "Listening_ErrorsForJoined";
        }

        public async Task<string[]> GetErrors(long resultId)
        {
            var query = $@"select ""{ErrorValue}"" from public.""{TableName}"" 
                                where ""{ResultId}""=@{ResultId}";

            using (var connection = Connection)
            {
                var errors = await connection.QueryAsync<string>(
                    query, new { ResultId = resultId });
                return errors.ToArray();
            }
        }
    }
}
