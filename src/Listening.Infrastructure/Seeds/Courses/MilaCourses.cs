using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Courses
{
    public class MilaCourses
    {
        public static Course[] GetCourses(int id, int typeId, int autorId)
        {
            var milaCourses = new Course[]
            {
                new Course
                {
                    Id = id++, Name = "Хранители и берегини семьи", TypeId = typeId, AuthorId = autorId,
                    OriginalSite = "https://artmilana.com", OriginalLink = "https://artmilana.com/pay?var=4"
                },

                new Course
                {
                    Id = id++, Name = "Консультант по предназначению", TypeId = typeId, AuthorId = autorId,
                    OriginalSite = "https://artmilana.com", OriginalLink = "https://artmilana.com/pay?var=1"
                },
            };


            return milaCourses;
        }
    }
}
