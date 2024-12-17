using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;

namespace Factory.Test
{
    /// <summary>
    /// Фикстура: единый контекст, расшариваемый между тестами. 
    /// В конструкторе создаём все необходимые списки.
    /// </summary>
    public class FactoryContextFixture
    {
        public List<TypeIndustry> Types { get; }
        public List<OwnershipForm> Ownerships { get; }
        public List<Supply> Supplies { get; }
        public List<Enterprise> Enterprises { get; }
        public List<Supplier> Suppliers { get; }

        public FactoryContextFixture()
        {
            // Список видов индустрии
            Types = new List<TypeIndustry>()
            {
                new TypeIndustry(1, "Cельское хозяйство"),
                new TypeIndustry(2, "Транспорт"),
                new TypeIndustry(3, "Легкая промышленность"),
                new TypeIndustry(4, "Тяжелая промышленность"),
                new TypeIndustry(5, "Строительство"),
                new TypeIndustry(6, "Материально - техническое снабжение")
            };

            // Список форм собственности
            Ownerships = new List<OwnershipForm>()
            {
                new OwnershipForm(1, "ЗАО"),
                new OwnershipForm(2, "ООО"),
                new OwnershipForm(3, "АО"),
                new OwnershipForm(4, "ОАО")
            };

            // Список поставок
            Supplies = new List<Supply>()
            {
                new Supply(1, 1, 1, "20.01.2023", 3),  // СТАН - Артур
                new Supply(2, 1, 2, "31.10.2022", 5),  // СТАН - Чендлер
                new Supply(3, 3, 3, "14.08.2022", 1),  // ВЗМК - Барни
                new Supply(4, 4, 4, "05.02.2023", 10), // АВИАКОР - Джон
                new Supply(5, 2, 5, "27.02.2023", 6),  // ЗГМ - Райан
                new Supply(6, 5, 5, "13.01.2023", 2),  // ЭКРАН - Райан
                new Supply(7, 4, 3, "04.01.2023", 12), // АВИАКОР - Барни
                new Supply(8, 2, 2, "09.12.2022", 4)   // ЗГМ - Чендлер
            };

            // Список предприятий
            Enterprises = new List<Enterprise>()
            {
                new Enterprise(1, "1036300446093", 6, "СТАН",    "ул.22 партъезда д.7а",  "88469926984",   1, 100, 1000.0),
                new Enterprise(2, "1156313028981", 6, "ЗГМ",     "ул.22 партъезда д.10а", "88462295931",   2, 150, 1500.0),
                new Enterprise(3, "1116318009510", 4, "ВЗМК",    "ул.Балаковская д.6а",   "884692007711",  2, 200, 2000.0),
                new Enterprise(4, "1026300767899", 2, "АВИАКОР", "ул.Земеца д.32",        "88463720888",   3, 250, 2500.0),
                new Enterprise(5, "1026301697487", 6, "ЭКРАН",   "ул.Кирова д.24",        "88469983785",   4, 130, 1300.0),
            };

            // Список поставщиков
            Suppliers = new List<Supplier>()
            {
                new Supplier(1, "Артур Пирожков",  "ул. Зацепильная д.42",    "89375550203"),
                new Supplier(2, "Чендлер Бинг",   "ул. Центральная д.1",     "89370101010"),
                new Supplier(3, "Барни Стинсон",  "ул. Приоденься д.50",     "89376431289"),
                new Supplier(4, "Джон Сноу",      "ул. Таргариенская д.35",  "89372229978"),
                new Supplier(5, "Райан Гослинг",  "ул. Лалаленд д.14",       "89371234567")
            };
        }
    }
}