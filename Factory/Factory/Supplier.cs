using System.ComponentModel.DataAnnotations;

namespace Factory.Model
{
    /// <summary>
    /// Class describing supplier
    /// </summary>
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public List<Supply> Supplies { get; set; } = null!;

        public Supplier()
        {
            Supplies = new List<Supply>();
        }

        public Supplier(int supplierID, string name, string address, string phone)
        {
            SupplierID = supplierID;
            Name = name;
            Address = address;
            Phone = phone;
            Supplies = new List<Supply>();
        }

        // Новый конструктор, принимающий сразу коллекцию поставок
        public Supplier(int supplierID, string name, string address, string phone, IEnumerable<Supply> supplies)
        {
            SupplierID = supplierID;
            Name = name;
            Address = address;
            Phone = phone;
            Supplies = new List<Supply>(supplies);
        }
    }
}