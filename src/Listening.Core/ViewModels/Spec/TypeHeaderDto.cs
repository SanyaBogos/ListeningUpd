using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Core.ViewModels.Spec
{
    public class TypeHeaderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CourseHeaderDto[] Courses { get; set; }
    }
}
