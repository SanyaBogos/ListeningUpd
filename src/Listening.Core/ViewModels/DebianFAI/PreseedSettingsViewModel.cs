using Listening.Core.ViewModels.AccountViewModels;
using Listening.Core.ViewModels.ConstraintsInterface.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Listening.Core.ViewModels.DebianFAI
{
    public class PreseedSettingsViewModel : BaseCaptchaViewModel, IEquatable<PreseedSettingsViewModel>
    {
        public string Mirror { get; set; }
        public string RootPassword { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserPassword { get; set; }
        public string AdditionalSoft { get; set; }

        public ImageConfig ImageConfig { get; set; }


        public ArchitectureType[] CurrentArchitectures { get; set; }

        public bool ForceDownload { get; set; }
        public HddSplitSettingsViewModel HddSplitSettingsVM { get; set; }

        public DeviceType DeviceType { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PreseedSettingsViewModel);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17, prime = 23;

                hash = hash * prime + Mirror.GetHashCode();
                hash = hash * prime + RootPassword.GetHashCode();
                hash = hash * prime + UserName.GetHashCode();
                hash = hash * prime + UserFullName.GetHashCode();
                hash = hash * prime + UserPassword.GetHashCode();
                hash = hash * prime + AdditionalSoft.GetHashCode();
                hash = hash * prime + HddSplitSettingsVM.GetHashCode();
                hash = hash * prime + DeviceType.GetHashCode();
                hash = hash * prime + ImageConfig.GetHashCode();

                return hash;
            }
        }

        public bool Equals(PreseedSettingsViewModel other)
        {
            return other != null && Mirror == other.Mirror
                && RootPassword == other.RootPassword
                && UserName == other.UserName
                && UserFullName == other.UserFullName
                && UserPassword == other.UserPassword
                && AdditionalSoft == other.AdditionalSoft
                && DeviceType == other.DeviceType
                && ImageConfig == other.ImageConfig
                && HddSplitSettingsVM == other.HddSplitSettingsVM;
        }


        public static bool operator ==(PreseedSettingsViewModel a, PreseedSettingsViewModel b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null)
                return false;

            if (b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(PreseedSettingsViewModel a, PreseedSettingsViewModel b) => !(a == b);
    }
}
