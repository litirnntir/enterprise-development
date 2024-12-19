using System.ComponentModel.DataAnnotations;

namespace Fabrics.Domain;

public class Shipment
{

    [Key]
    public int Id { get; set; }
   
    public int FabricId { get; set; } = 0;

    public int ProviderId { get; set; } = 0;

    public DateTime Date { get; set; }

    public int NumberOfGoods { get; set; }

    public Shipment() { }
    public Shipment(int id, int fabricId, int providerId, DateTime date, int numberOfGoods)
    {
        Id = id;
        FabricId = fabricId;
        ProviderId = providerId;
        Date = date;
        NumberOfGoods = numberOfGoods;
    }
}