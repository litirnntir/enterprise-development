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
            Assert.Equal(5, result.FirstOrDefault(x => x.IndustryType == 6)?.SupplierCount ?? 0);
            // Проверяем typeID == 4 -> 1 поставка
            Assert.Equal(1, result.FirstOrDefault(x => x.IndustryType == 4)?.SupplierCount ?? 0);
            // Проверяем typeID == 2 -> 2 поставки
            Assert.Equal(2, result.FirstOrDefault(x => x.IndustryType == 2)?.SupplierCount ?? 0);
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
        /// from 01.01.2023 to 30.01.2023
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
        /// Тест конструктора Supply
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
        /// Тест конструктора TypeIndustry
        /// </summary>
        [Fact]
        public void TypeConstructoryTest()
        {
            var type = new TypeIndustry(2, "Транспорт");
            Assert.Equal(2, type.TypeIndustryID);
            Assert.Equal("Транспорт", type.Name);
        }

        /// <summary>
        /// Тест конструктора OwnershipForm
        /// </summary>
        [Fact]
        public void OwnershipConstructoryTest()
        {
            var ownership = new OwnershipForm(1, "ЗАО");
            Assert.Equal(1, ownership.OwnershipFormID);
            Assert.Equal("ЗАО", ownership.Name);
        }

        /// <summary>
        /// Тест конструктора Enterprise
        /// </summary>
        [Fact]
        public void EnterpriseConstructorTest()
        {
            var enterprise = new Enterprise(1, "1036300446093", 6, "СТАН",
                                            "ул.22 партъезда д.7а", "88469926984",
                                            1, 100, 1000.0);

            Assert.Equal(1, enterprise.EnterpriseID);
            Assert.Equal("1036300446093", enterprise.RegistrationNumber);
            Assert.Equal(6, enterprise.TypeID);
            Assert.Equal("СТАН", enterprise.Name);
            Assert.Equal("ул.22 партъезда д.7а", enterprise.Address);
            Assert.Equal("88469926984", enterprise.TelephoneNumber);
            Assert.Equal(1, enterprise.OwnershipFormID);
            Assert.Equal(100, enterprise.EmployeesCount);
            Assert.Equal(1000.0, enterprise.TotalArea);
            Assert.Empty(enterprise.Supplies);
        }

        /// <summary>
        /// Тест конструктора Supplier
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
        /// Тест конструктора по умолчанию Supply
        /// </summary>
        [Fact]
        public void SupplyDefaultConstructorTest()
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