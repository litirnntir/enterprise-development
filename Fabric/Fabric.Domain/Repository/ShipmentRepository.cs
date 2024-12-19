using Fabrics.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fabrics.Domain.Repositories;

/// <summary>
/// Repository class for managing Shipment entities.
/// </summary>
public class ShipmentRepository : IRepository<Shipment>
{
    private readonly FabricsDbContext _context;

    public ShipmentRepository(FabricsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Deletes a Shipment entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Shipment to delete.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    public bool Delete(int id)
    {
        var shipment = GetById(id);
        if (shipment == null)
            return false;

        _context.Shipments.Remove(shipment);
        _context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Gets all Shipment entities.
    /// </summary>
    /// <returns>A list of all Shipment entities.</returns>
    public IEnumerable<Shipment> GetAll() => _context.Shipments.ToList();

    /// <summary>
    /// Gets a Shipment entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Shipment to retrieve.</param>
    /// <returns>The Shipment entity if found, otherwise null.</returns>
    public Shipment? GetById(int id) => _context.Shipments.FirstOrDefault(s => s.Id == id);

    /// <summary>
    /// Adds a new Shipment entity.
    /// </summary>
    /// <param name="entity">The Shipment entity to add.</param>
    /// <returns>The added Shipment entity.</returns>
    public Shipment? Post(Shipment entity)
    {
        _context.Shipments.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Updates an existing Shipment entity.
    /// </summary>
    /// <param name="id">ID of the Shipment to update.</param>
    /// <param name="entity">The updated Shipment entity.</param>
    /// <returns>True if the update was successful, otherwise false.</returns>
    public bool Put(int id, Shipment entity)
    {
        var existingShipment = GetById(id);
        if (existingShipment == null)
            return false;

        existingShipment.FabricId = entity.FabricId;
        existingShipment.ProviderId = entity.ProviderId;
        existingShipment.Date = entity.Date;
        existingShipment.NumberOfGoods = entity.NumberOfGoods;

        _context.SaveChanges();
        return true;
    }
}
