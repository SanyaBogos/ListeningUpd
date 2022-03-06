using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Server.Security
{
    public class SecurityRulesSingleton
    {
        public SecurityRules Rules { get; private set; }
        private static SecurityRulesSingleton _instance;
        public static SecurityRulesSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SecurityRulesSingleton();
                return _instance;
            }
        }

        private SecurityRulesSingleton()
        {
            var security = "security.json";
            int index = 0;
            string path;
            string filePath;

            do
            {
                path = Directory.GetCurrentDirectory()
                                .Split(new string[] { "bin" },
                                StringSplitOptions.RemoveEmptyEntries).First();

                filePath = Path.Combine(path, $"{GetDots(index++)}{security}");
            } while (!File.Exists(filePath));

            using (var sr = File.OpenText(filePath))
            {
                var text = sr.ReadToEnd();
                Rules = JsonConvert.DeserializeObject<SecurityRules>(text);
            }

            Rules.RijndaelManaged = new RijndaelManaged();
            Rules.RijndaelManaged.GenerateKey();
            Rules.RijndaelManaged.GenerateIV();
        }

        private string GetDots(int index) {
            var sb = new StringBuilder();

            for (int i = 0; i < index; i++)
                sb.Append("../");

            return sb.ToString();
        }
    }
}
