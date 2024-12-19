using System.ComponentModel.DataAnnotations;

namespace Fabrics.Domain;

public class Provider
{

    [Key]
    public int Id { get; set; }
 
    public string Name { get; set; } = string.Empty;
 
    public string TypeOfGoods { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public List<Shipment> Shipments { get; set; } = new List<Shipment>();
    public Provider() { }
    public Provider(int id, string name, string typeOfGoods, string address, List<Shipment> shipments)
    {
        Id = id;
        Name = name;
        TypeOfGoods = typeOfGoods;
        Address = address;
        Shipments = shipments;
    }
}
