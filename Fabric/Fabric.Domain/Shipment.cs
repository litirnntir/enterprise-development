using System.ComponentModel.DataAnnotations;

namespace Fabrics.Domain;

/// <summary>
/// Represents a shipment of goods related to a fabric.
/// </summary>
public class Shipment
{
    /// <summary>
    /// Gets or sets the unique identifier for the shipment.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the fabric identifier associated with the shipment.
    /// </summary>
    [Required]
    public int FabricId { get; set; }

    /// <summary>
    /// Gets or sets the provider identifier associated with the shipment.
    /// </summary>
    [Required]
    public int ProviderId { get; set; }

    /// <summary>
    /// Gets or sets the date of the shipment.
    /// </summary>
    [Required]
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the number of goods in the shipment.
    /// </summary>
    [Required]
    public int NumberOfGoods { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Shipment"/> class.
    /// </summary>
    public Shipment() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Shipment"/> class with specified parameters.
    /// </summary>
    public Shipment(int id, int fabricId, int providerId, DateTime date, int numberOfGoods)
    {
        Id = id;
        FabricId = fabricId;
        ProviderId = providerId;
        Date = date;
        NumberOfGoods = numberOfGoods;
    }
}
