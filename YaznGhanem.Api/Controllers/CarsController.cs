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
   
        public class CarsController : ApiBaseController
        {
            private readonly ICarsService _CarsServices;

            public CarsController(ICarsService repositoryServices)
            {
                _CarsServices = repositoryServices;
            }

            [HttpGet("GetAllCarss")]
            public async Task<ActionResult<List<Cars_InSimplifyDto>>> GetAllCarss()
            {
                var repositories = await _CarsServices.GetAllAsync();
                return Ok(repositories);
            }

            [HttpGet("GetCarsById")]
            public async Task<ActionResult<Cars_InDetailsDto>> GetCarsById(int id)
            {
                var repository = await _CarsServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddNewCars")]
            public async Task<ActionResult<int>> AddNewCars(Input_CarDto inDto)
            {
                var repositoryId = await _CarsServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteCars")]
            public async Task<ActionResult<bool>> DeleteCars(int id)
            {
                var result = await _CarsServices.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }


        [HttpGet("GetAllAsync_ForDesktop")]
        public async Task<ActionResult<List<Cars_InDetailsDto>>> GetAllAsync_ForDesktop(string hash)
        {
            var repositories = await _CarsServices.GetAllAsync_ForDesktop();
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
