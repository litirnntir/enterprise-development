using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Factory.Test
{
    /// <summary>
    /// Пример модели "Тип индустрии"
    /// </summary>
    public class TypeIndustry
    {
        public int TypeIndustryID { get; set; }
        public string Name { get; set; }

        public TypeIndustry()
        {
            TypeIndustryID = 0;
            Name = string.Empty;
        }

        public TypeIndustry(int id, string name)
        {
            TypeIndustryID = id;
            Name = name;
        }
    }

    /// <summary>
    /// Пример модели "Форма собственности"
    /// </summary>
    public class OwnershipForm
    {
        public int OwnershipFormID { get; set; }
        public string Name { get; set; }

        public OwnershipForm()
        {
            OwnershipFormID = 0;
            Name = string.Empty;
        }

        public OwnershipForm(int id, string name)
        {
            OwnershipFormID = id;
            Name = name;
        }
    }

    /// <summary>
    /// Пример модели "Поставщик"
    /// </summary>
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<Supply> Supplies { get; set; }

        public Supplier()
        {
            SupplierID = 0;
            Name = string.Empty;
            Address = string.Empty;
            Phone = string.Empty;
            Supplies = new List<Supply>();
        }

        public Supplier(int id, string name, string address, string phone)
        {
            SupplierID = id;
            Name = name;
            Address = address;
            Phone = phone;
            Supplies = new List<Supply>();
        }

        public Supplier(int id, string name, string address, string phone, List<Supply> supplies)
        {
            SupplierID = id;
            Name = name;
            Address = address;
            Phone = phone;
            Supplies = supplies;
        }
    }

    /// <summary>
    /// Пример модели "Поставка"
    /// </summary>
    public class Supply
    {
        public int SupplyID { get; set; }
        public int EnterpriseID { get; set; }
        public int SupplierID { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

        public Supply()
        {
            SupplyID = 0;
            EnterpriseID = 0;
            SupplierID = 0;
            Date = new DateTime(1970, 1, 1);
            Quantity = 0;
        }

        public Supply(int supplyID, int enterpriseID, int supplierID, string date, int quantity)
        {
            SupplyID = supplyID;
            EnterpriseID = enterpriseID;
            SupplierID = supplierID;
            Date = DateTime.Parse(date); // Или другой способ парсинга, если нужно
            Quantity = quantity;
        }
    }

    /// <summary>
    /// Пример модели "Предприятие"
    /// </summary>
    public class Enterprise
    {
        public int EnterpriseID { get; set; }
        public string RegistrationNumber { get; set; }
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public int OwnershipFormID { get; set; }
        public int EmployeesCount { get; set; }
        public int TotalArea { get; set; }
        public List<Supply> Supplies { get; set; }

        public Enterprise()
        {
            EnterpriseID = 0;
            RegistrationNumber = string.Empty;
            TypeID = 0;
            Name = string.Empty;
            Address = string.Empty;
            TelephoneNumber = string.Empty;
            OwnershipFormID = 0;
            EmployeesCount = 0;
            TotalArea = 0;
            Supplies = new List<Supply>();
        }

        public Enterprise(int enterpriseID, string registrationNumber, int typeID, string name,
                          string address, string telephoneNumber, int ownershipFormID,
                          int employeesCount, int totalArea)
        {
            EnterpriseID = enterpriseID;
            RegistrationNumber = registrationNumber;
            TypeID = typeID;
            Name = name;
            Address = address;
            TelephoneNumber = telephoneNumber;
            OwnershipFormID = ownershipFormID;
            EmployeesCount = employeesCount;
            TotalArea = totalArea;
            Supplies = new List<Supply>();
        }
    }

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
                new Enterprise(1, "1036300446093", 6, "СТАН",    "ул.22 партъезда д.7а",  "88469926984",   1, 100, 1000),
                new Enterprise(2, "1156313028981", 6, "ЗГМ",     "ул.22 партъезда д.10а", "88462295931",   2, 150, 1500),
                new Enterprise(3, "1116318009510", 4, "ВЗМК",    "ул.Балаковская д.6а",   "884692007711",  2, 200, 2000),
                new Enterprise(4, "1026300767899", 2, "АВИАКОР", "ул.Земеца д.32",        "88463720888",   3, 250, 2500),
                new Enterprise(5, "1026301697487", 6, "ЭКРАН",   "ул.Кирова д.24",        "88469983785",   4, 130, 1300),
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

    /// <summary>
    /// Тестовый класс, использующий IClassFixture для шаринга контекста (FactoryContextFixture).
    /// </summary>
    public class FactoryTest : IClassFixture<FactoryContextFixture>
    {
        private readonly FactoryContextFixture _fixture;

        public FactoryTest(FactoryContextFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Selecting information about some factory
        /// </summary>
        [Fact]
        public void RequestTest1()
        {
            var enterprise = _fixture.Enterprises;
            var result = from e in enterprise
                         where e.RegistrationNumber == "1116318009510"
                         select e;

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, x => x.EnterpriseID == 3);
        }

        /// <summary>
        /// Selecting all suppliers who made supplies from 01.01.2023 to 30.01.2023
        /// </summary>
        [Fact]
        public void RequestTest2()
        {
            var suppliers = _fixture.Suppliers;
            var supplies = _fixture.Supplies;

            var result = from sr in suppliers
                         join s in supplies on sr.SupplierID equals s.SupplierID
                         where s.Date > new DateTime(2023, 1, 1) && s.Date < new DateTime(2023, 1, 30)
                         orderby sr.Name
                         select sr;

            Assert.Equal(3, result.Count());
            Assert.Contains(result, x => x.SupplierID == 1);
            Assert.Contains(result, x => x.SupplierID == 3);
            Assert.Contains(result, x => x.SupplierID == 5);
            Assert.DoesNotContain(result, x => x.SupplierID == 2);
            Assert.DoesNotContain(result, x => x.SupplierID == 4);
        }

        /// <summary>
        /// Selecting count of factories working with each supplier
        /// </summary>
        [Fact]
        public void RequestTest3()
        {
            var suppliers = _fixture.Suppliers;
            var supplies = _fixture.Supplies;
            var enterprises = _fixture.Enterprises;

            var result = from s in suppliers
                         join sp in supplies on s.SupplierID equals sp.SupplierID
                         join e in enterprises on sp.EnterpriseID equals e.EnterpriseID
                         group e by s into g
                         select new { Supplier = g.Key, EnterpriseCount = g.Count() };

            Assert.Equal(1, result.First(r => r.Supplier.SupplierID == 1).EnterpriseCount);
            Assert.Equal(2, result.First(r => r.Supplier.SupplierID == 2).EnterpriseCount);
            Assert.Equal(2, result.First(r => r.Supplier.SupplierID == 3).EnterpriseCount);
            Assert.Equal(1, result.First(r => r.Supplier.SupplierID == 4).EnterpriseCount);
            Assert.Equal(2, result.First(r => r.Supplier.SupplierID == 5).EnterpriseCount);
        }

        /// <summary>
        /// Selecting count of suppliers for every industry type
        /// </summary>
        [Fact]
        public void RequestTest4()
        {
            var enterprises = _fixture.Enterprises;
            var suppliers = _fixture.Suppliers;
            var supplies = _fixture.Supplies;

            var result = from e in enterprises
                         join sp in supplies on e.EnterpriseID equals sp.EnterpriseID
                         join s in suppliers on sp.SupplierID equals s.SupplierID
                         group s by e.TypeID into g
                         select new
                         {
                             IndustryType = g.Key,
                             SupplierCount = g.Count()
                         };

            // Проверяем typeID == 6 -> 5 поставок
            Assert.Equal(5, result.FirstOrDefault(x => x.IndustryType == 6).SupplierCount);
            // Проверяем typeID == 4 -> 1 поставка
            Assert.Equal(1, result.FirstOrDefault(x => x.IndustryType == 4).SupplierCount);
            // Проверяем typeID == 2 -> 2 поставки
            Assert.Equal(2, result.FirstOrDefault(x => x.IndustryType == 2).SupplierCount);
        }

        /// <summary>
        /// Selecting top-5 factories by supply count
        /// </summary>
        [Fact]
        public void RequestTest5()
        {
            var expected = new List<string> { "СТАН", "АВИАКОР", "ЗГМ", "ВЗМК", "ЭКРАН" };
            var supplies = _fixture.Supplies;
            var enterprises = _fixture.Enterprises;

            var topEnterprises = (from s in supplies
                                  join e in enterprises on s.EnterpriseID equals e.EnterpriseID
                                  group e by new { e.EnterpriseID, e.Name } into g
                                  orderby g.Count() descending
                                  select g.Key.Name)
                                 .Take(5);

            Assert.Equal(expected, topEnterprises.ToList());
        }

        /// <summary>
        /// Selecting supplier who delivered max quantity of goods 
        /// from 01.01.2023 to 01.03.2023 (в коде используем 30.01.2023, как в примере)
        /// </summary>
        [Fact]
        public void RequestTest6()
        {
            var suppliers = _fixture.Suppliers;
            var supplies = _fixture.Supplies;

            var result = (from s in suppliers
                          join sp in supplies on s.SupplierID equals sp.SupplierID
                          where sp.Date > new DateTime(2023, 1, 1) && sp.Date < new DateTime(2023, 1, 30)
                          orderby sp.Quantity descending
                          select new { s.Name, s.Address, s.Phone })
                         .First(); // берем первый элемент (максимум по Quantity)

            Assert.Equal("Барни Стинсон", result.Name);
            Assert.Equal("ул. Приоденься д.50", result.Address);
            Assert.Equal("89376431289", result.Phone);
        }

        /// <summary>
        /// TypeIndustry constructor with parameters test
        /// </summary>
        [Fact]
        public void TypeConstructoryTest()
        {
            var type = new TypeIndustry(2, "Транспорт");
            Assert.Equal(2, type.TypeIndustryID);
            Assert.Equal("Транспорт", type.Name);
        }

        /// <summary>
        /// Ownership Form constructor with parameters test
        /// </summary>
        [Fact]
        public void OwnershipConstructoryTest()
        {
            var ownership = new OwnershipForm(1, "ЗАО");
            Assert.Equal(1, ownership.OwnershipFormID);
            Assert.Equal("ЗАО", ownership.Name);
        }

        /// <summary>
        /// Enterprise constructor with parameters test
        /// </summary>
        [Fact]
        public void EnterpriseConstructorTest()
        {
            var supply = new Supply(1, 1, 1, "20.01.2023", 3);
            var enterprise = new Enterprise(1, "1036300446093", 6, "СТАН", "ул.22 партъезда д.7а", "88469926984", 1, 100, 1000);

            Assert.Equal(1, enterprise.EnterpriseID);
            Assert.Equal("1036300446093", enterprise.RegistrationNumber);
            Assert.Equal(6, enterprise.TypeID);
            Assert.Equal("СТАН", enterprise.Name);
            Assert.Equal("ул.22 партъезда д.7а", enterprise.Address);
            Assert.Equal("88469926984", enterprise.TelephoneNumber);
            Assert.Equal(1, enterprise.OwnershipFormID);
            Assert.Equal(100, enterprise.EmployeesCount);
            Assert.Equal(1000, enterprise.TotalArea);
        }

        /// <summary>
        /// Supplier constructor with parameters test
        /// </summary>
        [Fact]
        public void SupplierConstructorTest()
        {
            var supply = new Supply(1, 1, 1, "20.01.2023", 3);
            var supplier = new Supplier(1, "Джон Сноу", "ул. Таргариенская д.35", "89372229978", new List<Supply> { supply });

            Assert.Equal(1, supplier.SupplierID);
            Assert.Equal("Джон Сноу", supplier.Name);
            Assert.Equal("ул. Таргариенская д.35", supplier.Address);
            Assert.Equal("89372229978", supplier.Phone);
            Assert.Single(supplier.Supplies);
            Assert.Equal(supply, supplier.Supplies[0]);
        }

        /// <summary>
        /// Supply constructor with parameters test
        /// </summary>
        [Fact]
        public void SupplyConstructorTest()
        {
            var supply = new Supply(1, 1, 2, "20.01.2023", 3);
            Assert.Equal(1, supply.SupplyID);
            Assert.Equal(1, supply.EnterpriseID);
            Assert.Equal(2, supply.SupplierID);
            Assert.Equal(new DateTime(2023, 1, 20), supply.Date);
            Assert.Equal(3, supply.Quantity);
        }

        /// <summary>
        /// TypeIndustry default constructor test
        /// </summary>
        [Fact]
        public void TDefaultConstructorTest()
        {
            var type = new TypeIndustry();
            Assert.Equal(0, type.TypeIndustryID);
            Assert.Equal(string.Empty, type.Name);
        }

        /// <summary>
        /// Ownership Form default constructor test
        /// </summary>
        [Fact]
        public void ODefaultConstructorTest()
        {
            var ownership = new OwnershipForm();
            Assert.Equal(0, ownership.OwnershipFormID);
            Assert.Equal(string.Empty, ownership.Name);
        }

        /// <summary>
        /// Enterprise default constructor test
        /// </summary>
        [Fact]
        public void EDefaultConstructorTest()
        {
            var enterprise = new Enterprise();
            Assert.Equal(0, enterprise.EnterpriseID);
            Assert.Equal(string.Empty, enterprise.RegistrationNumber);
            Assert.Equal(0, enterprise.TypeID);
            Assert.Equal(string.Empty, enterprise.Name);
            Assert.Equal(string.Empty, enterprise.Address);
            Assert.Equal(string.Empty, enterprise.TelephoneNumber);
            Assert.Equal(0, enterprise.OwnershipFormID);
            Assert.Equal(0, enterprise.EmployeesCount);
            Assert.Equal(0, enterprise.TotalArea);
            Assert.Empty(enterprise.Supplies);
        }

        /// <summary>
        /// Supplier default constructor test
        /// </summary>
        [Fact]
        public void SDefaultConstructorTest()
        {
            var supplier = new Supplier();
            Assert.Equal(0, supplier.SupplierID);
            Assert.Equal(string.Empty, supplier.Name);
            Assert.Equal(string.Empty, supplier.Address);
            Assert.Equal(string.Empty, supplier.Phone);
            Assert.Empty(supplier.Supplies);
        }

        /// <summary>
        /// Supply default constructor test
        /// </summary>
        [Fact]
        public void SPDefaultConstructorTest()
        {
            var supply = new Supply();
            Assert.Equal(0, supply.SupplyID);
            Assert.Equal(0, supply.EnterpriseID);
            Assert.Equal(0, supply.SupplierID);
            Assert.Equal(new DateTime(1970, 1, 1), supply.Date);
            Assert.Equal(0, supply.Quantity);
        }
    }
}
