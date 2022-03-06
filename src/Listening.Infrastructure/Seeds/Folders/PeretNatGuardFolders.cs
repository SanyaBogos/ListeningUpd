using Listening.Core.Entities.Specialized.Knowledge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Listening.Infrastructure.Seeds.Folders
{
    public class PeretNatGuardFolders
    {
        public static Folder[] GetFolders(int folderId, int videoId, int videoTypeId, int autorId)
        {
            var peretyatFolders = new Folder[]
            {
                new Folder
                {
                    Id = folderId++, Name = "Цели, задачи ваши и доходного лесосада", CourseId = autorId, Path = "Tseli,_zadachi_vashi_i_dokhodnogo_lesosada",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Цели и задачи своего лесосада", Path = "Tseli_i_zadachi_svoego_lesosada", VideoTypeId = videoTypeId,
                            Description =  @"Схема ""цветок пермакультуры"" позволяет мысленно охватить направления деятельности в своем селении РП, экопоселении, пермакультурной ферме. Внимательное изучение поможет лучше осознать свои цели.

Домашнее задание: 
1. Определить свои цели
2. Поставить задачи для достижения целей. 
" },
                        new Video { Id = videoId++, Name = "Доходный лесосад и колодное пчеловодство", Path = "Dokhodnyy_lesosad_i_kolodnoe_pchelovodstvo", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Критическая ситуация в Татарстане- вместо полноводных рек - лужи, нерест рыбы - под угрозой.", Path = "Kriticheskaya_situatsiya_v_Tatarstane-_vmesto_polnovodnykh_rek_-_luzhi,_nerest_ryby_-_pod_ugrozoy.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "В Калмыкии остро стоит проблема опустынивания земель", Path = "V_Kalmykii_ostro_stoit_problema_opustynivaniya_zemel", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Кедровые леса региона под угрозой гибели", Path = "Kedrovye_lesa_regiona_pod_ugrozoy_gibeli", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Юридические основы землепользования (Ореховый лесосад)", CourseId = autorId, Path = "Yuridicheskie_osnovy_zemlepol'zovaniya_(Orekhovyy_lesosad)",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Урок 2 Юридические основы землепользования (Ореховый лесосад)", Path = "Urok_2_Yuridicheskie_osnovy_zemlepolzovaniya_(Orekhovyy_lesosad)", VideoTypeId = videoTypeId,
                            Description = @"Налоговый кодекс РФ (НК РФ) ст 217 (см п. 13) <a href=""http://www.consultant.ru/document/cons_doc_LAW_28165/625f7f7ad302ab285fe87457521eb265c7dbee3c/""> тут </a>

Я не говорил про федеральную программу самозанятости населения <a href=""http://posobie-help.ru/subsidii/business/programma-samozanyatosti-naseleniya.html""> тут </a> , т.к сам ею не пользовался, но тем не менее у нас в хуторе многие пользовались этой программой и получали (в то время 60 т.р на развитие своего дела). Составляли упрощенный бизнес план, отчитывались о трате средств, а потом писали объяснительную записку, что ничего не получилось, дабы не возвращать средства государству. Это что касается ЛПХ и ИЖС.

А вот для КФХ есть ФЗ <a href=""http://www.consultant.ru/document/Cons_doc_LAW_52144/""> тут </a> ""О развитии малого и среднего предпринимательства в Российской Федерации""

112-ФЗ о ЛПХ - Приусадебное ЛПХ в совокупности с полевым ЛПХ - это один из лучших способов оформления физ. лицами земельного участка под поместье, усадьбу, лесосад <a href=""http://www.consultant.ru/document/cons_doc_LAW_43127/""> тут </a>

 

Да, в этом уроке я не затрагиваю земли лесного фонда. Т.к на мой взгляд земли с/х назначения в совокупности с землями поселений более приемлемы под создание пермакультурной фермы, Родового поместья, доходного лесосада и лесного фермерства. А потому обходил лесной фонд стороной (да у нас в Волгоградской и не много земель этой категории). Но в областях, где леса много, а с/х земель мало (например Сибирь или север европейской части РФ) стоит рассмотреть и этот вариант.
Так земли лесного фонда, можно использовать и земли лесного фонда для создания лесосадов и даже для возведения хоз построек при наличии ПЗЗ в градостроительном плане (теоретически даже дома для круглогодичного проживания на оформленном КФХ на землях лесного фонда).
Прикрепляю файл от известного юриста Василия Петрова в котором вы найдете

отыскание свободной земли на землях лесного фонда;
самостоятельное проектирование лесных участков;
камеральное межевание;
оформление земель лесного фонда в аренду или безвозмездное пользование для ведения сельского хозяйства;
согласование предоставления лесного участка с районным лесничеством;
заключение договора с областным Департаментом лесного хозяйства;
возведение временных и капитальных строений на лесной земле;
научное и государственное значение семейных форм лесопользования для развития аграрно-лесного сектора народного хозяйства.

Из недостатков этого метода. Аренда до 49 лет. Т.к согласно ЛК РФ земли лесного фонда являются достоянием государства, то их нельзя оформлять в собственность. К сожалению пока в нашей стране так. Можно брать в аренду на выборочную или сплошную рубку, но нельзя взять в собственность для выращивания редких и ценных видов. Только в аренду. И даже вырастив таким образом ценный лес (про это будет отдельный урок) или особо защитный участок леса ОЗУ, мы не сможем его официально выделить и придать такой особый статус с наложением природоохранного режима, т.к после окончании срока аренды, государство не сможет ее продлить на ОЗУ или ценном лесе и просто заберет его себе.

Как взять в аренду лесной участок под пермакультурный лесосад (1).pdf"
                        },
                        new Video { Id = videoId++, Name = "Третий вебинар марафона- юридические основы землепользования", Path = "Tretiy_vebinar_marafona-_yuridicheskie_osnovy_zemlepolzovaniya", VideoTypeId = videoTypeId,
                            Description = @"Вебинар о аспектах поиска и оформления земельного участка.
В конце вебинара есть интересные примеры поиска земли под поместье по публичной кадастровой карте."
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Субсидия на ЛПХ и КФХ от государства", CourseId = autorId, Path = "Subsidiya_na_LPKh_i_KFKh_ot_gosudarstva",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Лесосад от государства", Path = "Lesosad_ot_gosudarstva", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Пермакультурный сад Собковиака", Path = "Permakulturnyy_sad_Sobkoviaka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Пермакультурный сад Собковиака - 2", Path = "Permakulturnyy_sad_Sobkoviaka_-_2", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Бизнес план на лесосад для комиссии", CourseId = autorId, Path = "Biznes_plan_na_lesosad_dlya_komissii",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Лесосад от государства -2 (бизнес-план для комиссии)", Path = "Lesosad_ot_gosudarstva_-2_(biznes-plan_dlya_komissii)", VideoTypeId = videoTypeId,
                            Description = @"Статья Н.Ф.Киктенко <a href=""http://www.vedomosti.md/news/faleshtskoe-nou-hau-orehi-v-odnom-stroe-so-slivoj-smorodinoj""> тут </a>

Статья Павла Тулбы <a href=""https://agrostory.com/info-centre/market-news/the-garden-of-nuts-as-from-one-area-to-collect-three-types-of-crop/""> тут </a>"
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Климат", CourseId = autorId, Path = "Klimat",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Как найти подходящее место для ореховодства", Path = "Kak_nayti_podkhodyashchee_mesto_dlya_orekhovodstva", VideoTypeId = videoTypeId,
                            Description = @"Я в слайде использовал не совсем точную карту по зонам морозостойкости, даже несмотря на грядущее потепление климата в нашей стране, предлагаю все же руководствоваться старыми картами
Домашнее задание: 
1. Определить климатические параметры для своего ЗУ (города): САТ, зона м/с, БМП, ИСИ, ГТК.
2. Определить тип почвы в месте своего проживания.
3. Описать рельеф местности, сделать скан из топокарты
"
                        },
                        new Video { Id = videoId++, Name = "Лотон - список рекомендаций при покупке земли", Path = "Loton_-_spisok_rekomendatsiy_pri_pokupke_zemli", VideoTypeId = videoTypeId,
                            Description = @"1. Агроэкологический атлас (забирайте себе ссылку) <a href=""http://www.agroatlas.ru/ru/""> тут </a>
2. Глава ""Хорошее место для сада"" из книги Курдюмова и Железова ""Умный сад. Как перехитрить климат""
<a href=""http://sadisibiri.ru/klimat-perehitrim-3.html""> тут </a>
3. Известный фильм ""Список рекомендаций при покупке земли от Джефа Лотона"" "
                        },
                        new Video { Id = videoId++, Name = "Как создать папку в Облаке и добавить в нее файлы_", Path = "Kak_sozdat_papku_v_Oblake_i_dobavit_v_nee_fayly_", VideoTypeId = videoTypeId,
                            Description = @"Напоминаю, что лучше вести конспект по домашним заданиям (ДЗ), иначе информация будет усваиваться с трудом. ДЗ можно выкладывать на облаке, Яндекс-диске и присылать ссылку. Все ДЗ для удобства должны располагаться по разным папкам, но так чтобы можно было открыть нужное ДЗ. Например посмотреть ваши климатические параметры в одном ДЗ и соотнести их с выбранными вами породами в другом ДЗ которое тут же. Или соответствие спецификации пород к плану аллейного лесосада с бизнес расчетами по семеноводству.
Инструкция, как создавать папки в облаке на <a href=""mail.ru""> тут </a> "
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Особенности земельного участка", CourseId = autorId, Path = "Osobennosti_zemel'nogo_uchastka",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Как найти подходящее место для ореховодства, особенности ЗУ", Path = "Kak_nayti_podkhodyashchee_mesto_dlya_orekhovodstva,_osobennosti_ZU", VideoTypeId = videoTypeId,
                            Description = @"Ищите центр агрохимической службы своем регионе. Пример центра агрохимической службы - ЦАС ""Волгоградский"". <a href=""http://агрохим34.рф""> тут </a>

Один из сайтов (самых первых) с публичной кадастровой картой. Можно найти свою область, район и посмотреть на наличие свободных земель, занятых земель. А также кадастровую стоимость ЗУ, площадь, адрес и т.д. <a href=""http://pkk5.rosreestr.ru/#x=11554711.454933215&y=10055441.599232892&z=3""> тут </a>

Справочник по проращиванию семян <a href=""http://flower.onego.ru/agro/seeds01.html""> тут </a> 

Домашнее задание:
1.Научится пользоваться картой Росреестра (сделать скрин своего ЗУ, места проживания в городе)
2. Найти сайт областного центра агрохимической службы ЦАС.
3. Найти сайт районной администрации (в предполагаемом месте оформления ЗУ)"
                        },
                        new Video { Id = videoId++, Name = "Кадастровая карта", Path = "Kadastrovaya_karta", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Видеоинструкция- Публичная кадастровая карта", Path = "Videoinstruktsiya-_Publichnaya_kadastrovaya_karta", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Элементы дизайна", CourseId = autorId, Path = "Elementy_dizayna",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Создание устойчивой экосистемы", Path = "Sozdanie_ustoychivoy_ekosistemy", VideoTypeId = videoTypeId,
                            Description = @"Тороплюсь быстрее дать вам список ресурсов для заказа семян
1. Сайт рекомендованный Гусманом Минлебаевым для заказа семян:
http://www.lawyernursery.com/index.php
2. Сайт Энри Гримо Канада: http://www.grimonut.com
3. Фирма Шеффилдс: https://sheffields.com
4. Российский сайт ООО «Агбина» http://www.agbina.com
если заказываете в Агбине обязательно задавайте вопрос в форме
обратной связи, о дате и места сбора семян. Заказывайте только
свежие семена с максимальной всхожестью. Менеджеры фирмы
всегда отвечают на вопросы.
5. Сайт А.Сидоркина: http://www.treespk.ru
6. Народный семенной фонд: https://vk.com/semenaroda
7. http://www.ebay.com
8. https://ru.aliexpress.com
9. http://www.nuttrees.com/edible-nut-trees/other-edible..
10. https://www.forestag.com

Сейчас ноябрь, а это значит только начался сезон закупки семян и время на стратификацию есть. Даже на многоступечатую. Азотофиксирующие породы из семейства бобовых можно закупать семенами и весной, т.к они не нуждаются в стратификации, а нуждаются в скарификации, главным образом термической (крупные семена в механической).

 

Предлагаю ознакомиться с Федеральным законом N 101-ФЗ
""О государственном регулировании обеспечения плодородия земель сельскохозяйственного назначения""
Все валоканавы, валоканалы, арыки, пруды мы может причислить к мелиоративным мероприятиям
Статья 1 Основные понятия http://base.garant.ru/12112328/1/#block_103
Статья 7 Права собственников и арендаторов ЗУ http://base.garant.ru/12112328/3/#block_300

Домашнее задание: 
Предварительно подобрать нужные для Вашего климата, почв, рельефа и задач, элементы пермакультурного дизайна в геопластике. Если конечно они необходимы и вы примете решение о дизайне в геопластике."
                        },
                        new Video { Id = videoId++, Name = "Кристально ЧИСТЫИ? ПРУД.", Path = "Kristalno_ChISTYI_PRUD.", VideoTypeId = videoTypeId,
                            Description = @"Секрет по поддержанию чистоты воды в пруду природным методом"
                        },
                        new Video { Id = videoId++, Name = "NREGA-MP--Watershed Devlopment", Path = "NREGA-MP--Watershed_Devlopment", VideoTypeId = videoTypeId,
                            Description = @"Учебный, индийский фильм на английском языке по созданию влагоудерживающего ландшафта каменных мини-плотин в сухом климате."
                        },
                        new Video { Id = videoId++, Name = "Валоканавы в саду после дождя", Path = "Valokanavy_v_sadu_posle_dozhdya", VideoTypeId = videoTypeId,
                            Description = @"Валоканавы активно уже используются российскими пермакультурными дизайнерами (напомню, там где ГТК меньше 1)"
                        },
                        new Video { Id = videoId++, Name = "Пермакультурныи? рыбныи? пруд", Path = "Permakulturnyi_rybnyi_prud", VideoTypeId = videoTypeId,
                            Description = @"Также предлагаю (кто еще не посмотрел) ряд известных фильмов:
- один квадратный метр поверхности хорошего пруда будет более продуктивен. чем один квадратный метр лесосада. Идеальный пермакультурный пруд и аквакультура"
                        },
                        new Video { Id = videoId++, Name = "Строительство дамбы ключевой точки", Path = "Stroitelstvo_damby_klyuchevoy_tochki", VideoTypeId = videoTypeId,
                            Description = @"Из данного фильма вы узнаете, как построить дамбу ключевой точки. Ключевая линия – это система проектирования, разработанная П.А. Йомансом в Австралии в 1940-50-х годах. Эта система направлена сбор дождевой воды и хранение в системе объединённых прудов и валоканав"
                        },
                        new Video { Id = videoId++, Name = "Земляные работы по всему миру с Джефом Лотоном", Path = "Zemlyanye_raboty_po_vsemu_miru_s_Dzhefom_Lotonom", VideoTypeId = videoTypeId,
                            Description = @"Различные виды земляных работ (валоканавы, дамбы, уровневые переливы дамб и т.д.)"
                        },
                        new Video { Id = videoId++, Name = "Пермакультурные водные системы с ключевои? линиеи?", Path = "Permakulturnye_vodnye_sistemy_s_klyuchevoi_liniei", VideoTypeId = videoTypeId,
                            Description = @"Водоудерживающий дизайн в засушливом климате как проект ключевой линии состоящей из валоканав и прудов часто используется пермакультурными дизайнерами. Например ферма Севен Сидс была спроектирована на основе пермакультурных принципов и использования ключевой линии. Здесь мы увидим водную систему сверху до низу и узнаем о её благоприятном воздействии на окружающую территорию В прошлых занятиях мы рассмотрели дизайн- проект с ключевой линией состоящей валоканав и прудов. Подобная схема часто используется пермакультурными дизайнерами. Например ферма Севен Сидс была спроектирована на основе пермакультурных принципов и использования ключевой линии. Здесь мы увидим водную систему сверху до низу и узнаем о её благоприятном воздействии на окружающую территорию"
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Частные случаи дизайна", CourseId = autorId, Path = "Chastnye_sluchai_dizayna",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Особенности дизайна доходного лесосада", Path = "Osobennosti_dizayna_dokhodnogo_lesosada", VideoTypeId = videoTypeId,
                            Description = @"Посезонное наличие и направление ветров необходимо учитывать в вашем регионе для проектирования аллей. Так если в зимние и ране-весенние месяца (когда зимостойкость растения минимальна, после накопленных усталостей непогоды) есть наличие северных ветров, совпадающих с направлением аллей, то нужно ввести коррективы:

- уменьшить длину аллей разбив площадь лесосада ветрозащитными лесополосами (из продуктивных, сопутствующих пород) при площади более 3 га.
- запланировать изогнутые аллеи.
- использовать защищенные от этих ветров места (как мой ботсад).

Построение розы ветров для городов России http://stroydocs.com/info/e_veter
Нужно ввести свой город (ближайший город к селению или предполагаемому месту под лесосад) и нажать кнопку построить.
Желательно конечно присутствовать на месте в разные сезоны, дабы теория не расходилась с практикой. Но в период обучения пока теория.

 

Термины, основные понятия. Обратите внимание на агротехнические и мелиоративные мероприятия, это как раз элементы дизайна в геопластике и пермакультурные методы обработки почв.
http://base.garant.ru/12112328/1cafb24d049dcd1e7707a22d98e9858f/#block_103
N 101-ФЗ ""О государственном регулировании обеспечения плодородия земель сельскохозяйственного назначения""

Домашнее задание: 
1.Предварительно показать на планировке лесосада аллеи и слой водного плана (гряды, валоканавы и т.д в зависимости от ГТК местности, уровня залегания грунтовых вод, рельефа, пород ДКР) 
2. Возможен вариант без затратных земляных работ (ровный рельеф, приемлемый уровень грунтовых вод, соответствие ГТК местности породам)
"
                        },
                        new Video { Id = videoId++, Name = "Паханные валоканавы в моем дендрарии", Path = "Pakhannye_valokanavy_v_moem_dendrarii", VideoTypeId = videoTypeId,
                            Description = @"Валоканавы в моем ботаническом лесосаду"
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Микроклимат ЗУ", CourseId = autorId, Path = "Mikroklimat_ZU",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Микроклимат ЗУ (роза ветров)", Path = "Mikroklimat_ZU_(roza_vetrov)", VideoTypeId = videoTypeId,
                            Description = @"Рекомендую статью ""А почва продолжает умнеть"" из книги Умный сад в подробностях Н.Курдюмова http://sadisibiri.ru/kurdumov-umn-sad-podrobno-8.html Живая и богатая почва снизит эффект аллелопатии -подавления одних видов другими. 
                            
Домашнее задание: 
1. Предварительно применить элементы создания микроклиматических зон на планировке орехового лесосада. 
2. Подобрать породы в ветрозащитную изгородь и найти способ их проращивания (черенкование тополей, ив, посев берез, можжевельника, сосен и т.д) 
"
                        },
                        new Video { Id = videoId++, Name = "Мэтт Килби. Человек, посадивший тысячи деревьев", Path = "Mett_Kilbi._Chelovek,_posadivshiy_tysyachi_derevev", VideoTypeId = videoTypeId,
                            Description = @"Пример создания микроклимата (по безветрию и притенению) -укрытия сеянцев и саженцев в ветреной степной зоне.
Познавательная обзорная статья 5 прибыльных идей выращивания в теплице https://abcbiznes.ru/biznes-idei/681-5-pribylnyh-idey-vyraschivaniya-v-teplice.html"
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Ореховодство и виды пород", CourseId = autorId, Path = "Orekhovodstvo_i_vidy_porod",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Ореховодство и виды пород", Path = "Orekhovodstvo_i_vidy_porod", VideoTypeId = videoTypeId,
                            Description = @"Новый расширенный вебинарный урок по обзору орехоплодных пород с сосной Культера, тореей орехоносной, лапинами и т.д
Домашнее задание:
1. Начать составление бизнес-плана. Описать свои цели, входные данные ЗУ, перспективы ореховодства, преимущества данного направления. 
2. Определить нужность хищника в лесосаду (аллейном ореховом лесосаде) в зависимости от внешних условий и ваших пожеланий. 
"
                        },
                        new Video { Id = videoId++, Name = "обзор ореховых", Path = "obzor_orekhovykh", VideoTypeId = videoTypeId,
                            Description = @"Параллельно нашему курсу уже сейчас нужно начать работу по созданию своего паблика (группы в соц сетях: ВКонтакте, Одноклассниках, Фейсбуке, Инстаграм). Это абсолютно бесплатно. И уверяю вас, это вам под силу. В качестве примера покажу паблик ВК молодой девушки, которая этой весной переехала из Волгограда в маленький хутор Заплавка (рядом с нами). Она просмотрела половину курса ""Богатое поместье"" у меня в хуторе, в течении 3х дней. Я отвечал на все ее вопросы. Далее она просто копировала мой паблик ВК. Интернет ей провели в середины лета и с осени она активно взялась за маркетинг и сбыт, смотрите сами https://vk.com/exzosad365
Такой паблик станет вашим электронным магазином, где вы сможете выкладывать на витрину свои товары. И конечно рассказывать о своих новостях на земле. Сначала нужно будет пригласить в группу всех своих друзей и знакомых из города (это ваши потенциальные первые покупатели), далее пойдут лайки и репосты, вы проведете несколько акций для привлечения количества подписчиков. И через год у вас будет свой магазинчик, который также нужно будет продолжать развивать. Название может быть самым разным, например от банального ЛПХ Сидорова до лесосад нашей мечты."
                         },
                        new Video { Id = videoId++, Name = "Домашний Ореховый Бизнес.", Path = "Domashniy_Orekhovyy_Biznes.", VideoTypeId = videoTypeId,
                            Description = @"И уже сейчас можно начинать делать свой первый товар. Даже можно не дожидаться урожая ореховых пород. Покупаете орех, конечно желательно у дачников, т.е экологически чистый, деревенское масло, какао, и мед (если колодный, это еще больше бренда). И вы можете делать свой урбеч. Орех очищаете 
можно конечно продавать и очищенное ядро, но можно и пойти далее (про это еще будут уроки) и сделать урбеч. Упаковка стандартная - стеклотара 100 граммовая и винтовая крышка. Цену нужно ориентировать на Nutella Это одна из дорогих паст, а поскольку она в экологичности уступает, то не должна быть дороже. Получается 160 руб за 100 грамм или 1600 руб за кг вашего продукта. "
                         },
                        new Video { Id = videoId++, Name = "ОРЕХОВЫЙ БИЗНЕС.  ЗАРАБОТАТЬ МИЛЛИОН.  С чего начать.", Path = "OREKhOVYY_BIZNES.__ZARABOTAT_MILLION.__S_chego_nachat.", VideoTypeId = videoTypeId,
                            Description = @"Для очистки ореха на ядро (если заниматься этим) то подойдет такой приспособление: 
Если у кого нет своего электронного магазина, то уже начинаем его делать, а параллельно используем для продаж договоренности с блогерами, т.е владельцами крупных пабликов. Я и сам пользуюсь услугами большими чем моя группа, для реализации семян и плачу за это 20% от продаж. Считается, что 30% оплаты это нормально. Второй способ оплаты рекламы - репост вашей записи в очень крупные паблики с сотнями тысяч подписчиков. Такой репост стоит 400 руб и обычно окупается продажей товара в разы, к тому же с каждым таким репостом растет и число ваших подписчиков -потенциальных покупателей."
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Виды азотофиксаторов", CourseId = autorId, Path = "Vidy_azotofiksatorov",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Обзор азотофиксаторов", Path = "Obzor_azotofiksatorov", VideoTypeId = videoTypeId,
                            Description = @"Обзор азотофиксирующих пород. для создания орехового лесосада обязательно потребуется наличие в саду азотофиксаторов"
                        },
                        new Video { Id = videoId++, Name = "Азотобактер", Path = "Azotobakter", VideoTypeId = videoTypeId,
                            Description = @"Азотоба́ктер — род бактерий, живущих в почве и способных в результате процесса азотфиксации переводить газообразный азот в растворимую форму, доступную для усваивания растениями.Род азотобактер принадлежит к грамотрицательным бактериям и входит в группу так называемых свободноживущих азотфиксаторов.Представители рода обитают в нейтральных и щелочных почвах, воде и в ассоциации с некоторыми растениями.Образуют особые покоящиеся формы — цисты."
                        },
                        new Video { Id = videoId++, Name = "Семейство Бобовые. Видеоурок по биологии 6 класс", Path = "Semeystvo_Bobovye._Videourok_po_biologii_6_klass", VideoTypeId = videoTypeId,
                            Description = @"Видео урок биологии о семействе бобовых"
                        },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Переработка ореха. Урбеч, масло, мука. 3 урока", CourseId = autorId, Path = "Pererabotka_orekha._Urbech,_maslo,_muka._3_uroka",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Свое дело - ЛПХ (масло, мука, урбеч)", Path = "Svoe_delo_-_LPKh_(maslo,_muka,_urbech)", VideoTypeId = videoTypeId,
                            Description = @"На фото товары в вашей группе в социальных сетях. В данном случае скриншот из ВКонтакте. Орех из вашего лесосада, деревенское домашнее масло, свой колодный мед... из таких брендовых ингредиентов может состоять ваш товар. И потребитель этого товара втягивается своим финансированием в ваше ЛПХ, в его развитие и приумножение. Т.е возможен такой рекламный слоган: покупая нашу продукцию, вы финансируете создание лесосадов/оживление планеты/возрождения первозданного облика планеты"
                        },
                        new Video { Id = videoId++, Name = "Урбеч Мельница Амбо - новый дагестанский бренд", Path = "Urbech_Melnitsa_Ambo_-_novyy_dagestanskiy_brend", VideoTypeId = videoTypeId,
                            Description = @"Вторичная переработка ореха -ореховые пасты, варенья, урбеч и т.д.. Возможно изготовление сырого орехового урбеча и обжаренного. Это инновационное направление для своего семейного дела. Лесосад позволит расширить ассортимент продукции по различным видам орехов. А вид использования ЛПХ позволит продавать данный товар без сертификации и налогообложения. Один из перспективных видов продаж со своего приусадебного (подсобного) хозяйства, продажи в электронном магазине. "
                        },
                        new Video { Id = videoId++, Name = "Изготовление и реализация масла и муки из семян льна, тыквы и подсолнуха.", Path = "Izgotovlenie_i_realizatsiya_masla_i_muki_iz_semyan_lna,_tykvy_i_podsolnukha.", VideoTypeId = videoTypeId,
                            Description = @"Пример работы сельского предпринимательства по изготовлению урбеча в Дагестане "

                        },
                        new Video { Id = videoId++, Name = "Бизнес идея изготовления и реализации ореховых паст", Path = "Biznes_ideya_izgotovleniya_i_realizatsii_orekhovykh_past", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Переработка ореха. Орехоколы, пресса", CourseId = autorId, Path = "Pererabotka_orekha._Orekhokoly,_pressa",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Маслопресс _Добрыня-30_", Path = "Maslopress__Dobrynya-30_", VideoTypeId = videoTypeId,
                            Description = @"Бизнес идея: масленый пресс с гидроцилиндром и дубовым бочонком ус. 30 т ""Добрыня -30"" https://maslopress-tlt.tiu.ru/p226791380-maslopress-dobrynya.html + электромельница с каменными жерновами из кварцита https://vk.com/market-124463517?w=product-124463517_1112613%2Fquery + 1 станичный сайт (паблик ВК, ОК , ФБ, инстаграмм, канал на ютубе) + упаковка масла, муки, урбеча"

                         },
                        new Video { Id = videoId++, Name = "Первый в России обзор на голландский РУЧНОЙ маслопресс Piteba. Учимся отжимать масло", Path = "Pervyy_v_Rossii_obzor_na_gollandskiy_RUChNOY_maslopress_Piteba._Uchimsya_otzhimat_maslo", VideoTypeId = videoTypeId,
                            Description = @"Различные маслопрессы"

                         },
                        new Video { Id = videoId++, Name = "Пресс-боченки для холодного отжима масла", Path = "Press-bochenki_dlya_kholodnogo_otzhima_masla", VideoTypeId = videoTypeId,
                            Description = @"Маслопресс ручной. Эконом вариант.
Может отжимать масло и горячего, и холодного отжима, в зависимости от использования свечи для подогрева. Данный пресс имеет производительность 1 литр +-0,5 л в час. Что больше подходит для собственных нужд или небольших продаж с ЗУ площадью 1-2 га.
Недостаток есть соприкосновение масла и металла.
Знакомый ореховод из Удмуртии Владимир Усатов использует ручной гидравлический пресс для отжима масла. Он мучается на 15 тоннике (пресс с бутылочным гидродомкратом) Вот ссылочка на его сайт http://oreh-udm.ru обратите внимание, под перечнем саженцев, есть виды его масел. В слайдах я использовал фотографию его масла, что он присылал.
Обратите внимание, он пищет ""В межсезонье мы традиционно переключаемся на другой вид деятельности - ремесленное производство нерафинированных масел холодного отжима на ручном гидравлическом дубовом прессе"" Т.е один вид деятельности -питомниководство, сменяется другим, производством масла/жмыха. Страничка Владимира ВКонтакте"

                         },
                        new Video { Id = videoId++, Name = "Маслопресс ПШУ 4 Маслячок", Path = "Maslopress_PShU_4_Maslyachok", VideoTypeId = videoTypeId,
                            Description = @"Пресса из г.Павлово НН область https://www.wooden-market.ru/?yclid=991566537407338106
Цена приличные. Сами комплектующие (даже с заказом их дубового бочонка) могут обойтись покупателю в половинную стоимость. Например: https://fortol.ru/shop/UID_27088.html плюс https://fortol.ru/shop/UID_27117.html плюс бочонок = 20200 Если собрать раму самостоятельно, можно реально удешевить старт. При сборе рамы следует выдержать плоскостность места установки бочонка."
                         },
                        new Video { Id = videoId++, Name = "Маслопресс ПШУ- 4 'Маслячок' Масло из орехов", Path = "Maslopress_PShU-_4_Maslyachok_Maslo_iz_orekhov", VideoTypeId = videoTypeId,
                            Description = @"Шнековый пресс. Самый экономный вариант ПШУ-4 Всем радует, производительность хорошая, работает уже 2 года. Отжимает даже орех (грецкий, фундук) и кунжут, но долго мы подбирали режимы, пока не нашли это видео)"
                        },
                        new Video { Id = videoId++, Name = "Орехокол промышленный Эксперт. Основа орехового бизнеса.", Path = "Orekhokol_promyshlennyy_Ekspert._Osnova_orekhovogo_biznesa.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Сравнение механической и промышленной очистки грецкого ореха", Path = "Sravnenie_mekhanicheskoy_i_promyshlennoy_ochistki_gretskogo_orekha", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Как выбрать промышленный орехокол Конусный, валковый, барабанный принцип раскола ореха. 3D модели.", Path = "Kak_vybrat_promyshlennyy_orekhokol_Konusnyy,_valkovyy,_barabannyy_printsip_raskola_orekha._3D_modeli.", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Переработка ореха. Калибраторы. Мельницы", CourseId = autorId, Path = "Pererabotka_orekha._Kalibratory._Mel'nitsy",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Калибратор для фундука. Сортировка по размеру лесного ореха. Hazelnut sizing.", Path = "Kalibrator_dlya_funduka._Sortirovka_po_razmeru_lesnogo_orekha._Hazelnut_sizing.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Универсальный Калибратор грецкого ореха 3 в 1. Сортировка миндаля и фундука.", Path = "Universalnyy_Kalibrator_gretskogo_orekha_3_v_1._Sortirovka_mindalya_i_funduka.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Электрическая жерновая мельница для муки (50 кг. в час)", Path = "Elektricheskaya_zhernovaya_melnitsa_dlya_muki_(50_kg._v_chas)", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Мельница для арахисовой пасты +7(909)429-25-38", Path = "Melnitsa_dlya_arakhisovoy_pasty_+7(909)429-25-38", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Электромельня _Мелёна 220_", Path = "Elektromelnya__Melyona_220_", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Ореховодство. Размножение. Черный орех", CourseId = autorId, Path = "Orekhovodstvo._Razmnozhenie._Chernyy_orekh",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Черный орех ЛО с обработкой_", Path = "Chernyy_orekh_LO_s_obrabotkoy_", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Семенные плантации ореха", Path = "Semennye_plantatsii_orekha", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "How to shell black wallnuts", Path = "How_to_shell_black_wallnuts", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Орехокол Универсальный - лущение грецкого ореха и фундука.", Path = "Orekhokol_Universalnyy_-_lushchenie_gretskogo_orekha_i_funduka.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Black Walnut Harvesting_  From Start to Finish", Path = "Black_Walnut_Harvesting___From_Start_to_Finish", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Черный, серый, сердцевидный, ланкастерский орехи", CourseId = autorId, Path = "Chernyy,_seryy,_serdtsevidnyy,_lankasterskiy_orekhi",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Черный, серый, сердцевидный, ланкастерский орехи", Path = "Chernyy,_seryy,_serdtsevidnyy,_lankasterskiy_orekhi", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Черный орех в средней полосе", Path = "Chernyy_orekh_v_sredney_polose", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Как приготовить домашнюю халву.", Path = "Kak_prigotovit_domashnyuyu_khalvu.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Ореховая халва по-восточному.", Path = "Orekhovaya_khalva_po-vostochnomu.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Ореховая отрасль в мире.", Path = "Orekhovaya_otrasl_v_mire.", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Грецкий орех", CourseId = autorId, Path = "Gretskiy_orekh",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Грецкий орех", Path = "Gretskiy_orekh", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Феноменальная находка Орех с обильным латеральным плодоношением в Брянской области  Россия  Сентябрь", Path = "Fenomenalnaya_nakhodka_Orekh_s_obilnym_lateralnym_plodonosheniem_v_Bryanskoy_oblasti__Rossiya__Sentyabr", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Прививка ореха", Path = "Privivka_orekha", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Поликультурный обзор схем орехового сада  Н.Ф.Киктенко", Path = "Polikulturnyy_obzor_skhem_orekhovogo_sada__N.F.Kiktenko", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Кария, чекалкин орех", CourseId = autorId, Path = "Kariya,_chekalkin_orekh",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Урок 12 1 Кария  (с обработкой)", Path = "Urok_12_1_Kariya__(s_obrabotkoy)", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Harvesting and preparing Hickory Nuts for Long term storage.", Path = "Harvesting_and_preparing_Hickory_Nuts_for_Long_term_storage.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "ВКУСНЫЙ ОРЕХ КАРИЯ!", Path = "VKUSNYY_OREKh_KARIYa", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Чекалкин орех рябинолистный Xanthoceras Sorbifolium", Path = "Chekalkin_orekh_ryabinolistnyy_Xanthoceras_Sorbifolium", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Лещина", CourseId = autorId, Path = "Leshchina",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Урок 12-2 Лещина (с обработкой)", Path = "Urok_12-2_Leshchina_(s_obrabotkoy)", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Орехокол Оптима 2 колка лесного ореха, фундука. Hazelnut cracking machine Optima 2", Path = "Orekhokol_Optima_2_kolka_lesnogo_orekha,_funduka._Hazelnut_cracking_machine_Optima_2", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Фундук, как сеять", Path = "Funduk,_kak_seyat", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Поликультурный сад", Path = "Polikulturnyy_sad", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Семейство буковых", CourseId = autorId, Path = "Semeystvo_bukovykh",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Семейство буковых", Path = "Semeystvo_bukovykh", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Eillert C25 - Abrasive Peeling Machine", Path = "Eillert_C25_-_Abrasive_Peeling_Machine", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Клекачка", CourseId = autorId, Path = "Klekachka",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "клекачка", Path = "klekachka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Клекачка перистая - и цветы и орешки съедобны!", Path = "Klekachka_peristaya_-_i_tsvety_i_oreshki_sedobny", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "высадка клекачки", Path = "vysadka_klekachki", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "высадка клекачки", Path = "vysadka_klekachki", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Фисташка и миндаль", CourseId = autorId, Path = "Fistashka_i_mindal'",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Фисташка и миндаль", Path = "Fistashka_i_mindal", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Миндаль <Розовая пена> от НПО <Сады России>. Саженцы почтой!", Path = "Mindal_Rozovaya_pena_ot_NPO_Sady_Rossii._Sazhentsy_pochtoy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Видео о фисташке", Path = "Video_o_fistashke", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Практический опыт выращивания фисташек в Кыргызстане", Path = "Prakticheskiy_opyt_vyrashchivaniya_fistashek_v_Kyrgyzstane", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Закладка плантаций фисташки путем посева семян, путем посадки сеянцев с закрытой корневой системой", Path = "Zakladka_plantatsiy_fistashki_putem_poseva_semyan,_putem_posadki_seyantsev_s_zakrytoy_kornevoy_sistemoy", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Орехоплодные сосны", CourseId = autorId, Path = "Orekhoplodnye_sosny",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Орехоплодные сосны", Path = "Orekhoplodnye_sosny", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Орехоплодные сосны-2", Path = "Orekhoplodnye_sosny-2", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Самодельная машина для очистки кедрового ореха от скорлупы", Path = "Samodelnaya_mashina_dlya_ochistki_kedrovogo_orekha_ot_skorlupy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Самодельная машина для очистки кедрового ореха - 2", Path = "Samodelnaya_mashina_dlya_ochistki_kedrovogo_orekha_-_2", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Линия переработки кедрового ореха на ядро. www.nz-sm.ru", Path = "Liniya_pererabotki_kedrovogo_orekha_na_yadro._www.nz-sm.ru", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Станок для очистки кедрового ореха от скорлупы", Path = "Stanok_dlya_ochistki_kedrovogo_orekha_ot_skorlupy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Рушилка кедрового ореха КЕДРОЛУЧ", Path = "Rushilka_kedrovogo_orekha_KEDROLUCh", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Сосновая пыльца ??Польза, как собираем, храним и применяем", Path = "Sosnovaya_pyltsa_Polza,_kak_sobiraem,_khranim_i_primenyaem", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Гильдии, аллеи, азотофиксирующие породы", CourseId = autorId, Path = "Gil'dii,_allei,_azotofiksiruyushchie_porody",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЛО урок 15 Гильдии, аллеи, азотофиксирующ", Path = "LO_urok_15_Gildii,_allei,_azotofiksiruyushch", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Занятие 6 Доходный лесосад", Path = "Zanyatie_6_Dokhodnyy_lesosad", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Аллейное лесосадоводство", CourseId = autorId, Path = "Alleynoe_lesosadovodstvo",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЛО урок 16 Аллейное лесосадоводство", Path = "LO_urok_16_Alleynoe_lesosadovodstvo", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Жизнь в Синтропии", Path = "Zhizn_v_Sintropii", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Агролесоводство Эрнста Гетча", Path = "Agrolesovodstvo_Ernsta_Getcha", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Эрнст Гетч 'От сада к лесу'", Path = "Ernst_Getch_Ot_sada_k_lesu", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Сопутствующие виды. Краткосрочное и среднесрочное бизнес-планирование +урок по агротехнике батата", CourseId = autorId, Path = "Soputstvuyushchie_vidy._Kratkosrochnoe_i_srednesrochnoe_biznes-planirovanie_+urok_po_agrotekhnike_batata",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЛО урок 17 Сопутсвующие виды Краткосрочное", Path = "LO_urok_17_Soputsvuyushchie_vidy_Kratkosrochnoe", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Батат выращивание рассады - как вырастить Батат", Path = "Batat_vyrashchivanie_rassady_-_kak_vyrastit_Batat", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Ягода Годжи (дереза). Выращивание", Path = "Yagoda_Godzhi_(dereza)._Vyrashchivanie", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "лекция Глеба Тюрина", Path = "lektsiya_Gleba_Tyurina", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "3 способа создать интернет-магазин своими силами", Path = "3_sposoba_sozdat_internet-magazin_svoimi_silami", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вести с полей- фермер продает фрукты с помощью", Path = "Vesti_s_poley-_fermer_prodaet_frukty_s_pomoshchyu", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Экзотика Д Стадникова", Path = "Ekzotika_D_Stadnikova", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Экзотика в вегетарии", CourseId = autorId, Path = "Ekzotika_v_vegetarii",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЭПО урок 7 вегетарий", Path = "EPO_urok_7_vegetariy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Экзотика в вегетарии", Path = "Ekzotika_v_vegetarii", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Джеф Лотон. Канадская теплица", Path = "Dzhef_Loton._Kanadskaya_teplitsa", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Солнечный вегетарий, Парник из окон, Проверка зимой.", Path = "Solnechnyy_vegetariy,_Parnik_iz_okon,_Proverka_zimoy.", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Джефф Лотон _Пермакультура в городе_", Path = "Dzheff_Loton__Permakultura_v_gorode_", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Схемы НФ Киктенко", CourseId = autorId, Path = "Skhemy_NF_Kiktenko",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Урок 18 схемы киктенко с обработкой", Path = "Urok_18_skhemy_kiktenko_s_obrabotkoy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Посадка грецкого ореха", Path = "Posadka_gretskogo_orekha", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Схемы высадок в аллейном лесосаде. Сердцевидный орех", CourseId = autorId, Path = "Skhemy_vysadok_v_alleynom_lesosade._Serdtsevidnyy_orekh",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Урок 18 1 кейс с обработкой", Path = "Urok_18_1_keys_s_obrabotkoy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Чудо ферма", Path = "Chudo_ferma", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Свой питомник", CourseId = autorId, Path = "Svoy_pitomnik",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЛО урок 19 Свой питомник", Path = "LO_urok_19_Svoy_pitomnik", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Питомник с нуля. Остались сомнения_ - Давайте обсудим!", Path = "Pitomnik_s_nulya._Ostalis_somneniya__-_Davayte_obsudim", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Распикировка сеянцев на питомнике", Path = "Raspikirovka_seyantsev_na_pitomnike", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Закладка школки конского каштана в домашнем питомнике", Path = "Zakladka_shkolki_konskogo_kashtana_v_domashnem_pitomnike", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Примеры схемы аллей орех-лесосада (фундук)", CourseId = autorId, Path = "Primery_skhemy_alley_orekh-lesosada_(funduk)",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Схема лесосада. Фундук (с обработкой)", Path = "Skhema_lesosada._Funduk_(s_obrabotkoy)", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Примеры схем орех-лесосада (каштан, сосны)", CourseId = autorId, Path = "Primery_skhem_orekh-lesosada_(kashtan,_sosny)",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "20 схемы посадки", Path = "20_skhemy_posadki", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "ЛВПЦ, ООПТ, защитные леса, ОЗУ", CourseId = autorId, Path = "LVPTs,_OOPT,_zashchitnye_lesa,_OZU",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЛВПЦ, ООПТ, защитные леса, ОЗУ", Path = "LVPTs,_OOPT,_zashchitnye_lesa,_OZU", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Особо охраняемые природные территории", Path = "Osobo_okhranyaemye_prirodnye_territorii", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Вебинар по ООПТ с Михаилом Крейндлиным, Гринпис России", Path = "Vebinar_po_OOPT_s_Mikhailom_Kreyndlinym,_Grinpis_Rossii", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Застолби за собой 2 га леса", Path = "Zastolbi_za_soboy_2_ga_lesa", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Гинкго", CourseId = autorId, Path = "Ginkgo",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Гинкго", Path = "Ginkgo", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Гинкго Билоба, Развитие мозга!", Path = "Ginkgo_Biloba,_Razvitie_mozga", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Проектирование лесосада на поместье", CourseId = autorId, Path = "Proektirovanie_lesosada_na_pomest'e",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Проектируем доходный лесосад на поместье", Path = "Proektiruem_dokhodnyy_lesosad_na_pomeste", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Интродукция растений", CourseId = autorId, Path = "Introduktsiya_rasteniy",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Интродукция редких и ценных видов", Path = "Introduktsiya_redkikh_i_tsennykh_vidov", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Интродукция-2", Path = "Introduktsiya-2", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Водные орехи", CourseId = autorId, Path = "Vodnye_orekhi",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЛО урок 23 Водные орехи", Path = "LO_urok_23_Vodnye_orekhi", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Лотосы", Path = "Lotosy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Разведение лотосов в домашних условиях", Path = "Razvedenie_lotosov_v_domashnikh_usloviyakh", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Лотосы в августе", Path = "Lotosy_v_avguste", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "СТРЕЛОЛИСТ", Path = "STRELOLIST", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Как сохранить стрелолист зимой", Path = "Kak_sokhranit_strelolist_zimoy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "How to grow Organic Water Chestnuts", Path = "How_to_grow_Organic_Water_Chestnuts", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Закладка доходного лесосада", CourseId = autorId, Path = "Zakladka_dokhodnogo_lesosada",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Закладываем доходный лесосад", Path = "Zakladyvaem_dokhodnyy_lesosad", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Пермакультурный сад Собковиака 06", Path = "Permakulturnyy_sad_Sobkoviaka_06", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Капельный полив бутылками -всё гениально и просто!", Path = "Kapelnyy_poliv_butylkami_-vsyo_genialno_i_prosto", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "10 ошибок при посадке пермакультурных лесосадов!", Path = "10_oshibok_pri_posadke_permakulturnykh_lesosadov", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Микроэлементы и заключение", CourseId = autorId, Path = "Mikroelementy_i_zaklyuchenie",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "ЛО урок 24 Микроэлементы и заключение", Path = "LO_urok_24_Mikroelementy_i_zaklyuchenie", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Законное выращивание пород ДКР на с_х земле", Path = "Zakonnoe_vyrashchivanie_porod_DKR_na_s_kh_zemle", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Роль селена в органиме человека", Path = "Rol_selena_v_organime_cheloveka", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Магний и здоровье. Риски дефицита.", Path = "Magniy_i_zdorove._Riski_defitsita.", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Лесное фермерство 1. Соконосные виды", CourseId = autorId, Path = "Lesnoe_fermerstvo_1._Sokonosnye_vidy",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Лесное фермерство 1. Соконосные виды", Path = "Lesnoe_fermerstvo_1._Sokonosnye_vidy", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Заготовка пробковои? коры", Path = "Zagotovka_probkovoi_kory", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Правильная подсочка клена! Кленовый сок - 19 февраля 2017", Path = "Pravilnaya_podsochka_klena_Klenovyy_sok_-_19_fevralya_2017", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Лесное фермерство 2. Кленовый сироп", CourseId = autorId, Path = "Lesnoe_fermerstvo_2._Klenovyy_sirop",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "Лесное фермерство 2. Кленовый сироп", Path = "Lesnoe_fermerstvo_2._Klenovyy_sirop", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "НТВ- России?скии? кленовыи? сироп 6 кленов", Path = "NTV-_Rossiiskii_klenovyi_sirop_6_klenov", VideoTypeId = videoTypeId },
                        new Video { Id = videoId++, Name = "Делаем березовое масло (крем-паста) How to make birch cream (pasta, butter)", Path = "Delaem_berezovoe_maslo_(krem-pasta)_How_to_make_birch_cream_(pasta,_butter)", VideoTypeId = videoTypeId },
                    }
                },

                new Folder
                {
                    Id = folderId++, Name = "Новые изменения в законодательстве РФ", CourseId = autorId, Path = "Novye_izmeneniya_v_zakonodatel'stve_RF",
                    Videos = new Video[]
                    {
                        new Video { Id = videoId++, Name = "В 2020 году за переводы на карту доначислят НДФЛ и НДС...", Path = "V_2020_godu_za_perevody_na_kartu_donachislyat_NDFL_i_NDS...", VideoTypeId = videoTypeId },
                    }
                },

            };


            return peretyatFolders;
        }
    }
}
