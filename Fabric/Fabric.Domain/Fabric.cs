using System.ComponentModel.DataAnnotations;

namespace Fabrics.Domain;
/// <summary>
/// Class Fabric is used to store information of the fabric.
/// </summary>
public class Fabric
{

    [Key]
    public int Id { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string FormOfOwnership { get; set; } = string.Empty;

    public int NumberOfWorkers { get; set; }

    public int TotalSquare { get; set; }
    public List<Shipment> Shipments { get; set; } = new List<Shipment>();
    public Fabric() { }
    public Fabric(int id, string type, string name, string address, string phoneNumber, string formOfOwnership, int numberOfWorkers, int totalSquare, List<Shipment> shipments)
    {
        Id = id;
        Type = type;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        FormOfOwnership = formOfOwnership;
        NumberOfWorkers = numberOfWorkers;
        TotalSquare = totalSquare;
        Shipments = shipments;
    }
}
