using System.ComponentModel.DataAnnotations;

namespace Fabrics.Domain;
/// <summary>
/// Class Factory is used to store information about the fabric.
/// </summary>
public class Factory
{
    /// <summary>
    /// Gets or sets the unique identifier for the fabric.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the type of fabric.
    /// </summary>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the fabric.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the fabric.
    /// </summary>
    [Required]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number of the fabric.
    /// </summary>
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the form of ownership of the fabric.
    /// </summary>
    [Required]
    public string FormOfOwnership { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the number of workers in the fabric.
    /// </summary>
    [Required]
    public int NumberOfWorkers { get; set; }

    /// <summary>
    /// Gets or sets the total square footage of the fabric.
    /// </summary>
    [Required]
    public int TotalSquare { get; set; }

    /// <summary>
    /// Gets or sets the list of shipments related to the fabric.
    /// </summary>
    public List<Shipment>? Shipments { get; set; } = new List<Shipment>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Factory"/> class.
    /// </summary>
    public Factory() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Factory"/> class with specified parameters.
    /// </summary>
    public Factory(int id, string type, string name, string address, string phoneNumber, string formOfOwnership, int numberOfWorkers, int totalSquare, List<Shipment>? shipments)
    {
        Id = id;
        Type = type;
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        FormOfOwnership = formOfOwnership;
        NumberOfWorkers = numberOfWorkers;
        TotalSquare = totalSquare;
        Shipments = shipments ?? new List<Shipment>();
    }
}
