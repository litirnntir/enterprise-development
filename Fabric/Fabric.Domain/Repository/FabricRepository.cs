using Fabrics.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fabrics.Domain.Repositories;

/// <summary>
/// Repository class for managing Fabric entities.
/// </summary>
public class FabricRepository : IRepository<Fabric>
{
    private readonly FabricsDbContext _context;

    public FabricRepository(FabricsDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Deletes a Fabric entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Fabric to delete.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    public bool Delete(int id)
    {
        var fabric = GetById(id);
        if (fabric == null)
            return false;

        _context.Fabrics.Remove(fabric);
        _context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Gets all Fabric entities.
    /// </summary>
    /// <returns>A list of all Fabric entities.</returns>
    public IEnumerable<Fabric> GetAll() => _context.Fabrics.Include(f => f.Shipments).ToList();

    /// <summary>
    /// Gets a Fabric entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Fabric to retrieve.</param>
    /// <returns>The Fabric entity if found, otherwise null.</returns>
    public Fabric? GetById(int id) => _context.Fabrics.Include(f => f.Shipments).FirstOrDefault(f => f.Id == id);

    /// <summary>
    /// Adds a new Fabric entity.
    /// </summary>
    /// <param name="entity">The Fabric entity to add.</param>
    /// <returns>The added Fabric entity.</returns>
    public Fabric? Post(Fabric entity)
    {
        _context.Fabrics.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Updates an existing Fabric entity.
    /// </summary>
    /// <param name="id">ID of the Fabric to update.</param>
    /// <param name="entity">The updated Fabric entity.</param>
    /// <returns>True if the update was successful, otherwise false.</returns>
    public bool Put(int id, Fabric entity)
    {
        var existingFabric = GetById(id);
        if (existingFabric == null)
            return false;

        existingFabric.Type = entity.Type;
        existingFabric.Name = entity.Name;
        existingFabric.Address = entity.Address;
        existingFabric.PhoneNumber = entity.PhoneNumber;
        existingFabric.FormOfOwnership = entity.FormOfOwnership;
        existingFabric.NumberOfWorkers = entity.NumberOfWorkers;
        existingFabric.TotalSquare = entity.TotalSquare;
        existingFabric.Shipments = entity.Shipments;

        _context.SaveChanges();
        return true;
    }
}
