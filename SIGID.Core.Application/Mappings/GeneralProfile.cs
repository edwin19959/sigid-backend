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
                .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.CurrStock))
                .ForMember(dest => dest.MinimumStock, opt => opt.MapFrom(src => src.StockMin))
                .ForMember(dest => dest.StockDifference, opt => opt.MapFrom(src => src.CurrStock - src.StockMin))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description ?? ""))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        }
    }
}
