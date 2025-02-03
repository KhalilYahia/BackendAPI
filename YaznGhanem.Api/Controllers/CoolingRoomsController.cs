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
   
        public class CoolingRoomsController : ApiBaseController
        {
            private readonly ICoolingRoomsService _CoolingRoomsServices;

            public CoolingRoomsController(ICoolingRoomsService repositoryServices)
            {
                _CoolingRoomsServices = repositoryServices;
            }

            [HttpGet("GetAllCoolingRooms")]
            public async Task<ActionResult<List<CoolingRoomsSimplifyDto>>> GetAllCoolingRooms()
            {
                var repositories = await _CoolingRoomsServices.GetAllAsync();
                return Ok(repositories);
            }

            [HttpGet("GetCoolingRoomsById")]
            public async Task<ActionResult<CoolingRoomsDetailsDto>> GetCoolingRoomsById(int id)
            {
                var repository = await _CoolingRoomsServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddNewCoolingRooms")]
            public async Task<ActionResult<int>> AddNewCoolingRooms(InputCoolingRoomsDto inDto)
            {
                var repositoryId = await _CoolingRoomsServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteCoolingRooms")]
            public async Task<ActionResult<bool>> DeleteCoolingRooms(int id)
            {
                var result = await _CoolingRoomsServices.DeleteAsync(id);
                if (!result)
                {
                    return Ok(result);
                }
                return Ok(result);
            }

        [HttpGet("GetAllAsync_ForDesktop")]
        public async Task<ActionResult<List<CoolingRoomsDetailsDto>>> GetAllAsync_ForDesktop(string hash)
        {
            var repositories = await _CoolingRoomsServices.GetAllAsync_Fordesktop();
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
