using Microsoft.AspNetCore.Mvc;
using SIGID.Core.Application.Interfaces.Services;

namespace SIGID.API.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                var stats = await _dashboardService.GetDashboardStatsAsync();
                return Ok(new { success = true, data = stats, message = "Estadísticas obtenidas correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error al obtener las estadísticas del dashboard", error = ex.Message });
            }
        }

        [HttpGet("productos-stock-bajo")]
        public async Task<IActionResult> GetProductosStockBajo()
        {
            try
            {
                var productos = await _dashboardService.GetProductosStockBajoAsync();
                return Ok(new { success = true, data = productos, count = productos.Count, message = productos.Count > 0 ? "Productos con stock bajo obtenidos correctamente" : "No hay productos con stock bajo en este momento" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error al obtener productos con stock bajo", error = ex.Message });
            }
        }
    }
}
