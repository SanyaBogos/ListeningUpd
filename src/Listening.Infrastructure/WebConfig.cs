using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Listening.Infrastructure
{
    public class WebConfig
    {
        private const string Bin = "bin";
        private static readonly object _lock = new object();
        private static WebConfig _instance;

        public IConfiguration Configuration { get; private set; }
        public string CurrentDirectory { get; set; }

        public static WebConfig Instance
        {
            get
            {
                if (_instance == null)
                    lock (_lock)
                        if (_instance == null)
                            return _instance = new WebConfig();

                return _instance;
            }
        }

        private WebConfig()
        {
            CurrentDirectory = Path.Combine(Directory.GetCurrentDirectory()
                .Split(new string[] { Bin }, StringSplitOptions.RemoveEmptyEntries).First(),
                "../../src/Listening.Web");
            var appSettingsPath = Path.Combine(CurrentDirectory, "appsettings.json");
            var builder = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }
    }
}
