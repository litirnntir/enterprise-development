using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Factory.Model
{
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
}