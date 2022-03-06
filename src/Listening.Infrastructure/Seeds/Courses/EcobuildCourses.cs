using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Courses
{
    public class EcobuildCourses
    {
        public static Course[] GetCourses(int id, int bookId, int typeId, int[] autorIds, int[] filetypeIds)
        {
            var ecobuildCourses = new Course[]
            {
                // Shirokov
                new Course
                {
                    Id = id++, Name = "Проектируем себе экодом", TypeId = typeId, AuthorId = autorIds[0],
                    OriginalLink = "http://courses.ecoschool.info/land/shirokov",
                    Books = new Book[]
                    {
                        new Book { Id = bookId++, Name = "Презентация", Path = "shir/present", FileTypeId = filetypeIds[0] },
                        new Book { Id = bookId++, Name = "Соломенные дома", Path = "shir/straw", FileTypeId = filetypeIds[0] },
                        new Book { Id = bookId++, Name = "Экотехнология биопозитивных и энергоэффективных конструкций 1", Path = "shir/eco_tech_1", FileTypeId = filetypeIds[0] },
                        new Book { Id = bookId++, Name = "Экотехнология биопозитивных и энергоэффективных конструкций 2", Path = "shir/eco_tech_2", FileTypeId = filetypeIds[0] },
                    }
                },

                //new Course { Id = id++, Name = "Проектируем себе экодом (опыт)", TypeId = typeId, AuthorId = shirok.Id, 
                //    OriginalLink = "http://courses.ecoschool.info/land/shirokov" },

                //new Course { Id = id++, Name = "Проектируем себе экодом (дом за 5 дней)", TypeId = typeId, AuthorId = shirok.Id, 
                    //OriginalLink = "http://courses.ecoschool.info/land/shirokov" },

                // Kovalenko
                new Course {
                    Id = id++, Name = "Солэкодом" , TypeId = typeId, AuthorId = autorIds[1],
                    OriginalSite ="http://solecodom.ru", OriginalLink = "http://solecodom.ru/landing/",
                    Books = new Book[]
                    {
                        new Book { Id = bookId++, Name = "Из квартиры в дом", Path = "kov/iz_kvart", FileTypeId = filetypeIds[0] },
                    }
                },
                //new Course { Id = id++, Name = "Солэкодом (дополнение)" , TypeId = typeId, AuthorId = koval.Id, 
                //    OriginalSite ="http://solecodom.ru", OriginalLink = "http://solecodom.ru/landing/" },

                // Kedrovka
                new Course
                {
                    Id = id++, Name = "Кедровка" , TypeId = typeId, AuthorId = autorIds[2],
                    OriginalSite ="http://kedrovka.com.ua",
                    Books = new Book[]
                    {
                        new Book { Id = bookId++, Name = "Дом из самана — Эванс Я., Смит М.Д., Смайли Л.", Path = "ked/dom_iz_samana", FileTypeId = filetypeIds[0] },
                        new Book { Id = bookId++, Name = "Кладка печей своими руками — Шепелев", Path = "ked/kladka_pech", FileTypeId = filetypeIds[0] },
                        new Book { Id = bookId++, Name = "Штукатурные декоративно художественные работы — Шепелев", Path = "ked/shtuk_dekor", FileTypeId = filetypeIds[1] },
                        new Book { Id = bookId++, Name = "Современная кровля — Савельев", Path = "ked/sovrem_krovlya", FileTypeId = filetypeIds[0] },
                        new Book { Id = bookId++, Name = "Энциклопедия работ по дереву — Дэвид Дей", Path = "ked/entsikl_rab_po_der", FileTypeId = filetypeIds[0] },
                        new Book { Id = bookId++, Name = "Основные инструменты для обработки дерева — Хаслак П.", Path = "ked/haslak_obrb_derev", FileTypeId = filetypeIds[1] },
                    }
                },
            };


            return ecobuildCourses;
        }
    }
}
