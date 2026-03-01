using Microsoft.AspNetCore.Mvc;
using SIGID.Core.Application.DTO.Sales;
using SIGID.Core.Application.Interfaces.Services;

namespace SIGID.API.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SalesController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sales = await _saleService.GetAllAsync();
                return Ok(new { success = true, data = sales });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var sale = await _saleService.GetByIdAsync(id);
                if (sale == null) return NotFound(new { success = false, message = "Venta no encontrada" });
                return Ok(new { success = true, data = sale });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] CreateSaleDTO dto)
        {
            try
            {
                var sale = await _saleService.CreateAsync(dto);
                return Ok(new { success = true, data = sale, message = "Venta registrada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _saleService.DeleteAsync(id);
                if (!result) return NotFound(new { success = false, message = "Venta no encontrada" });
                return Ok(new { success = true, message = "Venta eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
