using AutoMapper;
using Fabrics.Domain;
using Fabrics.Domain.Repositories;
using Fabrics.Server.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Fabrics.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShipmentController(
    ILogger<ShipmentController> _logger,
    IRepository<Shipment> _shipmentRepository,
    IMapper _mapper
) : ControllerBase
{
    /// <summary>
    /// Retrieves all shipments.
    /// </summary>
    /// <returns>List of ShipmentGetDto</returns>
    /// <response code="200">Request successful</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ShipmentGetDto>), 200)]
    public ActionResult<IEnumerable<ShipmentGetDto>> Get()
    {
        _logger.LogInformation("Retrieving all shipments.");
        var shipments = _shipmentRepository.GetAll();
        return Ok(_mapper.Map<IEnumerable<ShipmentGetDto>>(shipments));
    }

    /// <summary>
    /// Retrieves a shipment by ID.
    /// </summary>
    /// <param name="id">ID of the shipment</param>
    /// <returns>The shipment with the specified ID</returns>
    /// <response code="200">Request successful</response>
    /// <response code="404">Shipment not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ShipmentGetDto), 200)]
    [ProducesResponseType(404)]
    public ActionResult<ShipmentGetDto> Get(int id)
    {
        var shipment = _shipmentRepository.GetById(id);
        if (shipment == null)
        {
            _logger.LogInformation("Shipment not found: {id}", id);
            return NotFound();
        }
        return Ok(_mapper.Map<ShipmentGetDto>(shipment));
    }

    /// <summary>
    /// Adds a new shipment.
    /// </summary>
    /// <param name="shipmentDto">The shipment data</param>
    /// <returns>The created shipment</returns>
    /// <response code="201">Shipment created successfully</response>
    [HttpPost]
    [ProducesResponseType(typeof(ShipmentGetDto), 201)]
    public ActionResult<ShipmentGetDto> Post([FromBody] ShipmentPostDto shipmentDto)
    {
        var shipment = _mapper.Map<Shipment>(shipmentDto);
        var result = _shipmentRepository.Post(shipment);
        return CreatedAtAction(nameof(Get), new { id = result?.Id }, _mapper.Map<ShipmentGetDto>(result));
    }

    /// <summary>
    /// Updates a shipment by ID.
    /// </summary>
    /// <param name="id">ID of the shipment</param>
    /// <param name="shipmentDto">The updated shipment data</param>
    /// <response code="204">Update successful</response>
    /// <response code="404">Shipment not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Put(int id, [FromBody] ShipmentPostDto shipmentDto)
    {
        var existingShipment = _shipmentRepository.GetById(id);
        if (existingShipment == null)
        {
            _logger.LogInformation("Shipment not found: {id}", id);
            return NotFound();
        }

        _mapper.Map(shipmentDto, existingShipment);
        _shipmentRepository.Put(id, existingShipment);
        return NoContent();
    }

    /// <summary>
    /// Deletes a shipment by ID.
    /// </summary>
    /// <param name="id">ID of the shipment</param>
    /// <response code="204">Deletion successful</response>
    /// <response code="404">Shipment not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Delete(int id)
    {
        var shipment = _shipmentRepository.GetById(id);
        if (shipment == null)
        {
            _logger.LogInformation("Shipment not found: {id}", id);
            return NotFound();
        }

        _shipmentRepository.Delete(id);
        return NoContent();
    }
}
