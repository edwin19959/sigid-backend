using Microsoft.AspNetCore.Mvc;
using SIGID.Core.Application.Interfaces.Services;

namespace SIGID.API.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetInventoryStats()
        {
            try
            {
                var stats = await _inventoryService.GetInventoryStatsAsync();
                return Ok(new { success = true, data = stats, message = "Estadísticas obtenidas correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error al obtener las estadísticas", error = ex.Message });
            }
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            try
            {
                var productos = await _inventoryService.GetLowStockProductsAsync();
                return Ok(new { success = true, data = productos, count = productos.Count, message = productos.Count > 0 ? "Productos con stock bajo obtenidos correctamente" : "No hay productos con stock bajo en este momento" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error al obtener productos con stock bajo", error = ex.Message });
            }
        }
    }
}