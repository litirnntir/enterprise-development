using Microsoft.AspNetCore.Mvc;
using Fabrics.Domain;
using Fabrics.Domain.Repositories;

namespace Fabrics.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalyticsController : ControllerBase
{
    private readonly IRepository<Provider> _providerRepository;
    private readonly IRepository<Fabric> _fabricRepository;
    private readonly IRepository<Shipment> _shipmentRepository;

    public AnalyticsController(IRepository<Provider> providerRepository, IRepository<Fabric> fabricRepository, IRepository<Shipment> shipmentRepository)
    {
        _providerRepository = providerRepository;
        _fabricRepository = fabricRepository;
        _shipmentRepository = shipmentRepository;
    }

    /// <summary>
    /// Get the total number of shipments for each type of goods.
    /// </summary>
    /// <returns>List of goods types with shipment counts.</returns>
    [HttpGet("shipments-by-goods-type")]
    public async Task<ActionResult<IEnumerable<object>>> GetShipmentsByGoodsType()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());

        var result = shipments
            .GroupBy(s => _providerRepository.GetById(s.ProviderId)?.TypeOfGoods)
            .Select(group => new
            {
                GoodsType = group.Key,
                ShipmentCount = group.Count()
            })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Get the average number of goods supplied by each provider.
    /// </summary>
    /// <returns>List of providers with average supplied goods.</returns>
    [HttpGet("average-goods-by-provider")]
    public async Task<ActionResult<IEnumerable<object>>> GetAverageGoodsByProvider()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());

        var result = shipments
            .GroupBy(s => s.ProviderId)
            .Select(group => new
            {
                Provider = _providerRepository.GetById(group.Key),
                AverageGoods = group.Average(s => s.NumberOfGoods)
            })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Get fabrics with the highest number of shipments.
    /// </summary>
    /// <returns>List of top fabrics by shipment count.</returns>
    [HttpGet("top-fabrics-by-shipments")]
    public async Task<ActionResult<IEnumerable<object>>> GetTopFabricsByShipments()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());

        var result = shipments
            .GroupBy(s => s.FabricId)
            .Select(group => new
            {
                Fabric = _fabricRepository.GetById(group.Key),
                ShipmentCount = group.Count()
            })
            .OrderByDescending(x => x.ShipmentCount)
            .Take(10)
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Get the total area of all fabrics grouped by ownership form.
    /// </summary>
    /// <returns>List of ownership forms with total fabric area.</returns>
    [HttpGet("total-area-by-ownership")]
    public async Task<ActionResult<IEnumerable<object>>> GetTotalAreaByOwnership()
    {
        var fabrics = await Task.FromResult(_fabricRepository.GetAll());

        var result = fabrics
            .GroupBy(f => f.FormOfOwnership)
            .Select(group => new
            {
                OwnershipForm = group.Key,
                TotalArea = group.Sum(f => f.TotalSquare)
            })
            .ToList();

        return Ok(result);
    }

    /// <summary>
    /// Get the most active providers by total number of goods supplied.
    /// </summary>
    /// <returns>List of providers with the most goods supplied.</returns>
    [HttpGet("most-active-providers")]
    public async Task<ActionResult<IEnumerable<object>>> GetMostActiveProviders()
    {
        var shipments = await Task.FromResult(_shipmentRepository.GetAll());

        var result = shipments
            .GroupBy(s => s.ProviderId)
            .Select(group => new
            {
                Provider = _providerRepository.GetById(group.Key),
                TotalGoods = group.Sum(s => s.NumberOfGoods)
            })
            .OrderByDescending(x => x.TotalGoods)
            .Take(5)
            .ToList();

        return Ok(result);
    }
}
