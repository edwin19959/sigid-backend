using AutoMapper;
using SIGID.Core.Application.DTO.Inventory;
using SIGID.Core.Domain.Entities;

namespace SIGID.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            // Mapeo para productos con stock bajo
            CreateMap<Product, LowStockProductDTO>()
                .ForMember(dest => dest.StockDifference,
                    opt => opt.MapFrom(src => src.CurrStock - src.StockMin));
        }
    }
}