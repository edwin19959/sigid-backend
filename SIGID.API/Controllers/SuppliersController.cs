using Microsoft.AspNetCore.Mvc;
using SIGID.Core.Application.DTO.Suppliers;
using SIGID.Core.Application.Interfaces.Services;

namespace SIGID.API.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var suppliers = await _supplierService.GetAllAsync();
                return Ok(new { success = true, data = suppliers });
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
                var supplier = await _supplierService.GetByIdAsync(id);
                if (supplier == null) return NotFound(new { success = false, message = "Suplidor no encontrado" });
                return Ok(new { success = true, data = supplier });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDTO dto)
        {
            try
            {
                var supplier = await _supplierService.CreateAsync(dto);
                return Ok(new { success = true, data = supplier, message = "Suplidor creado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSupplierDTO dto)
        {
            try
            {
                var supplier = await _supplierService.UpdateAsync(id, dto);
                if (supplier == null) return NotFound(new { success = false, message = "Suplidor no encontrado" });
                return Ok(new { success = true, data = supplier, message = "Suplidor actualizado correctamente" });
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
                var result = await _supplierService.DeleteAsync(id);
                if (!result) return NotFound(new { success = false, message = "Suplidor no encontrado" });
                return Ok(new { success = true, message = "Suplidor eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
