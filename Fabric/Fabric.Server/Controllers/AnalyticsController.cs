using Microsoft.AspNetCore.Mvc;
using Fabrics.Domain;
using Fabrics.Domain.Repositories;
using Fabrics.Server.Dto;

namespace Fabrics.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly IRepository<Provider> _providerRepository;
    private readonly IRepository<Factory> _fabricRepository;
    private readonly IRepository<Shipment> _shipmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnalyticsController"/> class.
    /// </summary>
    public AnalyticsController(IRepository<Provider> providerRepository, IRepository<Factory> fabricRepository, IRepository<Shipment> shipmentRepository)
    {
        _providerRepository = providerRepository;
        _fabricRepository = fabricRepository;
        _shipmentRepository = shipmentRepository;
    }

    /// <summary>
    /// Retrieves the number of shipments grouped by the type of goods.
    /// </summary>
    /// <returns>List of shipments grouped by goods type.</returns>
    [HttpGet("shipments-by-goods-type")]
    [ProducesResponseType(typeof(IEnumerable<object>), 200)]
    public async Task<ActionResult<IEnumerable<object>>> GetShipmentsByGoodsType()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());
        var result = shipments
            .GroupBy(s => _providerRepository.GetById(s.ProviderId)?.TypeOfGoods)
            .Select(group => new { GoodsType = group.Key ?? "Unknown", ShipmentCount = group.Count() })
            .ToList();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the average number of goods shipped by each provider.
    /// </summary>
    /// <returns>List of providers with their average shipped goods.</returns>
    [HttpGet("average-goods-by-provider")]
    [ProducesResponseType(typeof(IEnumerable<object>), 200)]
    public async Task<ActionResult<IEnumerable<object>>> GetAverageGoodsByProvider()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());
        var result = shipments
            .GroupBy(s => s.ProviderId)
            .Select(group =>
            {
                var provider = _providerRepository.GetById(group.Key);
                if (provider == null) return null;
                return new { Provider = new ProviderGetDto { Id = provider.Id, Name = provider.Name, TypeOfGoods = provider.TypeOfGoods, Address = provider.Address }, AverageGoods = group.Average(s => s.NumberOfGoods) };
            })
            .Where(x => x != null)
            .ToList();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the top 10 factories by the number of shipments received.
    /// </summary>
    /// <returns>List of top 10 factories with shipment count.</returns>
    [HttpGet("top-fabrics-by-shipments")]
    [ProducesResponseType(typeof(IEnumerable<object>), 200)]
    public async Task<ActionResult<IEnumerable<object>>> GetTopFabricsByShipments()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());
        var result = shipments
            .GroupBy(s => s.FabricId)
            .Select(group =>
            {
                var fabric = _fabricRepository.GetById(group.Key);
                if (fabric == null) return null;
                return new { Fabric = new FabricGetDto { Id = fabric.Id, Type = fabric.Type, Name = fabric.Name, Address = fabric.Address, PhoneNumber = fabric.PhoneNumber, FormOfOwnership = fabric.FormOfOwnership, NumberOfWorkers = fabric.NumberOfWorkers, TotalSquare = fabric.TotalSquare }, ShipmentCount = group.Count() };
            })
            .Where(x => x != null)
            .OrderByDescending(x => x.ShipmentCount)
            .Take(10)
            .ToList();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the total area of factories grouped by ownership form.
    /// </summary>
    /// <returns>List of ownership forms with their total area.</returns>
    [HttpGet("total-area-by-ownership")]
    [ProducesResponseType(typeof(IEnumerable<object>), 200)]
    public async Task<ActionResult<IEnumerable<object>>> GetTotalAreaByOwnership()
    {
        var fabrics = await Task.FromResult(_fabricRepository.GetAll());
        var result = fabrics
            .GroupBy(f => f.FormOfOwnership)
            .Select(group => new { OwnershipForm = group.Key ?? "Unknown", TotalArea = group.Sum(f => f.TotalSquare) })
            .ToList();
        return Ok(result);
    }

    /// <summary>
    /// Retrieves the top 5 most active providers based on the total number of goods shipped.
    /// </summary>
    /// <returns>List of top 5 active providers.</returns>
    [HttpGet("most-active-providers")]
    [ProducesResponseType(typeof(IEnumerable<object>), 200)]
    public async Task<ActionResult<IEnumerable<object>>> GetMostActiveProviders()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());
        var result = shipments
            .GroupBy(s => s.ProviderId)
            .Select(group =>
            {
                var provider = _providerRepository.GetById(group.Key);
                if (provider == null) return null;
                return new { Provider = new ProviderGetDto { Id = provider.Id, Name = provider.Name, TypeOfGoods = provider.TypeOfGoods, Address = provider.Address }, TotalGoods = group.Sum(s => s.NumberOfGoods) };
            })
            .Where(x => x != null)
            .OrderByDescending(x => x.TotalGoods)
            .Take(5)
            .ToList();
        return Ok(result);
    }
}
