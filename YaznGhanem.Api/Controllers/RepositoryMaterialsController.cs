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
    public class RepositoryMaterialsController : ApiBaseController
    {
        private readonly IRepositoryMaterialsService _repositoryMaterialsService;
        private readonly ISupplierService _supplierService;
        private readonly IBuyerService _BuyerService;
        private readonly IEmployeeService _EmployeeService;
        public RepositoryMaterialsController(IRepositoryMaterialsService repositoryMaterialsService, IEmployeeService EmployeeService, ISupplierService supplierService, IBuyerService buyerService)
        {
            _repositoryMaterialsService = repositoryMaterialsService;
            _supplierService = supplierService;
            _BuyerService = buyerService;
            _EmployeeService = EmployeeService;

        }

        [HttpGet("GetAllMaterials")]
        public async Task<ActionResult<List<RepositoryMaterialsDto>>> GetAllMaterials()
        {
            var materials = await _repositoryMaterialsService.GetAllAsync();
            return Ok(materials);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<GetAllDto>> GetAll()
        {
            var materials = await _repositoryMaterialsService.GetAllAsync();
            var suppliers = await _supplierService.GetAllAsync();
            var buyers = await _BuyerService.GetAllAsync();
            var employees = await _EmployeeService.GetEmployees("");
            var SOFarms = await _supplierService.GetAllSupplierOfFarmsAsync();
            
            var res_ =new GetAllDto { Materials = materials, Suppliers = suppliers,Buyers= buyers,Employees= employees, SOFarms= SOFarms };
            return Ok(res_);
        }
        [HttpGet("GetAllByCategoryId")]
        public async Task<ActionResult<GetAllDto>> GetAllByCategoryId(int catId)
        {
            var materials = await _repositoryMaterialsService.GetAllByCategoryId(catId);
            var suppliers = await _supplierService.GetAllAsync();
            var buyers = await _BuyerService.GetAllAsync();
            var SOFarms = await _supplierService.GetAllSupplierOfFarmsAsync();
            var res_ = new GetAllDto { Materials = materials, Suppliers = suppliers, Buyers = buyers, SOFarms= SOFarms };
            return Ok(res_);
        }
        //
        [HttpGet("GetMaterialById")]
        public async Task<ActionResult<RepositoryMaterialsDto>> GetMaterialById(int id)
        {
            var material = await _repositoryMaterialsService.GetByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }

        [HttpPost("AddMaterial")]
        public async Task<ActionResult<int>> AddMaterial(RepositoryMaterialsDto materialDto)
        {
            var materialId = await _repositoryMaterialsService.AddAsync(materialDto);
            return materialId;
        }

        [HttpPut("UpdateMaterial")]
        public async Task<ActionResult<bool>> UpdateMaterial(RepositoryMaterialsDto materialDto)
        {
            var result = await _repositoryMaterialsService.UpdateAsync(materialDto);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpOptions("DeleteMaterial")]
        public async Task<ActionResult<bool>> DeleteMaterial(int id)
        {
            var result = await _repositoryMaterialsService.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
