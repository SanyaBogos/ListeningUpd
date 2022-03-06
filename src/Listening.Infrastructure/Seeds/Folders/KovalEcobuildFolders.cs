using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Folders
{
    public class KovalEcobuildFolders
    {
        public static Folder[] GetFolders(int folderId, int videoId, int videoTypeId, int autorId)
        {
            var kovFolders = new Folder[]
            {
                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/ФУНДАМЕНТ/Типы фундаментов", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Ленточный", Path = "Lenta", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Плитный", Path = "Plita", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Свайный", Path = "Svai", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Солэкофундамент", Path = "Soleco", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/ФУНДАМЕНТ", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Разметка, опалубка, вязка арматуры", Path = "Razmetka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Закладка коммуникаций", Path = "ZakladCommunicazii", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вибрирование бетона", Path = "VibrirovanieBetona", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/СТЕНЫ", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Мауэрлат", Path = "Mauerlat", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Стеновые панели", Path = "StenoviePaneli", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Межэтажное перекрытие", Path = "MezhetazhnoePerekritie", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/СТЕНЫ/Отделка фасада/Штукатурка", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Глиняная штукатурка", Path = "Glina", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Декоративнее покрытие по штукатурке", Path = "Dekor", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Известковая штукатурка", Path = "Isvest", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/СТЕНЫ/Отделка фасада/Штукатурка/Мастер класс", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Саманный слой", Path = "Saman", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "То, чего не стоить бояться", Path = "NoWorry", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Финишный слой", Path = "FinishSloy", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/СТЕНЫ/Отделка фасада", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Дерево", Path = "Derevo", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Кирпич", Path = "Kirpich", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "ЦСП", Path = "CSP", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/ОКНА/Виды окон", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Алюминиевые окна", Path = "Alumminium", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Деревяные окна", Path = "DerevianieOkna", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Металлопластиковые окна", Path = "MetaloPlastik", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/ОКНА", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Какие окна выбрать?", Path = "KakieOknaVybrat", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Подоконники", Path = "Podokonniki", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/КРОВЛЯ", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Типы кровель", Path = "TipyCrovel", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Устройство кровли", Path = "UstroistvoKrovli", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/КРОВЛЯ/Виды кровельных покрытий", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Металочерепица", Path = "Metalocherepitsa", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Битумная черепица", Path = "Bitum", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Ондулин", Path = "Ondulin", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Тростниковая кровля и дранка", Path = "TrosnikIDranka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Черепица", Path = "Cherepiza", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Шифер и шиферная черепица", Path = "Shifer", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/КОММУНИКАЦИИ", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Теплые стены", Path = "TeplieSteny", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Электрика", Path = "Electrica", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Отопление", Path = "Otoplenie", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Водопровод и канализация", Path = "VodoprovodIKanal", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/ОТДЕЛОЧНЫЕ РАБОТЫ", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Глиняная и известковая штукатурка", Path = "GlinaIIzvest", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Облицовка стен деревом", Path = "Oblizovka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Подшивка потолка", Path = "PodshivkaPotolka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Покрытие пола", Path = "PokrytiePola", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/ОТДЕЛОЧНЫЕ РАБОТЫ/Cаманные перегородки", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "саманные перегородки", Path = "SamanPeregorodki1", VideoTypeId = videoTypeId, Repeat = 3 },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Базовая часть/ОРГАНИЗАЦИЯ СТРОИТЕЛЬСТВА", CourseId = autorId, Path = "base",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Поиск строительной бригады", Path = "PoiskBrigady", VideoTypeId = videoTypeId, Repeat = 3 },
                        new Video { Id = videoId++, Name = "Составление договора", Path = "SostavlenieDogovora", VideoTypeId = videoTypeId, Repeat = 3 },
                        new Video { Id = videoId++, Name = "Поиск и закупка строительных материалов", Path = "PoiskIZakupkaStroiMater", VideoTypeId = videoTypeId, Repeat = 3 },
                        new Video { Id = videoId++, Name = "Строительные инструменты", Path = "StroiInstrum", VideoTypeId = videoTypeId, Repeat = 3 },
                    }
                },


                new Folder
                {
                    Id = folderId++, Name = "Дополнение", CourseId = autorId, Path = "additional",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "20+ рекомендаций как сэкономить на строительстве собственного дома", Path = "TwentyRecomendations", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Гид по инструментам, какой строительный инструмент понадобится чтобы построить экодом", Path = "GidInstruments", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Из чего постоить дом, обзор 6 самых популярных технологий строительства", Path = "SixTehnologiiStroitelstva", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Как спроектировать свой экодом за 3 часа, даже если Вы не знаете с чего начать", Path = "SproecttirovatDomZa3", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Отопление, как выбрать идеальную систему отопления для вашего дома", Path = "Otoplenie", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Теплый дом, как утеплить свой дом и снизить траты на отопление в несколько раз", Path = "KakUteplit", VideoTypeId = videoTypeId },
                    }
                },
            };


            return kovFolders;
        }
    }
}
