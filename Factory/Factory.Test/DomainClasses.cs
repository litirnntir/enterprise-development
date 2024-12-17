using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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
    /// Пример модели "Поставка"
    /// </summary>
    public class Supply
    {
        [Key]
        public int SupplyID { get; set; } = 0;

        [ForeignKey("Enterprise")]
        public int EnterpriseID { get; set; } = 0;

        [ForeignKey("Supplier")]
        public int SupplierID { get; set; } = 0;

        public DateTime Date { get; set; } = new DateTime(1970, 1, 1);
        public int Quantity { get; set; } = 0;

        public Supply() { }

        public Supply(int supplyID, int enterpriseID, int supplierID, string date, int quantity)
        {
            SupplyID = supplyID;
            EnterpriseID = enterpriseID;
            SupplierID = supplierID;
            Date = DateTime.ParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Quantity = quantity;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Supply other)
            {
                return SupplyID == other.SupplyID &&
                       EnterpriseID == other.EnterpriseID &&
                       SupplierID == other.SupplierID &&
                       Date == other.Date &&
                       Quantity == other.Quantity;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SupplyID, EnterpriseID, SupplierID, Date, Quantity);
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
        public double TotalArea { get; set; }
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
            TotalArea = 0.0;
            Supplies = new List<Supply>();
        }

        public Enterprise(int enterpriseID, string registrationNumber, int typeID, string name,
                          string address, string telephoneNumber, int ownershipFormID,
                          int employeesCount, double totalArea)
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
}