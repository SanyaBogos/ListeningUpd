using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Core.Entities.Specialized;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class UniqueAppIdGeneratorRepository
    {
        private readonly string _connectionString;
        //private readonly IConfiguration _configuration;

        public UniqueAppIdGeneratorRepository(IConfiguration configuration)
        {
            _connectionString = configuration["Data:SqlPostegresConnectionString"];
            //_configuration = configuration;
        }

        public void BuildAppId()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var tran = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
                {
                    var query = $@"
                    UPDATE public.""System_UniqueAppIdGenerator""
                        SET ""{nameof(UniqueAppIdGenerator.CurrentId)}"" = ""{nameof(UniqueAppIdGenerator.CurrentId)}"" + 1;

                    select ""{nameof(UniqueAppIdGenerator.CurrentId)}"" 
                        from public.""System_UniqueAppIdGenerator""; ";

                    var result = connection.QuerySingle<int>(query);
                    tran.Commit();

                    AppSettings.AppId = result;
                }
            }
        }
    }
}
