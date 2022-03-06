using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listening.Core.ViewModels.DebianFAI
{
    public class HddSplitSettingsViewModel : IEquatable<HddSplitSettingsViewModel>
    {
        public PartitionConfig[] Configs { get; set; }

        public override string ToString()
        {
            var configStrings = string.Join(' ', Configs.Select(x => x.ToString()));

            var result = $@"d-i partman-auto/expert_recipe string               \
                boot-lvm ::                                     \
                    {configStrings}";

            return result;
        }

        public bool Equals(HddSplitSettingsViewModel other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return other != null && Enumerable.SequenceEqual(Configs, other.Configs);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as HddSplitSettingsViewModel);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17, prime = 23;

                foreach (var config in Configs)
                    hash = hash * prime + config.GetHashCode();

                return hash;
            }
        }

        public static bool operator ==(HddSplitSettingsViewModel a, HddSplitSettingsViewModel b) 
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null)
                return false;

            if (b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(HddSplitSettingsViewModel a, HddSplitSettingsViewModel b) => !(a == b);
    }
}
