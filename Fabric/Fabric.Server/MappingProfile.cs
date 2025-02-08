using AutoMapper;
using Fabrics.Domain;
using Fabrics.Server.Dto;

namespace Fabrics.Server;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Factory, FabricGetDto>();
        CreateMap<Factory, FabricPostDto>();
        CreateMap<Provider, ProviderGetDto>();
        CreateMap<Provider, ProviderPostDto>();
        CreateMap<Shipment, ShipmentGetDto>();
        CreateMap<Shipment, ShipmentPostDto>();

        CreateMap<FabricPostDto, Factory>();
        CreateMap<ProviderPostDto, Provider>();
        CreateMap<ShipmentPostDto, Shipment>();
    }
}