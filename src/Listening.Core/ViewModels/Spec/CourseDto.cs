using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Spec
{
    public class CourseDto
    {
        public int Id { get; set; }

        //public int TypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string OriginalSite { get; set; }

        public string OriginalLink { get; set; }

        //public TypeDto Type { get; set; }
        public string Type { get; set; }

        //public AuthorDto Author { get; set; }
        public string Author { get; set; }

        public FolderDto[] Folders { get; set; }

        public BookDto[] Books { get; set; }
    }
}
