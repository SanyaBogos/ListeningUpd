using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Courses
{
    public class NatureGuardenFolders
    {
        public static Course[] GetCourses(int id, int typeId, int[] autorIds)
        {
            var natureGuardenCourses = new Course[]
            {
                // Peretyatko
                new Course
                {
                    Id = id++, Name = "Ореховый лесосад", TypeId = typeId, AuthorId = autorIds[0],
                    OriginalSite = "https://golesosad.com", OriginalLink = "https://golesosad.com/product/training/"
                },

                // Orlov
                new Course
                {
                    Id = id++, Name = "Проектирование родовго поместья" , TypeId = typeId, AuthorId = autorIds[1],
                    OriginalSite = "http://semena-sveta.ru", OriginalLink = "http://semena-sveta.ru/product/dendrologicheskiy-marafon-3000-vidov"
                },
            };


            return natureGuardenCourses;
        }
    }
}
