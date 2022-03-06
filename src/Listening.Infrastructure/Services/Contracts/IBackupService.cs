using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IBackupService
    {
        string GetBackup();
        void Restore(string path);
    }
}
