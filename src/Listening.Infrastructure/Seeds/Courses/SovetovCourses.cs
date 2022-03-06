using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Courses
{
    public class SovetovCourses
    {
        public static Course[] GetCourses(int id, int typeId, int autorId)
        {
            var sovetovCourses = new Course[]
            {
                new Course
                {
                    Id = id++, Name = "Школа здоровья", TypeId = typeId, AuthorId = autorId,
                    OriginalLink = "https://www.youtube.com/watch?v=y_2MY7xWgf4&list=PLGAxwCwP9FEV0NKF-EE1-KuqU0zXDTdd8"
                },

                new Course
                {
                    Id = id++, Name = "Бег для здоровья", TypeId = typeId, AuthorId = autorId,
                    OriginalLink = "https://www.youtube.com/watch?v=u-1bDjEHSCk&list=PLGAxwCwP9FEU8pAtBvtCHmXClxE0ClNmy"
                },
            };


            return sovetovCourses;
        }
    }
}
