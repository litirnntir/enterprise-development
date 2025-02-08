using System.ComponentModel.DataAnnotations;

namespace Fabrics.Domain;

/// <summary>
/// Represents a provider of goods.
/// </summary>
public class Provider
{
    /// <summary>
    /// Gets or sets the unique identifier for the provider.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of goods provided by the provider.
    /// </summary>
    [Required]
    public string TypeOfGoods { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the provider.
    /// </summary>
    [Required]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of shipments associated with the provider.
    /// </summary>
    public List<Shipment>? Shipments { get; set; } = new List<Shipment>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Provider"/> class.
    /// </summary>
    public Provider() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Provider"/> class with specified parameters.
    /// </summary>
    public Provider(int id, string name, string typeOfGoods, string address, List<Shipment>? shipments)
    {
        Id = id;
        Name = name;
        TypeOfGoods = typeOfGoods;
        Address = address;
        Shipments = shipments ?? new List<Shipment>();
    }
}
