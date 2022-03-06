using Listening.Core.ViewModels.Steg;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Services.Contracts
{
    public interface IStegPictureService
    {
        string SimpleInjectMessage(StegSettingsDto settings);
        string SimpleEjectMessage(StegSettingsDto settings);
    }
}
