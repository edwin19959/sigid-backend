using Microsoft.AspNetCore.Mvc;
using SIGID.Core.Application.DTO.Purchases;
using SIGID.Core.Application.Interfaces.Services;

namespace SIGID.API.Controllers
{
    [Route("api/purchases")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchasesController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var purchases = await _purchaseService.GetAllAsync();
                return Ok(new { success = true, data = purchases });
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
                var purchase = await _purchaseService.GetByIdAsync(id);
                if (purchase == null) return NotFound(new { success = false, message = "Compra no encontrada" });
                return Ok(new { success = true, data = purchase });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseDTO dto)
        {
            try
            {
                var purchase = await _purchaseService.CreateAsync(dto);
                return Ok(new { success = true, data = purchase, message = "Compra registrada correctamente" });
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
                var result = await _purchaseService.DeleteAsync(id);
                if (!result) return NotFound(new { success = false, message = "Compra no encontrada" });
                return Ok(new { success = true, message = "Compra eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
