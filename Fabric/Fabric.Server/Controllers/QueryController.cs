using Fabrics.Domain.Repositories;
using Fabrics.Domain;
using Fabrics.Server.Dto;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class QueryController(IRepository<Shipment> shipmentRepository, IRepository<Provider> providerRepository, IRepository<Factory> fabricRepository) : ControllerBase
{
    /// <summary>
    /// Get all shipments by a specific provider.
    /// </summary>
    [HttpGet("shipments-by-provider")]
    public async Task<ActionResult<IEnumerable<ShipmentGetDto>>> GetShipmentsByProvider(int providerId)
    {
        var shipments = await Task.FromResult(shipmentRepository.GetAll());
        var filteredShipments = shipments
            .Where(s => s.ProviderId == providerId)
            .Select(s => new ShipmentGetDto
            {
                Id = s.Id,
                FabricId = s.FabricId,
                ProviderId = s.ProviderId,
                Date = s.Date,
                NumberOfGoods = s.NumberOfGoods
            });

        return Ok(filteredShipments);
    }

    /// <summary>
    /// Get all providers who supplied goods within a specific period, ordered by name.
    /// </summary>
    [HttpGet("providers-by-date")]
    public async Task<ActionResult<IEnumerable<ProviderGetDto>>> GetProvidersByDate(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            return BadRequest("Start date must be earlier than end date.");

        var shipments = await Task.FromResult(shipmentRepository.GetAll());
        var providers = shipments
            .Where(s => s.Date >= startDate && s.Date <= endDate)
            .Select(s => providerRepository.GetById(s.ProviderId))
            .Where(p => p != null)
            .Distinct()
            .OrderBy(p => p.Name)
            .Select(p => new ProviderGetDto
            {
                Id = p.Id,
                Name = p.Name,
                TypeOfGoods = p.TypeOfGoods,
                Address = p.Address
            });

        return Ok(providers);
    }

    /// <summary>
    /// Get the count of unique fabrics served by each provider.
    /// </summary>
    [HttpGet("count-fabrics-by-provider")]
    public async Task<ActionResult<IEnumerable<object>>> CountFabricsByProvider()
    {
        var shipments = await Task.FromResult(shipmentRepository.GetAll());
        var fabricCounts = shipments
            .GroupBy(s => s.ProviderId)
            .Select(group => new
            {
                Provider = providerRepository.GetById(group.Key)?.Name ?? "Unknown",
                FabricCount = group.Select(s => s.FabricId).Distinct().Count()
            });

        return Ok(fabricCounts);
    }

    /// <summary>
    /// Get the top 5 fabrics by shipment count.
    /// </summary>
    [HttpGet("top-5-fabrics")]
    public async Task<ActionResult<IEnumerable<object>>> Top5FabricsByShipments()
    {
        var shipments = await Task.FromResult(shipmentRepository.GetAll());
        var topFabrics = shipments
            .GroupBy(s => s.FabricId)
            .Select(group => new
            {
                Fabric = fabricRepository.GetById(group.Key)?.Name ?? "Unknown",
                ShipmentCount = group.Count()
            })
            .OrderByDescending(x => x.ShipmentCount)
            .Take(5);

        return Ok(topFabrics);
    }

    /// <summary>
    /// Get providers with the maximum quantity of goods supplied within a specified period.
    /// </summary>
    [HttpGet("max-quantity-providers-by-period")]
    public async Task<ActionResult<IEnumerable<object>>> MaxProvidersByQuantity(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
            return BadRequest("Start date must be earlier than end date.");

        var shipments = await Task.FromResult(shipmentRepository.GetAll());
        var providerQuantities = shipments
            .Where(s => s.Date >= startDate && s.Date <= endDate)
            .GroupBy(s => s.ProviderId)
            .Select(group => new
            {
                Provider = providerRepository.GetById(group.Key)?.Name ?? "Unknown",
                TotalQuantity = group.Sum(s => s.NumberOfGoods)
            })
            .ToList();

        var maxQuantity = providerQuantities.Max(x => x.TotalQuantity);

        var maxProviders = providerQuantities
            .Where(x => x.TotalQuantity == maxQuantity);

        return Ok(maxProviders);
    }
}
