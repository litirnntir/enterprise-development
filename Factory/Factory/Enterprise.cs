using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Model;

/// <summary>
/// Class describing factory
/// </summary>
public class Enterprise
{
    /// <summary>
    /// Enterprise Identifier (Primary Key)
    /// </summary>
    [Key]
    public int EnterpriseID { get; set; }

    /// <summary>
    /// RegistrationNumber
    /// </summary>
    [Required] // строка не должна быть null
    public string RegistrationNumber { get; set; } = string.Empty;

    /// <summary>
    /// Industry Type ID
    /// </summary>
    [Required]
    [ForeignKey("TypeIndustry")]
    public int TypeID { get; set; }

    /// <summary>
    /// Factory name
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Address
    /// </summary>
    [Required]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Telephone number
    /// </summary>
    [Required]
    public string TelephoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Ownership form ID
    /// </summary>
    [Required]
    [ForeignKey("OwnershipForm")]
    public int OwnershipFormID { get; set; }

    /// <summary>
    /// Employees count
    /// </summary>
    [Required]
    public int EmployeesCount { get; set; }

    /// <summary>
    /// Total Area
    /// </summary>
    [Required]
    public double TotalArea { get; set; }

    /// <summary>
    /// List of supplies
    /// </summary>
    [Required]
    public List<Supply> Supplies { get; set; } = new();  // Инициализируем пустым списком

    public Enterprise() { }

    public Enterprise(
        int enterpriseID,
        string registrationNumber,
        int typeID,
        string name,
        string address,
        string telephoneNumber,
        int ownershipFormID,
        int employeesCount,
        double totalArea)
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