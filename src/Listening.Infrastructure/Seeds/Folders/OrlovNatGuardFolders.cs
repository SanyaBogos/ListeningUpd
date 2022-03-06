using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Folders
{
    public class OrlovNatGuardFolders
    {
        public static Folder[] GetFolders(int folderId, int videoId, int videoTypeId, int autorId)
        {
            var orlovFolders = new Folder[]
            {
                new Folder
                {
                    Id = folderId++, Name = "Предпроектное исследование", CourseId = autorId, Path = "Predproektnoe_issledovanie",
                    Videos = new Video[]
                    {
                        //new Video { Id = videoId++, Name = "лекция", Path = "lektsiya.jpg", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Лекция", Path = "video", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вебинар", Path = "vebinar", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Предпроектное резюме", CourseId = autorId, Path = "Predproektnoe_rezyume",
                    Videos = new Video[]
                    {
                        //new Video { Id = videoId++, Name = "лекция", Path = "lektsiya.jpg", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Лекция", Path = "video", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вебинар", Path = "vebinar", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Позиционное зонирование", CourseId = autorId, Path = "Pozitsionnoe_zonirovanie",
                    Videos = new Video[]
                    {
                        //new Video { Id = videoId++, Name = "лекция", Path = "lektsiya.jpg", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Лекция", Path = "video", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вебинар", Path = "vebinar", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Ассортиментная ведомость, медоносы добавочного взятка, дендрологический план", CourseId = autorId, Path = "lect_4_5",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Ассортиментная ведомость", Path = "4_Assortimentnaya_vedomost", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Медоносы добавочного взятка", Path = "4_2_Medonosy_dobavochnogo_vzyatka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Дендрологический план", Path = "5_Dendrologicheskiy_plan", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вебинар", Path = "vebinar_k_lektsiyam_4_i_5", VideoTypeId = videoTypeId },
                        //new Video { Id = videoId++, Name = "лекция", Path = "lektsiya.jpg", VideoTypeId = videoTypeId },
                        //new Video { Id = videoId++, Name = "лекция", Path = "lektsiya.jpg", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Генеральный и детальные планы", CourseId = autorId, Path = "lect_6_7",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Генеральный план", Path = "6_Generalnyy_plan", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Детальные планы", Path = "7_Detalnye_plany", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вебинар", Path = "vebinar_k_lektsiyam_6_i_7", VideoTypeId = videoTypeId },
                        //new Video { Id = videoId++, Name = "лекция", Path = "lektsiya.jpg", VideoTypeId = videoTypeId },
                        //new Video { Id = videoId++, Name = "лекция", Path = "lektsiya.jpg", VideoTypeId = videoTypeId },
                    }
                },
            };


            return orlovFolders;
        }
    }
}
