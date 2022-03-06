using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Folders
{
    public class FrolovFolders
    {
        public static Folder[] GetFolders(int folderId, int videoId, int[] videoTypeIds, 
            Dictionary<int, int> courseIds, Dictionary<int, int> miniCourseIds)
        {
            var frolovFolders = new Folder[]
            {

                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Бонус/Большой вебинар - 6-и часовой марафон", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Bonus/Bolshoy_vebinar_-_6-i_chasovoy_marafon",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Ю.А. Фролов на марафоне Доброздравина(6 часов) 1 часть", Path = "Yu_A__Frolov_na_marafone_Dobrozdravina(6_chasov)_1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Ю.А. Фролов на марафоне Доброздравина(6 часов) 2 часть", Path = "Yu_A__Frolov_na_marafone_Dobrozdravina(6_chasov)_2_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Ю.А. Фролов на марафоне Доброздравина(6 часов) 3 часть", Path = "Yu_A__Frolov_na_marafone_Dobrozdravina(6_chasov)_3_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Бонус/Вводная лекция для начинающих и в помощь Вам для отрезвления близких", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Bonus/Vvodnaya_lektsiya_dlya_nachinayushchikh_i_v_pomoshch_Vam_dlya_otrezvleniya_blizkikh",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Фролов Юрий Андреевич выступление на Ассамблее Эко-Здоровья- Развенчание стереотипов", Path = "Frolov_Yuriy_Andreevich_vystuplenie_na_Assamblee_Eko-Zdorovya-_Razvenchanie_stereotipov", VideoTypeId = videoTypeIds[4] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Бонус/Лекция от 5 марта", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Bonus/Lektsiya_ot_5_marta",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Выступление Фролова Ю. А. 5 марта на коференции _Здоровье и красота 2014_ - 1 часть - YouTube [720p]", Path = "Vystuplenie_Frolova_Yu__A__5_marta_na_koferentsii__Zdorove_i_krasota_2014__-_1_chast_-_YouTube_[720p]", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Выступление Фролова Ю. А. 5 марта на коференции _Здоровье и красота 2014_ - 2 часть - YouTube [720p]", Path = "Vystuplenie_Frolova_Yu__A__5_marta_na_koferentsii__Zdorove_i_krasota_2014__-_2_chast_-_YouTube_[720p]", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Бонус/Онкология и живое питание", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Bonus/Onkologiya_i_zhivoe_pitanie",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Как сыроедение лечит онкологию Ю А Фролов 1 часть mp4", Path = "Kak_syroedenie_lechit_onkologiyu_Yu_A_Frolov_1_chast_mp4", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Как сыроедение лечит онкологию Ю А Фролов 2 часть mp4", Path = "Kak_syroedenie_lechit_onkologiyu_Yu_A_Frolov_2_chast_mp4", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Вебинар 11-12.12.13/Вебинар 11.12.2013/Видеозапись", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Vebinar_11-12_12_13/Vebinar_11_12_2013/Videozapis",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Семинар Ю.А.Фролова. День 1", Path = "Seminar_Yu_A_Frolova__Den_1", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Вебинар 11-12.12.13/Вебинар 12.12.2013/Видеозапись", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Vebinar_11-12_12_13/Vebinar_12_12_2013/Videozapis",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Семинар Ю.А.Фролова. День 2", Path = "Seminar_Yu_A_Frolova__Den_2", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Вебинар 31.10.2013", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Vebinar_31_10_2013",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Вебинар 31.10.2013", Path = "Vebinar_31_10_2013", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Закрытый вебинар/День 1 - Кожа, Печень, Почки, Лимфатическая система/Запись на видеокамеру", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Zakrytyy_vebinar/Den_1_-_Kozha,_Pechen,_Pochki,_Limfaticheskaya_sistema/Zapis_na_videokameru",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "▶ День 1 Кожа, Печень, Почки, Лимфатическая система", Path = "_arrow_forward__Den_1_Kozha,_Pechen,_Pochki,_Limfaticheskaya_sistema", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Закрытый вебинар/День 1 - Кожа, Печень, Почки, Лимфатическая система/Запись с ноутбука", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Zakrytyy_vebinar/Den_1_-_Kozha,_Pechen,_Pochki,_Limfaticheskaya_sistema/Zapis_s_noutbuka",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Вебинар Фролова _Ошибки_ день 1 часть 1", Path = "Vebinar_Frolova__Oshibki__den_1_chast_1", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Вебинар Фролова _Ошибки_ день 1 часть 2", Path = "Vebinar_Frolova__Oshibki__den_1_chast_2", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Закрытый вебинар/День 2 - Паразиты и Суставы/Запись на видеокамеру", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Zakrytyy_vebinar/Den_2_-_Parazity_i_Sustavy/Zapis_na_videokameru",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "День 2 Паразиты и Суставы", Path = "Den_2_Parazity_i_Sustavy", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Закрытый вебинар/День 2 - Паразиты и Суставы/Запись с ноутбука", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Zakrytyy_vebinar/Den_2_-_Parazity_i_Sustavy/Zapis_s_noutbuka",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Фролов Вебинар _Ошибки_ День 2 Часть 1", Path = "Frolov_Vebinar__Oshibki__Den_2_Chast_1", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Фролов вебинар _Ошибки_ День 2 Часть 2", Path = "Frolov_vebinar__Oshibki__Den_2_Chast_2", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Ошибки  на сыроедении/Ошибки  на сыроедении. 04.02.2014", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Oshibki__na_syroedenii/Oshibki__na_syroedenii__04_02_2014",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Ошибки на сыроедении - 1 часть", Path = "Oshibki_na_syroedenii_-_1_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/1. Вебинары/Ошибки  на сыроедении/Ошибки в питании на сыроедении. 06.02.2014", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/1__Vebinary/Oshibki__na_syroedenii/Oshibki_v_pitanii_na_syroedenii__06_02_2014",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Ошибки на сыроедении - 2 часть", Path = "Oshibki_na_syroedenii_-_2_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №1 пакет с видеозаписями. Мать и Ребёнок. Грибковые инфекции. Онкология/2. Съёмка программы для ТВ-я", CourseId = courseIds[1], Path = "Inf_No1_paket_s_videozapisyami__Mat_i_Rebenok__Gribkovye_infektsii__Onkologiya/2__Semka_programmy_dlya_TV-ya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Съёмка программы для ТВ-я", Path = "Semka_programmy_dlya_TV-ya", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №2 пакет с видеозаписями. Уникальная информация для жизни/Выступление 15-16 марта 2014", CourseId = courseIds[2], Path = "Inf_No2_paket_s_videozapisyami__Unikalnaya_informatsiya_dlya_zhizni/Vystuplenie_15-16_marta_2014",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "выступление 15-16 марта", Path = "vystuplenie_15-16_marta", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №2 пакет с видеозаписями. Уникальная информация для жизни/Выступление 15-16 марта 2014/Запись от 26 марта 2014", CourseId = courseIds[2], Path = "Inf_No2_paket_s_videozapisyami__Unikalnaya_informatsiya_dlya_zhizni/Vystuplenie_15-16_marta_2014/Zapis_ot_26_marta_2014",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Запись от 26 марта", Path = "Zapis_ot_26_marta", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №2 пакет с видеозаписями. Уникальная информация для жизни/Ответы на вопросы 25 марта", CourseId = courseIds[2], Path = "Inf_No2_paket_s_videozapisyami__Unikalnaya_informatsiya_dlya_zhizni/Otvety_na_voprosy_25_marta",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "25 марта ответы на вопросы", Path = "25_marta_otvety_na_voprosy", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/1. Запись семинара 19.04.2014/1 камера", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/1__Zapis_seminara_19_04_2014/1_kamera",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "▶ 1 часть 1 камера", Path = "_arrow_forward__1_chast_1_kamera", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "▶ 2 часть 1 камера", Path = "_arrow_forward__2_chast_1_kamera", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "▶ 3 часть 1 камера", Path = "_arrow_forward__3_chast_1_kamera", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "▶ 4 часть 1 камера", Path = "_arrow_forward__4_chast_1_kamera", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "▶ 5 часть 1 камера", Path = "_arrow_forward__5_chast_1_kamera", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/1. Запись семинара 19.04.2014/2 камера", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/1__Zapis_seminara_19_04_2014/2_kamera",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "▶ 1 часть 2 камера", Path = "_arrow_forward__1_chast_2_kamera", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "▶ 2 часть 2 камера", Path = "_arrow_forward__2_chast_2_kamera", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "▶ 3 часть 2 камера", Path = "_arrow_forward__3_chast_2_kamera", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/1. Запись семинара 19.04.2014/3 камера/practika_laboratoriya", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/1__Zapis_seminara_19_04_2014/3_kamera/practika_laboratoriya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "practika v labaratorii", Path = "practika_v_labaratorii", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/1. Запись семинара 19.04.2014/3 камера/practika_v_zale_utro", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/1__Zapis_seminara_19_04_2014/3_kamera/practika_v_zale_utro",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "practika v zale utro", Path = "practika_v_zale_utro", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/1. Запись семинара 19.04.2014/3 камера/practika_v_zale_vecher_kartoshino", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/1__Zapis_seminara_19_04_2014/3_kamera/practika_v_zale_vecher_kartoshino",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "practika v zale vecher part1", Path = "practika_v_zale_vecher_part1", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "practika v zale vecher part2", Path = "practika_v_zale_vecher_part2", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/1. Запись семинара 19.04.2014/3 камера/theory_utro_v_zale", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/1__Zapis_seminara_19_04_2014/3_kamera/theory_utro_v_zale",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "teoriya utro v zale part 0", Path = "teoriya_utro_v_zale_part_0", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "teoriya utro v zale part 1", Path = "teoriya_utro_v_zale_part_1", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "teoriya utro v zale part 2", Path = "teoriya_utro_v_zale_part_2", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "teoriya utro v zale part 3", Path = "teoriya_utro_v_zale_part_3", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/2. Семинар 29го марта/№1", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/2__Seminar_29go_marta/No1",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1 часть", Path = "1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2 часть", Path = "2_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3 часть", Path = "3_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №3 - Двухдневный аграрный семинар Фролова Ю.А/2. Семинар 29го марта/№2", CourseId = courseIds[3], Path = "Inf_No3_-_Dvukhdnevnyy_agrarnyy_seminar_Frolova_Yu_A/2__Seminar_29go_marta/No2",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1 часть", Path = "1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2 часть", Path = "2_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3 часть", Path = "3_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №4 Второй аграрный семинар Фролова Ю. А/1. Камера 1", CourseId = courseIds[4], Path = "Inf_No4_Vtoroy_agrarnyy_seminar_Frolova_Yu__A/1__Kamera_1",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Картошино 1 часть", Path = "Kartoshino_1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Картошино 2 часть ", Path = "Kartoshino_2_chast_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Картошино 3 часть", Path = "Kartoshino_3_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Картошино 4 часть", Path = "Kartoshino_4_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Картошино 5 часть", Path = "Kartoshino_5_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №4 Второй аграрный семинар Фролова Ю. А/2. Камера 2", CourseId = courseIds[4], Path = "Inf_No4_Vtoroy_agrarnyy_seminar_Frolova_Yu__A/2__Kamera_2",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Картошино 2 камера улица", Path = "Kartoshino_2_kamera_ulitsa", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №5 Паразиты, Глисты/1. Аудио запись с кадрами", CourseId = courseIds[5], Path = "Inf_No5_Parazity,_Glisty/1__Audio_zapis_s_kadrami",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "2 новых глиста", Path = "2_novykh_glista", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Вводное слово Общие положения", Path = "Vvodnoe_slovo_Obshchie_polozheniya", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Дегельминтизация", Path = "Degelmintizatsiya", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Коло Вада 2 плюс Заключение", Path = "Kolo_Vada_2_plyus_Zaklyuchenie", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Народные средства и методики", Path = "Narodnye_sredstva_i_metodiki", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №6 Паразиты, глисты 2-х дневный вебинар – 3 в 1/1. Вебинар 7го мая", CourseId = courseIds[6], Path = "Inf_No6_Parazity,_glisty_2-kh_dnevnyy_vebinar_-_3_v_1/1__Vebinar_7go_maya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "вебинар 7го мая", Path = "vebinar_7go_maya", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №6 Паразиты, глисты 2-х дневный вебинар – 3 в 1/2. Семинар по паразитам 27 и 28го мая 2014/Вебинар Ю.А.Фролова -Как избавиться от паразитов- Видео материалы", CourseId = courseIds[6], Path = "Inf_No6_Parazity,_glisty_2-kh_dnevnyy_vebinar_-_3_v_1/2__Seminar_po_parazitam_27_i_28go_maya_2014/Vebinar_Yu_A_Frolova_-Kak_izbavitsya_ot_parazitov-_Video_materialy",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Вебинар Ю.А.Фролова -Как избавиться от паразитов- Часть 1", Path = "Vebinar_Yu_A_Frolova_-Kak_izbavitsya_ot_parazitov-_Chast_1", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Вебинар Ю.А.Фролова -Как избавиться от паразитов- Часть 2", Path = "Vebinar_Yu_A_Frolova_-Kak_izbavitsya_ot_parazitov-_Chast_2", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №7 ЭКО Строительство от А до Я – 2 в 1/1. Семинар 30.05-01.06", CourseId = courseIds[7], Path = "Inf_No7_EKO_Stroitelstvo_ot_A_do_Ya_-_2_v_1/1__Seminar_30_05-01_06",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "30 - приветствие", Path = "30_-_privetstvie", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "31 мая 3часть", Path = "31_maya_3chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №7 ЭКО Строительство от А до Я – 2 в 1/2. Запись вебинара по экостроительству", CourseId = courseIds[7], Path = "Inf_No7_EKO_Stroitelstvo_ot_A_do_Ya_-_2_v_1/2__Zapis_vebinara_po_ekostroitelstvu",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "вебинар 1часть", Path = "vebinar_1chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "вебинар 2часть", Path = "vebinar_2chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "вебинар 3часть", Path = "vebinar_3chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "вебинар 4часть", Path = "vebinar_4chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №8 Методики излечения от грибковых заболеваний, кожных и внутренних поражений/1. Методичка по грибкам", CourseId = courseIds[8], Path = "Inf_No8_Metodiki_izlecheniya_ot_gribkovykh_zabolevaniy,_kozhnykh_i_vnutrennikh_porazheniy/1__Metodichka_po_gribkam",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Вступление к методичке по Грибкам", Path = "Vstuplenie_k_metodichke_po_Gribkam", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №8 Методики излечения от грибковых заболеваний, кожных и внутренних поражений/2. Бонус для скачивания. Встреча с Ю.А. Фроловым по теме Грибковые заболевания", CourseId = courseIds[8], Path = "Inf_No8_Metodiki_izlecheniya_ot_gribkovykh_zabolevaniy,_kozhnykh_i_vnutrennikh_porazheniy/2__Bonus_dlya_skachivaniya__Vstrecha_s_Yu_A__Frolovym_po_teme_Gribkovye_zabolevaniya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Встреча с Ю.А. Фроловым по теме Грибковые заболевания", Path = "Vstrecha_s_Yu_A__Frolovym_po_teme_Gribkovye_zabolevaniya", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №9 Вода, болезни обезвоживания, Лимфа, Почки – болезни, их взаимосвязи и методики лечения/1. Бонус - Сёмка передачи (для РЭН ТВ) Фролов Ю. А. О Воде", CourseId = courseIds[9], Path = "Inf_No9_Voda,_bolezni_obezvozhivaniya,_Limfa,_Pochki_-_bolezni,_ikh_vzaimosvyazi_i_metodiki_lecheniya/1__Bonus_-_Semka_peredachi_(dlya_REN_TV)_Frolov_Yu__A__O_Vode",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Бонус - Сёмка передачи (для РЭН ТВ) о  Воде", Path = "Bonus_-_Semka_peredachi_(dlya_REN_TV)_o__Vode", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №9 Вода, болезни обезвоживания, Лимфа, Почки – болезни, их взаимосвязи и методики лечения/2. Бонус - Выступление Фролова Ю. А. о воде на конференции врачей - сыроедов", CourseId = courseIds[9], Path = "Inf_No9_Voda,_bolezni_obezvozhivaniya,_Limfa,_Pochki_-_bolezni,_ikh_vzaimosvyazi_i_metodiki_lecheniya/2__Bonus_-_Vystuplenie_Frolova_Yu__A__o_vode_na_konferentsii_vrachey_-_syroedov",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Бонус - Выступление Фролова Ю.А о воде на конференции врачей - сыроедов", Path = "Bonus_-_Vystuplenie_Frolova_Yu_A_o_vode_na_konferentsii_vrachey_-_syroedov", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №9 Вода, болезни обезвоживания, Лимфа, Почки – болезни, их взаимосвязи и методики лечения/4. Запись вебинара 31.06.2013 от Фролова Ю. А. Вебинар про Воду-", CourseId = courseIds[9], Path = "Inf_No9_Voda,_bolezni_obezvozhivaniya,_Limfa,_Pochki_-_bolezni,_ikh_vzaimosvyazi_i_metodiki_lecheniya/4__Zapis_vebinara_31_06_2013_ot_Frolova_Yu__A__Vebinar_pro_Vodu-",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Запись вебинара 31.06.2013 от Фролова Ю. А. Вебинар про Воду", Path = "Zapis_vebinara_31_06_2013_ot_Frolova_Yu__A__Vebinar_pro_Vodu", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №10 Печень, желчный пузырь, поджелудочная железа — методики лечения всевозможных заболеваний. Комплексная система. Приборы/1. Путеводитель по Продукту №10", CourseId = courseIds[10], Path = "Inf_No10_Pechen,_zhelchnyy_puzyr,_podzheludochnaya_zheleza_-_metodiki_lecheniya_vsevozmozhnykh_zabolevaniy__Kompleksnaya_sistema__Pribory/1__Putevoditel_po_Produktu_No10",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Путеводитель по Продукту №10", Path = "Putevoditel_po_Produktu_No10", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №10 Печень, желчный пузырь, поджелудочная железа — методики лечения всевозможных заболеваний. Комплексная система. Приборы/3. Взаимосвязи Желчного Пузыря, Поджелудочной Железы и Кишечника - акцент!", CourseId = courseIds[10], Path = "Inf_No10_Pechen,_zhelchnyy_puzyr,_podzheludochnaya_zheleza_-_metodiki_lecheniya_vsevozmozhnykh_zabolevaniy__Kompleksnaya_sistema__Pribory/3__Vzaimosvyazi_Zhelchnogo_Puzyrya,_Podzheludochnoy_Zhelezy_i_Kishechnika_-_aktsent",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Взаимосвязи Желчного Пузыря, Поджелудочной Железы и Кишечника - акцент!", Path = "Vzaimosvyazi_Zhelchnogo_Puzyrya,_Podzheludochnoy_Zhelezy_i_Kishechnika_-_aktsent", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №11 4×4 внедорожники, тюнинг. Планирование и подготовка к дальним экспедициям. Безопасность/1. Запись вебинара 24.07.2014", CourseId = courseIds[11], Path = "Inf_No11_4x4_vnedorozhniki,_tyuning__Planirovanie_i_podgotovka_k_dalnim_ekspeditsiyam__Bezopasnost/1__Zapis_vebinara_24_07_2014",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "вебинар 4х4 1 часть", Path = "vebinar_4kh4_1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "вебинар 4х4 2 часть", Path = "vebinar_4kh4_2_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №11 4×4 внедорожники, тюнинг. Планирование и подготовка к дальним экспедициям. Безопасность/3. Видео. Тюнинг 105-ки - история со сметами", CourseId = courseIds[11], Path = "Inf_No11_4x4_vnedorozhniki,_tyuning__Planirovanie_i_podgotovka_k_dalnim_ekspeditsiyam__Bezopasnost/3__Video__Tyuning_105-ki_-_istoriya_so_smetami",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "инфопродукт 11 часть 1", Path = "infoprodukt_11_chast_1", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "инфопродукт 11 часть 2", Path = "infoprodukt_11_chast_2", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №11 4×4 внедорожники, тюнинг. Планирование и подготовка к дальним экспедициям. Безопасность/4. Экономия топлива", CourseId = courseIds[11], Path = "Inf_No11_4x4_vnedorozhniki,_tyuning__Planirovanie_i_podgotovka_k_dalnim_ekspeditsiyam__Bezopasnost/4__Ekonomiya_topliva",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "49_пресс_конференц", Path = "49_press_konferents", VideoTypeId = videoTypeIds[13] },
                        new Video { Id = videoId++, Name = "49_СТО", Path = "49_STO", VideoTypeId = videoTypeIds[13] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №11 4×4 внедорожники, тюнинг. Планирование и подготовка к дальним экспедициям. Безопасность/7. Прочее, интересное/водная ласточка", CourseId = courseIds[11], Path = "Inf_No11_4x4_vnedorozhniki,_tyuning__Planirovanie_i_podgotovka_k_dalnim_ekspeditsiyam__Bezopasnost/7__Prochee,_interesnoe/vodnaya_lastochka",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Водный махолет", Path = "Vodnyy_makholet", VideoTypeId = videoTypeIds[9] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №12 Выживание, подготовка к кризисам, бедствиям, катастрофам, войнам… Апокалипсис, Конец света… Что делать/1. Запись вебинара от 11.09.2014 (будет доступно через 2 дня)", CourseId = courseIds[12], Path = "Inf_No12_Vyzhivanie,_podgotovka_k_krizisam,_bedstviyam,_katastrofam,_voynam____Apokalipsis,_Konets_sveta____Chto_delat/1__Zapis_vebinara_ot_11_09_2014_(budet_dostupno_cherez_2_dnya)",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "вебинар выживание", Path = "vebinar_vyzhivanie", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №12 Выживание, подготовка к кризисам, бедствиям, катастрофам, войнам… Апокалипсис, Конец света… Что делать/2. Видео - подготовка необходимого инвентаря для выживания", CourseId = courseIds[12], Path = "Inf_No12_Vyzhivanie,_podgotovka_k_krizisam,_bedstviyam,_katastrofam,_voynam____Apokalipsis,_Konets_sveta____Chto_delat/2__Video_-_podgotovka_neobkhodimogo_inventarya_dlya_vyzhivaniya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1 часть", Path = "1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2 часть", Path = "2_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3 часть", Path = "3_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "4 часть", Path = "4_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №13 Лечение болезней органов и тканей системы Пищеварения – методики", CourseId = courseIds[13], Path = "Inf_No13_Lechenie_bolezney_organov_i_tkaney_sistemy_Pishchevareniya_-_metodiki",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Методики Лечения заболевани желудочно-кишечного тракта", Path = "Metodiki_Lecheniya_zabolevani_zheludochno-kishechnogo_trakta", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №13 Лечение болезней органов и тканей системы Пищеварения – методики/2. Вебинар от 2 октября", CourseId = courseIds[13], Path = "Inf_No13_Lechenie_bolezney_organov_i_tkaney_sistemy_Pishchevareniya_-_metodiki/2__Vebinar_ot_2_oktyabrya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Вебинар Фролова Ю.А. По Пищеварительной системе от 2 октября 14г. 4 часа 50 минут. - YouTube [720p]", Path = "Vebinar_Frolova_Yu_A__Po_Pishchevaritelnoy_sisteme_ot_2_oktyabrya_14g__4_chasa_50_minut__-_YouTube_[720p]", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №13 Лечение болезней органов и тканей системы Пищеварения – методики/3. Видео инструктор по инфопродукту – путеводитель, пояснитель", CourseId = courseIds[13], Path = "Inf_No13_Lechenie_bolezney_organov_i_tkaney_sistemy_Pishchevareniya_-_metodiki/3__Video_instruktor_po_infoproduktu_-_putevoditel,_poyasnitel",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Видео инструктор", Path = "Video_instruktor", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №13 Лечение болезней органов и тканей системы Пищеварения – методики/4. Методики Лечения заболеваний желудочно-кишечного тракта", CourseId = courseIds[13], Path = "Inf_No13_Lechenie_bolezney_organov_i_tkaney_sistemy_Pishchevareniya_-_metodiki/4__Metodiki_Lecheniya_zabolevaniy_zheludochno-kishechnogo_trakta",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Методики Лечения заболеваний желудочно-кишечного тракта", Path = "Metodiki_Lecheniya_zabolevaniy_zheludochno-kishechnogo_trakta", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №15", CourseId = courseIds[15], Path = "Inf_No15",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1 часть", Path = "1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2 часть", Path = "2_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №16 Запись семинара по Органическому земледелию от 14 марта 2015 года/1 Камера", CourseId = courseIds[16], Path = "Inf_No16_Zapis_seminara_po_Organicheskomu_zemledeliyu_ot_14_marta_2015_goda/1_Kamera",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Семинар 14.03.2015  1 часть", Path = "Seminar_14_03_2015__1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Семинар 14.03.2015 2 часть ", Path = "Seminar_14_03_2015_2_chast_", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №16 Запись семинара по Органическому земледелию от 14 марта 2015 года/2 Камера", CourseId = courseIds[16], Path = "Inf_No16_Zapis_seminara_po_Organicheskomu_zemledeliyu_ot_14_marta_2015_goda/2_Kamera",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Семинар 14.03.2015  1 часть", Path = "Seminar_14_03_2015__1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Семинар 14.03.2015 2 часть", Path = "Seminar_14_03_2015_2_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №16 Запись семинара по Органическому земледелию от 14 марта 2015 года/Рассада", CourseId = courseIds[16], Path = "Inf_No16_Zapis_seminara_po_Organicheskomu_zemledeliyu_ot_14_marta_2015_goda/Rassada",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Рассада", Path = "Rassada", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №17 Кровеносная и кроветворная система. Болезни, методики, лечение", CourseId = courseIds[17], Path = "Inf_No17_Krovenosnaya_i_krovetvornaya_sistema__Bolezni,_metodiki,_lechenie",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Аритмии", Path = "Aritmii", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Варикоз. Венозная Недостаточность", Path = "Varikoz__Venoznaya_Nedostatochnost", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Внутричерепное повышенное давление.", Path = "Vnutricherepnoe_povyshennoe_davlenie_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Гипертония. Гипотония. Гипертонический Криз.", Path = "Gipertoniya__Gipotoniya__Gipertonicheskiy_Kriz_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Инсульт. Мероприятия.", Path = "Insult__Meropriyatiya_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Инфаркт. Срочные меры и Реабилитация.", Path = "Infarkt__Srochnye_mery_i_Reabilitatsiya_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "КардиоНевроз и нарушение капиллярного кровообращения", Path = "KardioNevroz_i_narushenie_kapillyarnogo_krovoobrashcheniya", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Перемежаюшаяся хромота", Path = "Peremezhayushayasya_khromota", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Склероз. Атеросклероз", Path = "Skleroz__Ateroskleroz", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Стенокардия -Ишемическая Болезнь.", Path = "Stenokardiya_-Ishemicheskaya_Bolezn_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Тромбофлебит. Тромбозы. Флебиты. Язвы.", Path = "Tromboflebit__Trombozy__Flebity__Yazvy_", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №18 Лечение болезней органов и тканей Дыхательной системы (бронхо – лёгочные), ухо, горло, нос", CourseId = courseIds[18], Path = "Inf_No18_Lechenie_bolezney_organov_i_tkaney_Dykhatelnoy_sistemy_(bronkho_-_legochnye),_ukho,_gorlo,_nos",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1. Вводное слово к Инфопродукту по Лечению Бронхо лёгочных заболеваний", Path = "1__Vvodnoe_slovo_k_Infoproduktu_po_Lecheniyu_Bronkho_legochnykh_zabolevanii", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2. Очищение организма в срочном порядке, снятие т-ры за час", Path = "2__Ochishchenie_organizma_v_srochnom_poryadke,_snyatie_t-ry_za_chas", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3. Лечение Фарингита, Ларингита, Ангигны", Path = "3__Lechenie_Faringita,_Laringita,_Angigny", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "4. Лечение тонзиллита", Path = "4__Lechenie_tonzillita", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "5. Лечение Бронхита, Обструктивного бронхита", Path = "5__Lechenie_Bronkhita,_Obstruktivnogo_bronkhita", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "6. Лечение Плеврита, Пневмонии", Path = "6__Lechenie_Plevrita,_Pnevmonii", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "7. Лечение Гайморита, Синусита", Path = "7__Lechenie_Gaimorita,_Sinusita", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "8. Лечение Отита, Лабиринтита, болезнь Меньера", Path = "8__Lechenie_Otita,_Labirintita,_bolezn_Menera", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "9. Лечение Ринита (Насморк)", Path = "9__Lechenie_Rinita_(Nasmork)", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "10. Лечение Аденоидов, Аденоидита", Path = "10__Lechenie_Adenoidov,_Adenoidita", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "11. Лечение Туберкулёза Лёгких", Path = "11__Lechenie_Tuberkuleza_Legkikh", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "12. Лечение Бронхиальной Астмы", Path = "12__Lechenie_Bronkhialnoy_Astmy", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №19 2-х дневный Семинар по Эко строительству из хозяйства Фролова Ю.А. В Пушкинских Горах от 24-25.10.15", CourseId = courseIds[19], Path = "Inf_No19_2-kh_dnevnyy_Seminar_po_Eko_stroitelstvu_iz_khozyaystva_Frolova_Yu_A__V_Pushkinskikh_Gorakh_ot_24-25_10_15",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1 день", Path = "1_den", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2 день 1 часть", Path = "2_den_1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2 день 2 часть", Path = "2_den_2_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Инфопродукт №20 Работы по огороду и теплицам в Августе, Осенью и подготовка участка к Зиме", CourseId = courseIds[20], Path = "Inf_No20_Raboty_po_ogorodu_i_teplitsam_v_Avguste,_Osenyu_i_podgotovka_uchastka_k_Zime",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Ответы на вопросы 2-х дневного интенсива 1 часть", Path = "Otvety_na_voprosy_2-kh_dnevnogo_intensiva_1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Ответы на вопросы 2-х дневного интенсива 2 часть", Path = "Otvety_na_voprosy_2-kh_dnevnogo_intensiva_2_chast", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №4 Аденома предстательной железы. Простатит. Причины. Последствия. Профилактика. Комплексные методики лечения", CourseId = miniCourseIds[4], Path = "Min_Inf_No4_Adenoma_predstatelnoy_zhelezy__Prostatit__Prichiny__Posledstviya__Profilaktika__Kompleksnye_metodiki_lecheniya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Аденома и Простатит вводное слово", Path = "Adenoma_i_Prostatit_vvodnoe_slovo", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Лечение Аденомы и Простатита Методики", Path = "Lechenie_Adenomy_i_Prostatita_Metodiki", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №5 Кисты. Киста головного мозга, Кисты яичников, Печени, почек… Комплексное лечение", CourseId = miniCourseIds[5], Path = "Min_Inf_No5_Kisty__Kista_golovnogo_mozga,_Kisty_yaichnikov,_Pecheni,_pochek____Kompleksnoe_lechenie",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Кисты Вступление", Path = "Kisty_Vstuplenie", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Кисты головного мозга", Path = "Kisty_golovnogo_mozga", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Кисты прочие", Path = "Kisty_prochie", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №6 Липомы, Атеромы, Гигромы — Методики безоперационного лечения", CourseId = miniCourseIds[6], Path = "Min_Inf_No6_Lipomy,_Ateromy,_Gigromy_-_Metodiki_bezoperatsionnogo_lecheniya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Липомы Атеромы Гигромы", Path = "Lipomy_Ateromy_Gigromy", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №7 Миомы, Фибромиомы. Причины. Последствия. Профилактика. Лечение натуральными комплексными методами/Мини инфопродукт №7", CourseId = miniCourseIds[7], Path = "Min_Inf_No7_Miomy,_Fibromiomy__Prichiny__Posledstviya__Profilaktika__Lechenie_naturalnymi_kompleksnymi_metodami/Min_Inf_No7",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Важная дополнительная информация", Path = "Vazhnaya_dopolnitelnaya_informatsiya", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Миомы, Фибромиомы. Причины, последствия, профилактика, лечение натуральными комплексными методами.", Path = "Miomy,_Fibromiomy__Prichiny,_posledstviya,_profilaktika,_lechenie_naturalnymi_kompleksnymi_metodami_", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №7 Миомы, Фибромиомы. Причины. Последствия. Профилактика. Лечение натуральными комплексными методами/Мини инфопродукт №7/вебинар о причинах миом и не только (про овуляцию – циклы , миомы, кисты – примерно с 40 минуты)", CourseId = miniCourseIds[7], Path = "Min_Inf_No7_Miomy,_Fibromiomy__Prichiny__Posledstviya__Profilaktika__Lechenie_naturalnymi_kompleksnymi_metodami/Min_Inf_No7/vebinar_o_prichinakh_miom_i_ne_tolko_(pro_ovulyatsiyu_-_tsikly_,_miomy,_kisty_-_primerno_s_40_minuty)",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "вебинар о причинах миом и не только (про овуляцию – циклы , миомы, кисты – примерно с 40 минуты)", Path = "vebinar_o_prichinakh_miom_i_ne_tolko_(pro_ovulyatsiyu_-_tsikly_,_miomy,_kisty_-_primerno_s_40_minuty)", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №8 Папилломы. Кондиломы (вирус папилломатоза). Причины. Профилактика. Методики комплексного лечения. Полный, системный подход", CourseId = miniCourseIds[8], Path = "Min_Inf_No8_Papillomy__Kondilomy_(virus_papillomatoza)__Prichiny__Profilaktika__Metodiki_kompleksnogo_lecheniya__Polnyy,_sistemnyy_podkhod",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Папилломы. Кандиломы", Path = "Papillomy__Kandilomy", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №9 Болезнь Лайма – Клещевой Боррелиоз", CourseId = miniCourseIds[9], Path = "Min_Inf_No9_Bolezn_Layma_-_Kleshchevoy_Borrelioz",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1. Общие сведения, симптомы и осложнения", Path = "1__Obshchie_svedeniya,_simptomy_i_oslozhneniya", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2. Дополнения, рекомендации общие", Path = "2__Dopolneniya,_rekomendatsii_obshchie", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3. Лечение антибиотиками Поправки Предосторожности", Path = "3__Lechenie_antibiotikami_Popravki_Predostorozhnosti", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "4. Методики лечения Система 1 часть", Path = "4__Metodiki_lecheniya_Sistema_1_chast", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "5. Методики лечения, система 2 часть Корректировки и доп информация Методики одного доктора", Path = "5__Metodiki_lecheniya,_sistema_2_chast_Korrektirovki_i_dop_informatsiya_Metodiki_odnogo_doktora", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "6. По травам дополнительно  Астрагал особые свойства", Path = "6__Po_travam_dopolnitelno__Astragal_osobye_svoistva", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №10 Болезни полости Рта (от Кариеса до Парадонтита)", CourseId = miniCourseIds[10], Path = "Min_Inf_No10_Bolezni_polosti_Rta_(ot_Kariesa_do_Paradontita)",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1. Лечение зубов, профилактика и общие сведения", Path = "1__Lechenie_zubov,_profilaktika_i_obshchie_svedeniya", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2. Методики лечения Устранение З боли, как лакировать эмаль", Path = "2__Metodiki_lecheniya_Ustranenie_Z_boli,_kak_lakirovat_emal", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3. Парадонтит Гингиит, Глоссит, дополнительные методики, экстракты, масла, травы", Path = "3__Paradontit_Gingiit,_Glossit,_dopolnitelnye_metodiki,_ekstrakty,_masla,_travy", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №11 Угри. Угревая сыпь. Чёрные ( комедоны), Белые ( мелиумы) и Розовые ( розацея) угри. Причины и следствия. Лечение", CourseId = miniCourseIds[11], Path = "Min_Inf_No11_Ugri__Ugrevaya_syp__Chernye_(_komedony),_Belye_(_meliumy)_i_Rozovye_(_rozatseya)_ugri__Prichiny_i_sledstviya__Lechenie",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1. Введение, общие положения", Path = "1__Vvedenie,_obshchie_polozheniya", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2. Комедоны. Лечение чёрных угрей", Path = "2__Komedony__Lechenie_chernykh_ugrei", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3. Милиумы Лечение белых угрей", Path = "3__Miliumy_Lechenie_belykh_ugrei", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "4. Розацея Розовые угри", Path = "4__Rozatseya_Rozovye_ugri", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "5. Лечение угрей медикаментами и пара слов о кремах КК", Path = "5__Lechenie_ugrei_medikamentami_i_para_slov_o_kremakh_KK", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №12 Пигментные пятна — причины, лечение, очищение кожи", CourseId = miniCourseIds[12], Path = "Min_Inf_No12_Pigmentnye_pyatna_-_prichiny,_lechenie,_ochishchenie_kozhi",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "1. Вводная часть, общие методы важная часть системного подхода", Path = "1__Vvodnaya_chast,_obshchie_metody_vazhnaya_chast_sistemnogo_podkhoda", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "2. Лечение пигментных пятен, веснушек", Path = "2__Lechenie_pigmentnykh_pyaten,_vesnushek", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "3. Чистотел Сбор, своиства, мази, настоики, масло применения от А о Я", Path = "3__Chistotel_Sbor,_svoistva,_mazi,_nastoiki,_maslo_primeneniya_ot_A_o_Ya", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №13 Новый !!! Дополнительные методики комплексного лечения грибковых поражений, в том числе кожных заболеваний", CourseId = miniCourseIds[13], Path = "Min_Inf_No13_Novyy__Dopolnitelnye_metodiki_kompleksnogo_lecheniya_gribkovykh_porazheniy,_v_tom_chisle_kozhnykh_zabolevaniy",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "М®≠® ®≠дЃ ь 13. Га®°™ЃҐл• ѓЃа†¶•≠®п (™Ѓ¶≠л• Ґ вЃђ з®бЂ•) , Ђ•з•≠®•, ђ•вЃ§®з™†, з†бвм 1.", Path = "M(R)=(R)_(R)=dG__13__Ga(R)degTMGGl*_gGa+P*=(R)p_(TMGP=l*_G_vGdj_z(R)bDj*)_,_Dj*z*=(R)*,_dj*vGS(R)zTM+,_z+bvm_1_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "М®≠® ®≠дЃ ь 13. Га®°™ЃҐл• ѓЃа†¶•≠®п (™Ѓ¶≠л• Ґ вЃђ з®бЂ•) , Ђ•з•≠®•, ђ•вЃ§®з™†, з†бвм 2.", Path = "M(R)=(R)_(R)=dG__13__Ga(R)degTMGGl*_gGa+P*=(R)p_(TMGP=l*_G_vGdj_z(R)bDj*)_,_Dj*z*=(R)*,_dj*vGS(R)zTM+,_z+bvm_2_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "М®≠® ®≠дЃ ь 13. Га®°™ЃҐл• ѓЃа†¶•≠®п (™Ѓ¶≠л• Ґ вЃђ з®бЂ•) , Ђ•з•≠®•, ђ•вЃ§®з™†, з†бвм 3.", Path = "M(R)=(R)_(R)=dG__13__Ga(R)degTMGGl*_gGa+P*=(R)p_(TMGP=l*_G_vGdj_z(R)bDj*)_,_Dj*z*=(R)*,_dj*vGS(R)zTM+,_z+bvm_3_", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "М†£≠•І®п   Каг¶™† ЭбђЃае† + ѓа•І•≠в†ж®п Ѓ ђ®а• £а®°ЃҐ", Path = "M+L=*I(R)p___KagPTM+_EbdjGae+_+_ga*I*=v+zh(R)p_G_dj(R)a*_La(R)degGG", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №14 Методики лечения от Паразитов (Гельминтов) и Лямблий используя лечебные грибные препараты. Полная готовая методика. Дополнение к Инфопродукту № 5+6", CourseId = miniCourseIds[14], Path = "Min_Inf_No14_Metodiki_lecheniya_ot_Parazitov_(Gelmintov)_i_Lyambliy_ispolzuya_lechebnye_gribnye_preparaty__Polnaya_gotovaya_metodika__Dopolnenie_k_Infu_No_5+6",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "А≠в®ѓ†а†І®в†а≠†п ѓаЃ£а†ђђ† б ѓЃђЃймо Ђ•з•°≠ле £а®°≠ле ѓа•ѓ†а†вЃҐ", Path = "A=v(R)g+a+I(R)v+a=+p_gaGLa+djdj+_b_gGdjGymo_Dj*z*deg=le_La(R)deg=le_ga*g+a+vGG", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "М†£≠•І®п   Каг¶™† ЭбђЃае† + ѓа•І•≠в†ж®п Ѓ ђ®а• £а®°ЃҐ", Path = "M+L=*I(R)p___KagPTM+_EbdjGae+_+_ga*I*=v+zh(R)p_G_dj(R)a*_La(R)degGG", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №15 Мастопатия (кисты молочных желёз). Лечение с использованием грибных препаратов в общей комплексной методике лечения. Необходимое дополнение к Мини инфо продукту № 5", CourseId = miniCourseIds[15], Path = "Min_Inf_No15_Mastopatiya_(kisty_molochnykh_zhelez)__Lechenie_s_ispolzovaniem_gribnykh_preparatov_v_obshchey_kompleksnoy_metodike_lecheniya__Neobkhodimoe_dopolnenie_k_Mini_info_produktu_No_5",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Кисты Вступление", Path = "Kisty_Vstuplenie", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Кисты головного мозга", Path = "Kisty_golovnogo_mozga", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Кисты прочие", Path = "Kisty_prochie", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Магнезия   Кружка Эсморха + презентация о мире грибов", Path = "Magneziya___Kruzhka_Esmorkha_+_prezentatsiya_o_mire_gribov", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №16 Миомы. Лечение с использованием грибных препаратов в общей комплексной методике лечения. Необходимое дополнение к Мини инфо продукту № 7", CourseId = miniCourseIds[16], Path = "Min_Inf_No16_Miomy__Lechenie_s_ispolzovaniem_gribnykh_preparatov_v_obshchey_kompleksnoy_metodike_lecheniya__Neobkhodimoe_dopolnenie_k_Mini_info_produktu_No_7",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Копия Магнезия   Кружка Эсморха + презентация о мире грибов", Path = "Kopiya_Magneziya___Kruzhka_Esmorkha_+_prezentatsiya_o_mire_gribov", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Копия Миомы и Фибромиомы лечение комплексное грибными препаратами и травяными сборами в общей комплекс", Path = "Kopiya_Miomy_i_Fibromiomy_lechenie_kompleksnoe_gribnymi_preparatami_i_travyanymi_sborami_v_obshchey_kompleks", VideoTypeId = videoTypeIds[0] },
                    }
                },
                new Folder
                {
                    Id = folderId++, Name = "Мини инфопродукт №18 Сахарный диабет 1 и 2 типа. Причины, симптомы, осложнения, обследования, комплексные методики лечения", CourseId = miniCourseIds[18], Path = "Min_Inf_No18_Sakharnyy_diabet_1_i_2_tipa__Prichiny,_simptomy,_oslozhneniya,_obsledovaniya,_kompleksnye_metodiki_lecheniya",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Сахарный Диабет вступление и теория (1)", Path = "Sakharnyy_Diabet_vstuplenie_i_teoriya_(1)", VideoTypeId = videoTypeIds[0] },
                        new Video { Id = videoId++, Name = "Сахарный диабот методики лечения (1)", Path = "Sakharnyy_diabot_metodiki_lecheniya_(1)", VideoTypeId = videoTypeIds[0] },
                    }
                },
            };


            return frolovFolders;
        }
    }
}
