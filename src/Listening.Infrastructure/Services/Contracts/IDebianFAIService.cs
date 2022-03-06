using Listening.Core.ViewModels.DebianFAI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IDebianFAIService
    {
        string GetPreseed(PreseedSettingsViewModel settings);
        //string GetDefaultSettings();
        PreseedSettingsViewModel GetDefaultSettings();
        string GetImage(PreseedSettingsViewModel settings);
        Task<byte[]> GetFileBytes (string distributiveResultName);
    }
}
