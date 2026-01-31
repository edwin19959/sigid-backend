using SIGID.Core.Application.DTO.Dashboard;

namespace SIGID.Core.Application.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardStatsDTO> GetDashboardStatsAsync();
        Task<List<ProductStockBajoDTO>> GetProductosStockBajoAsync();
    }
}
