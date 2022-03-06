using Listening.Core.Entities.Custom;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class ChatCleanRepository
    {
        public async Task CleanupSignalRIds()
        {
            var appId = AppSettings.AppId;
            var query = $@"UPDATE public.""AspNetUsers""
                            SET ""{nameof(ApplicationUser.SignalRId)}"" = null, 
                                ""{nameof(ApplicationUser.AppId)}"" = null
                            WHERE ""{nameof(ApplicationUser.SignalRId)}"" is not null and 
                                (""{nameof(ApplicationUser.AppId)}"" is null 
                                    or ""{nameof(ApplicationUser.AppId)}"" = {appId}); ";

            var connectionString = WebConfig.Instance.Configuration["Data:SqlPostegresConnectionString"];

            using (var connection = new NpgsqlConnection(connectionString))
                await connection.ExecuteAsync(query);
        }
    }
}
