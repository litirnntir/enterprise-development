using Microsoft.AspNetCore.Mvc;
using Fabrics.Domain;
using Fabrics.Domain.Repositories;
using Fabrics.Server.Dto;
using AutoMapper;

namespace Fabrics.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProviderController(IRepository<Provider> repository, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Get all providers.
    /// </summary>
    /// <returns>List of ProviderGetDto</returns>
    /// <response code="200">Request successful</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProviderGetDto>>> Get()
    {
        var providers = await Task.FromResult(repository.GetAll());
        var providerDtos = mapper.Map<IEnumerable<ProviderGetDto>>(providers);
        return Ok(providerDtos);
    }

    /// <summary>
    /// Get a provider by ID.
    /// </summary>
    /// <param name="id">ID of the provider to retrieve</param>
    /// <returns>The provider with the specified ID</returns>
    /// <response code="200">Request successful</response>
    /// <response code="404">Provider not found</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProviderGetDto>> Get(int id)
    {
        var provider = await Task.FromResult(repository.GetById(id));

        if (provider == null)
            return NotFound();

        var providerDto = mapper.Map<ProviderGetDto>(provider);
        return Ok(providerDto);
    }

    /// <summary>
    /// Add a new provider.
    /// </summary>
    /// <param name="dto">The provider to add</param>
    /// <returns>The created provider</returns>
    /// <response code="201">Provider created successfully</response>
    [HttpPost]
    public async Task<ActionResult<ProviderGetDto>> Post([FromBody] ProviderPostDto dto)
    {
        var provider = mapper.Map<Provider>(dto);
        var createdProvider = await Task.FromResult(repository.Post(provider));
        var createdDto = mapper.Map<ProviderGetDto>(createdProvider);
        return CreatedAtAction(nameof(Get), new { id = createdDto.Id }, createdDto);
    }

    /// <summary>
    /// Update a provider by ID.
    /// </summary>
    /// <param name="id">ID of the provider to update</param>
    /// <param name="dto">The updated provider</param>
    /// <returns>The updated provider</returns>
    /// <response code="200">Request successful</response>
    /// <response code="404">Provider not found</response>
    [HttpPut("{id}")]
    public async Task<ActionResult<ProviderGetDto>> Put(int id, [FromBody] ProviderPostDto dto)
    {
        var provider = mapper.Map<Provider>(dto);
        if (!repository.Put(id, provider))
            return NotFound();

        var updatedDto = mapper.Map<ProviderGetDto>(provider);
        return Ok(updatedDto);
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
