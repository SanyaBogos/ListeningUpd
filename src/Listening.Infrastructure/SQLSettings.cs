using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listening.Infrastructure
{
    public class SQLSettings
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }

        public SQLSettings(string connectionString)
        {
            var parts = connectionString.Split(';');

            UserId = GetValue(parts, nameof(UserId));
            Password = GetValue(parts, nameof(Password));
            Host = GetValue(parts, nameof(Host));
            Port = GetValue(parts, nameof(Port));
            Database = GetValue(parts, nameof(Database));
        }

        private string GetValue(string[] parts, string name)
        {
            var part = parts.First(x => x.Replace(" ", "").Contains(name, StringComparison.OrdinalIgnoreCase));
            var result = part.Split('=').Last();
            return result;
        }
    }
}
