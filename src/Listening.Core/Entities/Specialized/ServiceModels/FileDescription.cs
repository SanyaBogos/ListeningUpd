using Listening.Core;

namespace Listening.Server.Entities.Specialized.ServiceModels
{
    public class FileDescription
    {
        public string Name { get; set; }
        public FileContentType Type { get; set; }

        public FileDescription(string name, FileContentType type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}
