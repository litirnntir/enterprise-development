using Fabrics.Domain;
using Fabrics.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fabrics.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueryController(IRepository<Shipment> shipmentRepository, IRepository<Provider> providerRepository) : ControllerBase
{
    /// <summary>
    /// Get all shipments by a specific provider.
    /// </summary>
    /// <param name="providerId">ID of the provider</param>
    /// <returns>List of shipments</returns>
    [HttpGet("shipments_by_provider")]
    public async Task<ActionResult<IEnumerable<Shipment>>> GetShipmentsByProvider(int providerId)
    {
        var shipments = await Task.FromResult(shipmentRepository.GetAll());

        return Ok(shipments.Where(s => s.ProviderId == providerId));
    }

    /// <summary>
    /// Get all providers who supplied goods within a specific period, ordered by name.
    /// </summary>
    /// <param name="startDate">Start date of the period</param>
    /// <param name="endDate">End date of the period</param>
    /// <returns>List of providers</returns>
    [HttpGet("providers_by_date")]
    public async Task<ActionResult<IEnumerable<Provider>>> GetProvidersByDate(DateTime startDate, DateTime endDate)
    {
        if (DateTime.Compare(startDate, endDate) >= 0)
            return BadRequest("Start date must be earlier than end date.");

        var shipments = await Task.FromResult(shipmentRepository.GetAll());

        return Ok(shipments
            .Where(s => s.Date >= startDate && s.Date <= endDate)
            .Select(s => providerRepository.GetById(s.ProviderId))
            .Distinct()
            .OrderBy(p => p.Name));
    }

    /// <summary>
    /// Get the count of unique fabrics served by each provider.
    /// </summary>
    /// <returns>List of providers with fabric counts</returns>
    [HttpGet("count_fabrics_by_provider")]
    public async Task<ActionResult<IEnumerable<object>>> CountFabricsByProvider()
    {
        var shipments = await Task.FromResult(shipmentRepository.GetAll());

        return Ok(shipments
            .GroupBy(s => s.ProviderId)
            .Select(group => new
            {
                Provider = providerRepository.GetById(group.Key),
                FabricCount = group.Select(s => s.FabricId).Distinct().Count()
            }).ToList());
    }

    /// <summary>
    /// Get the top 5 fabrics by shipment count.
    /// </summary>
    /// <returns>List of top fabrics</returns>
    [HttpGet("top_5_fabrics")]
    public async Task<ActionResult<IEnumerable<object>>> Top5FabricsByShipments()
    {
        var shipments = await Task.FromResult(shipmentRepository.GetAll());

        return Ok(shipments
            .GroupBy(s => s.FabricId)
            .Select(group => new
            {
                FabricId = group.Key,
                ShipmentCount = group.Count()
            })
            .OrderByDescending(x => x.ShipmentCount)
            .Take(5)
            .ToList());
    }

    /// <summary>
    /// Get providers with the maximum quantity of goods supplied within a specified period.
    /// </summary>
    /// <param name="startDate">Start date of the period</param>
    /// <param name="endDate">End date of the period</param>
    /// <returns>List of providers with maximum quantity</returns>
    [HttpGet("max_quantity_providers_by_period")]
    public async Task<ActionResult<IEnumerable<object>>> MaxProvidersByQuantity(DateTime startDate, DateTime endDate)
    {
        if (DateTime.Compare(startDate, endDate) >= 0)
            return BadRequest("Start date must be earlier than end date.");

        var shipments = await Task.FromResult(shipmentRepository.GetAll());

        var providerQuantities = shipments
            .Where(s => s.Date >= startDate && s.Date <= endDate)
            .GroupBy(s => s.ProviderId)
            .Select(group => new
            {
                Provider = providerRepository.GetById(group.Key),
                TotalQuantity = group.Sum(s => s.NumberOfGoods)
            }).ToList();

        var maxQuantity = providerQuantities.Max(x => x.TotalQuantity);

        return Ok(providerQuantities
            .Where(x => x.TotalQuantity == maxQuantity));
    }
}
