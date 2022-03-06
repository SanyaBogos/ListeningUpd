using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Folders
{
    public class ShirEcobuildFolders
    {
        public static Folder[] GetFolders(int folderId, int videoId, int videoTypeId, int autorId)
        {
            var yuraKovPract = "Юра Коваленко. Практика строительства";
            var yuraKovWeb = "Юра Коваленко: Вебинар";
            var howToBuild = "Выступление на 'Как построить экодом 2014' ";

            var shirFolders = new Folder[]
            {
                new Folder
                {
                    Id = folderId++, Name = "Базовая часть", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Введение. История развития соломенных технологий. Энергоинформатика.", Path = "01_vvedenie_istoria_razvitia_energoinformatika_", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Конструктив экодомов. Печи. Золотое сечение. Часть 1", Path = "02_1_Konstruktive_pechi_zolotoe_sechenie", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Конструктив экодомов. Печи. Золотое сечение. Часть 2", Path = "02_2_Konstruktive_pechi_zolotoe_sechenie", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Экодизайн. Примеры проектов экодомов.", Path = "03_ecodesign_primery_proektov", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Проекты. Нюансы строительства.", Path = "04_nuansy_stroitelstva", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Пермакультура", Path = "05_permacultura", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Пермакультура. Продолжение.", Path = "06_permacultura_prodolzhenie", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Выбор участка. Ответы на вопросы.", Path = "07_vibor_uchastka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Чистки", Path = "08_1_chistka_", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Печи. Нюансы строительства.", Path = "09_pechi_njuanses", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Ответы на вопросы. Проекты участников каркас.", Path = "10_Otvety_na_voprosy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Заключительная.", Path = "11_", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Опыт", CourseId = autorId, Path = "exp",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = $"{yuraKovPract} 1", Path = "pract1", VideoTypeId = videoTypeId, Repeat = 5 },
                        new Video { Id = videoId++, Name = $"{yuraKovPract} 2", Path = "pract2", VideoTypeId = videoTypeId, Repeat = 3 },
                        new Video { Id = videoId++, Name = $"{yuraKovPract} 3", Path = "pract3", VideoTypeId = videoTypeId, Repeat = 5 },
                        new Video { Id = videoId++, Name = $"{howToBuild} ", Path = "kak_stroit", VideoTypeId = videoTypeId, Repeat = 8 },
                        new Video { Id = videoId++, Name = $"{yuraKovWeb} ", Path = "webinar", VideoTypeId = videoTypeId, Repeat = 8 },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Дом за 5 дней", CourseId = autorId, Path = "dom_za_5",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Проект под Омском ", Path = "omsk", VideoTypeId = videoTypeId, Repeat = 5 },
                        new Video { Id = videoId++, Name = "Проект в Татарстане 1", Path = "tatar1", VideoTypeId = videoTypeId, Repeat = 4 },
                        new Video { Id = videoId++, Name = "Проект в Татарстане 2", Path = "tatar2", VideoTypeId = videoTypeId, Repeat = 3 },
                        new Video { Id = videoId++, Name = "Проект в Татарстане 3", Path = "tatar3", VideoTypeId = videoTypeId, Repeat = 5 },
                        new Video { Id = videoId++, Name = "Проект в Татарстане 4", Path = "tatar4", VideoTypeId = videoTypeId, Repeat = 2 },
                        new Video { Id = videoId++, Name = "Проект в Татарстане 5", Path = "tatar5", VideoTypeId = videoTypeId, Repeat = 4 },
                        new Video { Id = videoId++, Name = "Проект в Татарстане 6", Path = "tatar6", VideoTypeId = videoTypeId, Repeat = 2 },
                    }
                },
            };


            return shirFolders;
        }
    }
}
