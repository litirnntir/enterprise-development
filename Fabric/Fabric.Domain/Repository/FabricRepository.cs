using Fabrics.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fabrics.Domain.Repositories;

/// <summary>
/// Repository class for managing Factory entities.
/// </summary>
public class FabricRepository(FabricsDbContext _context) : IRepository<Factory>
{
    /// <summary>
    /// Deletes a Factory entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Factory to delete.</param>
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
    /// Gets all Factory entities.
    /// </summary>
    /// <returns>A list of all Factory entities.</returns>
    public IEnumerable<Factory> GetAll() => _context.Fabrics.Include(f => f.Shipments).ToList();

    /// <summary>
    /// Gets a Factory entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Factory to retrieve.</param>
    /// <returns>The Factory entity if found, otherwise null.</returns>
    public Factory? GetById(int id) => _context.Fabrics.Include(f => f.Shipments).FirstOrDefault(f => f.Id == id);

    /// <summary>
    /// Adds a new Factory entity.
    /// </summary>
    /// <param name="entity">The Factory entity to add.</param>
    /// <returns>The added Factory entity.</returns>
    public Factory? Post(Factory entity)
    {
        _context.Fabrics.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Updates an existing Factory entity.
    /// </summary>
    /// <param name="id">ID of the Factory to update.</param>
    /// <param name="entity">The updated Factory entity.</param>
    /// <returns>True if the update was successful, otherwise false.</returns>
    public bool Put(int id, Factory entity)
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
