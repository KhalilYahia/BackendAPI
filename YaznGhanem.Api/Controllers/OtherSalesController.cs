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
using Newtonsoft.Json;
using YaznGhanem.Services.services;
using YaznGhanem.WebApi;
using Microsoft.AspNetCore.Authorization;



namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
   
        public class OtherSalesController : ApiBaseController
        {
            private readonly IOtherSalesService _OtherSalesServices;

            public OtherSalesController(IOtherSalesService repositoryServices)
            {
                _OtherSalesServices = repositoryServices;
            }

            [HttpGet("GetAllOtherSales")]
            public async Task<ActionResult<List<OtherSalesSimplifyDto>>> GetAllOtherSales()
            {
                var repositories = await _OtherSalesServices.GetAllAsync();
                return Ok(repositories);
            }

            [HttpGet("GetOtherSalesById")]
            public async Task<ActionResult<OtherSalesDetailsDto>> GetOtherSalesById(int id)
            {
                var repository = await _OtherSalesServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddNewOtherSales")]
            public async Task<ActionResult<int>> AddNewOtherSales(InputOtherSalesDto inDto)
            {
                var repositoryId = await _OtherSalesServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteOtherSales")]
            public async Task<ActionResult<bool>> DeleteOtherSales(int id)
            {
                var result = await _OtherSalesServices.DeleteAsync(id);
                if (!result)
                {
                    return Ok(result);
                }
                return Ok(result);
            }

            [HttpGet("GetAllDailies_Fordesktop")]
            public async Task<ActionResult<List<OtherSalesDetailsDto>>> GetAllDailies_Fordesktop(string hash)
            {
                var repositories = await _OtherSalesServices.GetAllAsync_Fordesktop();
                var dataString = JsonConvert.SerializeObject(repositories);
                var Newhash_ = Hash_kh.GetHash(JsonConvert.SerializeObject(dataString));
                if (Newhash_ == hash)
                    return Ok(new { Data = "", Hash = Newhash_ });
                else
                {
                    return Ok(new { Data = repositories, Hash = Newhash_ });
                }
            }
    }


}
