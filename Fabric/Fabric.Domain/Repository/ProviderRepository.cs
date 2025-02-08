using Fabrics.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fabrics.Domain.Repositories;

/// <summary>
/// Repository class for managing Provider entities.
/// </summary>
public class ProviderRepository(FabricsDbContext _context) : IRepository<Provider>
{
    /// <summary>
    /// Deletes a Provider entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Provider to delete.</param>
    /// <returns>True if deletion was successful, otherwise false.</returns>
    public bool Delete(int id)
    {
        var provider = GetById(id);
        if (provider == null)
            return false;

        _context.Providers.Remove(provider);
        _context.SaveChanges();
        return true;
    }

    /// <summary>
    /// Gets all Provider entities.
    /// </summary>
    /// <returns>A list of all Provider entities.</returns>
    public IEnumerable<Provider> GetAll() => _context.Providers.Include(p => p.Shipments).ToList();

    /// <summary>
    /// Gets a Provider entity by its ID.
    /// </summary>
    /// <param name="id">ID of the Provider to retrieve.</param>
    /// <returns>The Provider entity if found, otherwise null.</returns>
    public Provider? GetById(int id) => _context.Providers.Include(p => p.Shipments).FirstOrDefault(p => p.Id == id);

    /// <summary>
    /// Adds a new Provider entity.
    /// </summary>
    /// <param name="entity">The Provider entity to add.</param>
    /// <returns>The added Provider entity.</returns>
    public Provider? Post(Provider entity)
    {
        _context.Providers.Add(entity);
        _context.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Updates an existing Provider entity.
    /// </summary>
    /// <param name="id">ID of the Provider to update.</param>
    /// <param name="entity">The updated Provider entity.</param>
    /// <returns>True if the update was successful, otherwise false.</returns>
    public bool Put(int id, Provider entity)
    {
        var existingProvider = GetById(id);
        if (existingProvider == null)
            return false;

        existingProvider.Name = entity.Name;
        existingProvider.TypeOfGoods = entity.TypeOfGoods;
        existingProvider.Address = entity.Address;
        existingProvider.Shipments = entity.Shipments;

        _context.SaveChanges();
        return true;
    }
}
