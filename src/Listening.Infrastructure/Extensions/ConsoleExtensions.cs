using System.Collections.Generic;
using System.Diagnostics;

namespace Listening.Infrastructure.Extensions
{
    public static class ConsoleExtensions
    {
        public static string Bash(this string cmd , string workingDirectory = null)
        {
            return Bsh(cmd, workingDirectory);
        }

        public static List<string> Bash(this string[] cmds /*, string workingDirectory*/)
        {
            var results = new List<string>();

            foreach (var cmd in cmds)
            {
                var result = Bsh(cmd);
                results.Add(result);
            }

            return results;
        }

        public static string CommandPrompt(this string cmd /*, string workingDirectory*/)
        {
            return CmdPrmpt(cmd);
        }

        public static List<string> CommandPrompt(this string[] cmds /*, string workingDirectory*/)
        {
            var results = new List<string>();

            foreach (var cmd in cmds)
            {
                var result = CmdPrmpt(cmd);
                results.Add(result);
            }

            return results;
        }

        private static string CmdPrmpt(string cmd)
        {
            var process = new Process();
            var variables = new System.Collections.Specialized.StringDictionary();

            var startInfo = new ProcessStartInfo
            {
                //EnvironmentVariables = variables,
                
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                FileName = "cmd.exe",
                Arguments = $"/C {cmd.Replace("/", "\\")}"
            };
            process.StartInfo = startInfo;
            process.Start();

            process.WaitForExit();
            string output = process.StandardOutput.ReadToEnd();

            process.Dispose();
            return output;
        }

        private static string Bsh(string cmd, string workingDirectory = null)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };

            if (!string.IsNullOrEmpty(workingDirectory))
                process.StartInfo.WorkingDirectory = workingDirectory;

            process.Start();
            process.WaitForExit();
            string result = process.StandardOutput.ReadToEnd();
            process.Dispose();

            return result;
        }
    }
}
