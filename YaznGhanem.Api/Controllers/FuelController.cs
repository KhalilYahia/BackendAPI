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
   
        public class FuelController : ApiBaseController
        {
            private readonly IFuelService _FuelServices;

            public FuelController(IFuelService repositoryServices)
            {
                _FuelServices = repositoryServices;
            }

            [HttpGet("GetAllFuels")]
            public async Task<ActionResult<List<Fuel_InSimplifyDto>>> GetAllFuels()
            {
                var repositories = await _FuelServices.GetAllAsync();
                return Ok(repositories);
            }

            [HttpGet("GetFuelById")]
            public async Task<ActionResult<Fuel_InDetailsDto>> GetFuelById(int id)
            {
                var repository = await _FuelServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddNewFuel")]
            public async Task<ActionResult<int>> AddNewFuel(Input_FuelDto inDto)
            {
                var repositoryId = await _FuelServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteFuel")]
            public async Task<ActionResult<bool>> DeleteFuel(int id)
            {
                var result = await _FuelServices.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }

        [HttpGet("GetAllAsync_ForDesktop")]
        public async Task<ActionResult<List<Fuel_InDetailsDto>>> GetAllAsync_ForDesktop(string hash)
        {
            var repositories = await _FuelServices.GetAllAsync_ForDesktop();
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
