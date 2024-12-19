using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Fabrics.Domain;
using Fabrics.Domain.Repositories;

namespace Fabrics.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProviderController(IRepository<Provider> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get all providers.
    /// </summary>
    /// <returns>List of <see cref="Provider"/></returns>
    /// <response code="200">Request successful</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Provider>>> Get() => Ok(await Task.FromResult(repository.GetAll()));

    /// <summary>
    /// Get a provider by ID.
    /// </summary>
    /// <param name="id">ID of the provider to retrieve</param>
    /// <returns>The provider with the specified ID</returns>
    /// <response code="200">Request successful</response>
    /// <response code="404">Provider not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<Provider>> Get(int id)
    {
        var provider = await Task.FromResult(repository.GetById(id));

        if (provider == null)
            return NotFound();

        return Ok(provider);
    }

    /// <summary>
    /// Add a new provider.
    /// </summary>
    /// <param name="item">The provider to add</param>
    /// <returns>The created provider</returns>
    /// <response code="200">Request successful</response>
    [HttpPost]
    public async Task<ActionResult<Provider>> Post([FromBody] Provider item)
    {
        var provider = await Task.FromResult(repository.Post(item));
        return Ok(provider);
    }

    /// <summary>
    /// Update a provider by ID.
    /// </summary>
    /// <param name="id">ID of the provider to update</param>
    /// <param name="item">The updated provider</param>
    /// <returns>The updated provider</returns>
    /// <response code="200">Request successful</response>
    /// <response code="404">Provider not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<Provider>> Put(int id, [FromBody] Provider item)
    {
        if (!repository.Put(id, item))
            return NotFound();

        return Ok(item);
    }

    /// <summary>
    /// Delete a provider by ID.
    /// </summary>
    /// <param name="id">ID of the provider to delete</param>
    /// <response code="200">Request successful</response>
    /// <response code="404">Provider not found</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!repository.Delete(id)) return NotFound();
        return Ok();
    }
}

