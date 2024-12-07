﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Model;

/// <summary>
/// Class describing supplying
/// </summary>
public class Supply
{
    /// <summary>
    /// Supply identifier
    /// </summary>
    [Key]
    public int SupplyID { get; set; } = 0;

    /// <summary>
    /// Enterprise identifier
    /// </summary>
    [ForeignKey("Enterprise")]
    public int EnterpriseID { get; set; } = 0;

    /// <summary>
    /// Supplier identifier
    /// </summary>
    [ForeignKey("Supplier")]
    public int SupplierID { get; set; } = 0;

    /// <summary>
    /// Date
    /// </summary>
    public DateTime Date { get; set; } = new DateTime(1970, 1, 1);

    /// <summary>
    /// Goods count
    /// </summary>
    public int Quantity { get; set; } = 0;

    public Supply() { }

    public Supply(int supplyID, int enterpriseID, int supplierID, string date, int quantity)
    {
        SupplyID = supplyID;
        EnterpriseID = enterpriseID;
        SupplierID = supplierID;
        Date = DateTime.Parse(date);
        Quantity = quantity;
    }
}