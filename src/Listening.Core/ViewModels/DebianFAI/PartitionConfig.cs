

using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.DebianFAI
{
    public class PartitionConfig : IEquatable<PartitionConfig>
    {
        public int Size { get; set; }
        public PartitionType PartitionType { get; set; }
        public FileSystemType FileSystemType { get; set; }

        public override string ToString()
        {
            var fsTypeName = Enum.GetName(typeof(FileSystemType), FileSystemType);
            var result = string.Format(_templates[PartitionType], Size.ToString(), fsTypeName);
            return result;
        }


        public bool Equals(PartitionConfig other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return other != null && Size == other.Size
                        && PartitionType == other.PartitionType
                        && FileSystemType == other.FileSystemType;
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

                hash = hash * prime + Size.GetHashCode();
                hash = hash * prime + PartitionType.GetHashCode();
                hash = hash * prime + FileSystemType.GetHashCode();

                return hash;
            }
        }

        public static bool operator ==(PartitionConfig a, PartitionConfig b)
        {
            if (ReferenceEquals(a, b))
                return true;

            if (a is null)
                return false;

            if (b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(PartitionConfig a, PartitionConfig b) => !(a == b);

        private static Dictionary<PartitionType, string> _templates = new Dictionary<PartitionType, string> {
            { PartitionType.boot, @"            {0} {0} {0} {1}                           \
            $primary{{ }} $bootable{{ }}                \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /boot }}                     \
        .                                           \" },
            { PartitionType.swap, @"            {0} {0} {0} linux-swap                           \
            $lvmok{{ }}                               \
            lv_name{{ swap }} in_vg {{ debian }}        \
            $primary{{ }}                             \
            method{{ swap }} format{{ }}                \
        .                                           \" },
            { PartitionType.root, @"            {0} {0} -1 {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ root }} in_vg {{ debian }}        \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ / }}                         \
        .                                           \" },
            { PartitionType.tmp, @"            {0} {0} {0} {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ tmp }} in_vg {{ debian }}         \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /tmp }}                      \
        .                                           \" },
            { PartitionType.var, @"            {0} {0} {0} {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ var }} in_vg {{ debian }}         \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /var }}                      \
        .                                           \" },
            { PartitionType.varLog, @"            {0} {0} {0} {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ var_log }} in_vg {{ debian }}     \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /var/log }}                  \
        .                                           \" },

           { PartitionType.varTmp, @"            {0} {0} {0} {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ var_tmp }} in_vg {{ debian }}     \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /var/tmp }}                  \
        .                                           \" },

           { PartitionType.home, @"            {0} {0} {0} {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ home }} in_vg {{ debian }}     \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /home }}                  \
        .                                           \" },

           { PartitionType.rootPart, @"            {0} {0} {0} {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ root_ }} in_vg {{ debian }}     \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /root }}                  \
        .                                           \" },

            { PartitionType.opt, @"            {0} {0} {0} {1}                           \
            $lvmok{{ }}                               \
            lv_name{{ opt }} in_vg {{ debian }}     \
            $primary{{ }}                             \
            method{{ format }} format{{ }}              \
            use_filesystem{{ }} filesystem{{ {1} }}    \
            mountpoint{{ /opt }}                  \
        .                                           \" }
        };
    }
}