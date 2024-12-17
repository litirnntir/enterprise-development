﻿namespace Factory.Server.Dto;

/// <summary>
/// Class describing supplier
/// </summary>
public class SupplierGetDto
{
    /// <summary>
    /// Supplier identifier
    /// </summary>
    public int SupplierID { get; set; } = 0;

    /// <summary>
    /// Supplier's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///  Address
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Phone 
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}