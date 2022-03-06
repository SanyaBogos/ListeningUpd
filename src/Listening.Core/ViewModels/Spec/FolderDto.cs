using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Spec
{
    public class FolderDto
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        //public Course Course { get; set; }

        public VideoDto[] Videos { get; set; }
    }
}
