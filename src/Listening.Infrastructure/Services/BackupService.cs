using Listening.Infrastructure.Extensions;
using Listening.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Listening.Infrastructure.Services
{
    public class BackupService : IBackupService
    {
        private readonly string _deleteFileName;
        private readonly string _createFileName;
        private readonly string _tempPath;
        private readonly string _backupPath;
        private readonly SQLSettings _sqlSettings;

        public BackupService(
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _deleteFileName = "delete_db.sql";
            _createFileName = "create_db.sql";
            _tempPath = $"{Directory.GetCurrentDirectory()}/bin{configuration["Data:FileStorage:Temp"]}";
            _backupPath = $"{env.WebRootPath}{configuration["Data:FileStorage:PostgresBackup"]}";
            _sqlSettings = new SQLSettings(configuration["Data:SqlPostegresConnectionString"]);
            Init();
        }

        public string GetBackup()
        {
            var time = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            var fullFilePath = $"{_backupPath}backup-{time}.sql";

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // TODO: implement correctly
                var command1 = $"set PGPASSWORD={_sqlSettings.Password}";
                var command2 = $@"pg_dump -U postgres -h {_sqlSettings.Host} -p {_sqlSettings.Port} -f ""{fullFilePath}"" -d ""{_sqlSettings.Database}"" ";
                var both = $"{command1} & {command2}";
                both.CommandPrompt();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var command = $@"export PGPASSWORD='{_sqlSettings.Password}'; su postgres -c 'pg_dump -U postgres -h {_sqlSettings.Host} -p {_sqlSettings.Port} -f ""{fullFilePath}"" -d ""{_sqlSettings.Database}""'";
                command.Bash();
            }

            return fullFilePath;
        }

        public void Restore(string path)
        {
            var text = File.ReadAllText(path);
            var dropDB = $@"drop database ""{_sqlSettings.Database}"";\n";
            var createDB = $@"create database ""{_sqlSettings.Database}"";\n";
            var result = $"{dropDB}{createDB}{text}";
            File.WriteAllText(path, result);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // TODO: implement correctly
                var command1 = $"set PGPASSWORD={_sqlSettings.Password}";
                var command2 = $@"psql -U postgres -h {_sqlSettings.Host} -p {_sqlSettings.Port} -d ""{_sqlSettings.Database}"" -f {path}";
                var commands = new string[] { command1, command2 };
                commands.CommandPrompt();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                var removeDBCmd = $@"export PGPASSWORD='{_sqlSettings.Password}'; su postgres -c 'psql -U postgres -h {_sqlSettings.Host} -p {_sqlSettings.Port} -d postgres -f {_tempPath}{_deleteFileName} '";
                var createDBCmd = $@"export PGPASSWORD='{_sqlSettings.Password}'; su postgres -c 'psql -U postgres -h {_sqlSettings.Host} -p {_sqlSettings.Port} -d postgres -f {_tempPath}{_createFileName} '";
                var restore = $@"export PGPASSWORD='{_sqlSettings.Password}'; su postgres -c 'psql -U postgres -h {_sqlSettings.Host} -p {_sqlSettings.Port} -d ""{_sqlSettings.Database}"" -f {path} '";
                var commands = new string[] { removeDBCmd, createDBCmd, restore };

                commands.Bash();
            }
        }

        private void Init()
        {
            if (!Directory.Exists(_backupPath))
                Directory.CreateDirectory(_backupPath);

            CreateSQLQueries();

            // set rules for access if linux platform
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string allowBackupFolderCmd = $"chmod a+x {_backupPath}";
                string allowDeleteSQLFile = $"chmod a+x {_tempPath}{_deleteFileName}";
                string allowCreateSQLFile = $"chmod a+x {_tempPath}{_createFileName}";
                var commands = new string[] { allowBackupFolderCmd, allowDeleteSQLFile, allowCreateSQLFile };

                commands.Bash();
            }
        }

        private void CreateSQLQueries()
        {
            GenerateDeleteDatabase();
            GenerateCreateDatabase();
        }

        private void GenerateDeleteDatabase()
        {
            var deletePath = $"{_tempPath}{_deleteFileName}";

            if (!File.Exists(deletePath))
            {
                using (StreamWriter w = File.AppendText(deletePath))
                    w.WriteLine($@"
-- Making sure the database exists
--SELECT * from pg_database where datname = '{_sqlSettings.Database}';

-- Disallow new connections
UPDATE pg_database SET datallowconn = 'false' WHERE datname = '{_sqlSettings.Database}';
ALTER DATABASE ""{_sqlSettings.Database}"" CONNECTION LIMIT 1;

-- Terminate existing connections
SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{_sqlSettings.Database}';

-- Drop database
DROP DATABASE ""{_sqlSettings.Database}"";
                    ");
            }
        }

        private void GenerateCreateDatabase()
        {
            var createPath = $"{_tempPath}{_createFileName}";

            if (!File.Exists(createPath))
            {
                using (StreamWriter w = File.AppendText(createPath))
                    w.WriteLine($@" create database ""{_sqlSettings.Database}""; ");
            }
        }
    }
}
