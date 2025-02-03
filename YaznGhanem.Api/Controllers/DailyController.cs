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
using YaznGhanem.WebApi;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;



namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
   
        public class DailyController : ApiBaseController
        {
            private readonly IDailyService _dailyServices;

            public DailyController(IDailyService repositoryServices)
            {
                _dailyServices = repositoryServices;
            }

            [HttpGet("GetAllDailies")]
            public async Task<ActionResult<List<DailySimplifyDto>>> GetAllDailies(string hash)
            {
                var repositories = await _dailyServices.GetAllAsync();
                var dataString = JsonConvert.SerializeObject(repositories);
                var Newhash_ = Hash_kh.GetHash(JsonConvert.SerializeObject(dataString));
                if(Newhash_==hash)
                    return Ok(new { Data = "", Hash = Newhash_ });
                else
                {
                    return Ok(new { Data = repositories, Hash = Newhash_ });
                 }
            }
       

            [HttpGet("GetDailyById")]
            public async Task<ActionResult<DailyDetailsDto>> GetDailyById(int id)
            {
                var repository = await _dailyServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddNewDaily")]
            public async Task<ActionResult<int>> AddNewDaily(InputDailyDto inDto)
            {
                var repositoryId = await _dailyServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpPost("UpdateDaily")]
            public async Task<ActionResult<int>> UpdateDaily(InputDailyDto inDto)
            {
                var repositoryId = await _dailyServices.UpdateAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteDaily")]
            public async Task<ActionResult<bool>> DeleteDaily(int id)
            {
                var result = await _dailyServices.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }

        [HttpGet("GetAllDailies_Fordesktop")]
        public async Task<ActionResult<List<DailyDetailsDto>>> GetAllDailies_Fordesktop(string hash)
        {
            var repositories = await _dailyServices.GetAllAsync_Fordesktop();
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
