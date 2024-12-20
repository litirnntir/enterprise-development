using AutoMapper;
using Fabrics.Domain;
using Fabrics.Server.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fabrics.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShipmentController(
    ILogger<ShipmentController> logger,
    IDbContextFactory<FabricsDbContext> contextFactory,
    IMapper mapper
) : ControllerBase
{
    private readonly ILogger<ShipmentController> _logger = logger;
    private readonly IDbContextFactory<FabricsDbContext> _contextFactory = contextFactory;
    private readonly IMapper _mapper = mapper;

    [HttpGet]
    public async Task<IEnumerable<ShipmentGetDto>> Get()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        _logger.LogInformation("Retrieving all shipments.");
        var shipments = await context.Shipments.ToListAsync();
        return _mapper.Map<IEnumerable<ShipmentGetDto>>(shipments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipmentGetDto>> Get(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var shipment = await context.Shipments.FirstOrDefaultAsync(s => s.Id == id);
        if (shipment == null)
        {
            _logger.LogInformation("Shipment not found: {id}", id);
            return NotFound();
        }
        return Ok(_mapper.Map<ShipmentGetDto>(shipment));
    }

    [HttpPost]
    public async Task<ActionResult<ShipmentGetDto>> Post([FromBody] ShipmentPostDto shipmentDto)
    {
        var shipment = _mapper.Map<Shipment>(shipmentDto);

        await using var context = await _contextFactory.CreateDbContextAsync();
        await context.Shipments.AddAsync(shipment);
        await context.SaveChangesAsync();

        var result = _mapper.Map<ShipmentGetDto>(shipment);
        return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ShipmentPostDto shipmentDto)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var existingShipment = await context.Shipments.FirstOrDefaultAsync(s => s.Id == id);

        if (existingShipment == null)
        {
            _logger.LogInformation("Shipment not found: {id}", id);
            return NotFound();
        }

        _mapper.Map(shipmentDto, existingShipment);
        context.Shipments.Update(existingShipment);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var shipment = await context.Shipments.FirstOrDefaultAsync(s => s.Id == id);

        if (shipment == null)
        {
            _logger.LogInformation("Shipment not found: {id}", id);
            return NotFound();
        }

        context.Shipments.Remove(shipment);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
