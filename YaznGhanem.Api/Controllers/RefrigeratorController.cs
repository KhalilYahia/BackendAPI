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
   
        public class RefrigeratorController : ApiBaseController
        {
            private readonly IRefrigeratorService _RefrigeratorServices;

            public RefrigeratorController(IRefrigeratorService repositoryServices)
            {
                _RefrigeratorServices = repositoryServices;
            }

            [HttpGet("GetAllRefrigerator")]
            public async Task<ActionResult<List<RefrigeratorSimplifyDto>>> GetAllRefrigerator()
            {
                var repositories = await _RefrigeratorServices.GetAllAsync();
                return Ok(repositories);
            }

            [HttpGet("GetRefrigeratorById")]
            public async Task<ActionResult<RefrigeratorDto>> GetRefrigeratorById(int id)
            {
                var repository = await _RefrigeratorServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddNewRefrigerator")]
            public async Task<ActionResult<int>> AddNewRefrigerator(InputRefrigeratorDto inDto)
            {
                var repositoryId = await _RefrigeratorServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteRefrigerator")]
            public async Task<ActionResult<bool>> DeleteRefrigerator(int id)
            {
                var result = await _RefrigeratorServices.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }

        [HttpGet("GetAllAsync_ForDesktop")]
        public async Task<ActionResult<List<RefrigeratorDto>>> GetAllAsync_ForDesktop(string hash)
        {
            var repositories = await _RefrigeratorServices.GetAllAsync_ForDesktop();
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
