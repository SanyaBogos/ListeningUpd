using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Listening.Infrastructure.Services
{
    public static class DebianCleanupService
    {
        public static string TempIsoPath { get; set; }

        public static void Cleanup()
        {
            if (Directory.Exists(TempIsoPath))
                Directory.Delete(TempIsoPath, true);
        }
    }
}
