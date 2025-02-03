using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using YaznGhanem.Services.Iservices;
using LLama;
using LLama.Common;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Hosting;
using NuGet.Packaging.Signing;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Authorization;



namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ApiBaseController
    {
        private readonly ISupplierService _supplierService;
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        // POST: api/Supplier
        [HttpPost("AddSupplier")]
        public async Task<ActionResult<int>> AddSupplier(SupplierDto supplierDto)
        {
            var supplierId = await _supplierService.AddAsync(supplierDto);
            return CreatedAtAction(nameof(GetSupplierById), new { id = supplierId }, supplierId);
        }

        // PUT: api/Supplier
        [HttpPut("UpdateSupplier")]
        public async Task<ActionResult<bool?>> UpdateSupplier(SupplierDto supplierDto)
        {
            var result = await _supplierService.UpdateAsync(supplierDto);
            return result;
        }

        // DELETE: api/Supplier/{id}
        [HttpOptions("DeleteSupplier")]
        public async Task<ActionResult<bool?>> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteAsync(id);
            return result;
        }

        // GET: api/Supplier
        [HttpGet("GetAllSuppliers")]
        public async Task<ActionResult<List<SupplierDto>>> GetAllSuppliers()
        {
            var suppliers = await _supplierService.GetAllAsync();
            return Ok(suppliers);
        }

        // GET: api/Supplier/{id}
        [HttpGet("GetSupplierById")]
        public async Task<ActionResult<SupplierDto>> GetSupplierById(int id)
        {
            var supplier = await _supplierService.GetByIdAsync(id);
            if (supplier != null)
            {
                return Ok(supplier);
            }
            return NotFound();
        }


    }
}
