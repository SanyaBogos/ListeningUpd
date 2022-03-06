

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Listening.Core.ViewModels.DebianFAI
{
    public class ImageConfig : IEquatable<ImageConfig>
    {
        public string Name { get; set; }
        public ArchitectureType ArchitectureType { get; set; }
        public UrlType UrlType { get; set; }


        public override string ToString()
        {
            var architectureName = Enum.GetName(typeof(ArchitectureType), ArchitectureType);

            if (architectureName.Contains("_"))
            {
                var parts = architectureName.Split('_');
                architectureName = $"{parts[0]}-{parts[1][0].ToString().ToUpper()}{parts[1].Substring(1)}";
            }

            var urlName = _urlTypes[UrlType];
            var extension = _extensionTypes[UrlType];

            var dotIndex = Name.IndexOf('.');
            var shouldRemoveDotsFromName = false;

            if (dotIndex >= 0)
            {
                var numString = Name.Substring(0, dotIndex);
                var number = Convert.ToInt32(numString);
                shouldRemoveDotsFromName = number < 6;
            }

            var preparedName = shouldRemoveDotsFromName ? Regex.Replace(Name, @"[^0-9]+", "") : Name;
            var imageName = $@"debian-{preparedName}-{architectureName}-netinst.{extension}";
            var fullUrl = string.Format($@"{Name.Replace('-', '_').ToLower()}/{architectureName}/{urlName}/{imageName}");

            return fullUrl;
        }


        public bool Equals(ImageConfig other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return other != null && Name == other.Name
                        && ArchitectureType == other.ArchitectureType
                        && UrlType == other.UrlType;
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

                hash = hash * prime + Name.GetHashCode();
                hash = hash * prime + ArchitectureType.GetHashCode();
                hash = hash * prime + UrlType.GetHashCode();

                return hash;
            }
        }

        public static bool operator ==(ImageConfig a, ImageConfig b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null)
                return false;

            if (b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ImageConfig a, ImageConfig b) => !(a == b);

        private static Dictionary<UrlType, string> _urlTypes = new Dictionary<UrlType, string>() {
            { UrlType.iso, "iso-cd" },
            { UrlType.hybrid, "iso-hybrid" },
            { UrlType.jigdo, "jigdo-cd" }
        };

        private static Dictionary<UrlType, string> _extensionTypes = new Dictionary<UrlType, string>() {
            { UrlType.iso, "iso" },
            { UrlType.hybrid, "iso" },
            { UrlType.jigdo, "jigdo" }
        };
    }
}