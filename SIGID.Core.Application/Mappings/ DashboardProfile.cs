using AutoMapper;
using SIGID.Core.Application.DTO.Dashboard;
using SIGID.Core.Domain.Entities;

namespace SIGID.Core.Application.Mappings
{
    public class DashboardProfile : Profile
    {
        public DashboardProfile()
        {
            CreateMap<Product, ProductStockBajoDTO>()
                .ForMember(dest => dest.DiferenciaStock,
                    opt => opt.MapFrom(src => src.CurrStock - src.StockMin));
        }
    }
}