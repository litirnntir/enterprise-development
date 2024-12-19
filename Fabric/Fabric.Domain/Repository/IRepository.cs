namespace Fabrics.Domain.Repositories;

/// <summary>
/// Generic repository interface for managing entities.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Gets all entities.
    /// </summary>
    /// <returns>A list of all entities.</returns>
    IEnumerable<T> GetAll();

    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity.</param>
    /// <returns>The entity with the specified identifier, or null if not found.</returns>
    T? GetById(int id);

    /// <summary>
    /// Adds a new entity.
    /// </summary>
    /// <param name="entity">The new entity to add.</param>
    /// <returns>The added entity.</returns>
    T? Post(T entity);

    /// <summary>
    /// Updates an existing entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to update.</param>
    /// <param name="entity">The updated entity.</param>
    /// <returns>True if the update was successful, otherwise false.</returns>
    bool Put(int id, T entity);

    /// <summary>
    /// Deletes an entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the entity to delete.</param>
    /// <returns>True if the deletion was successful, otherwise false.</returns>
    bool Delete(int id);
}
