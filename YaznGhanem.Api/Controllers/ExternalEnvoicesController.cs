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
   
        public class ExternalEnvoicesController : ApiBaseController
        {
            private readonly IExternalEnvoicesService _ExternalEnvoicesServices;

            public ExternalEnvoicesController(IExternalEnvoicesService repositoryServices)
            {
                _ExternalEnvoicesServices = repositoryServices;
            }

            [HttpGet("GetAllExternalEnvoices")]
            public async Task<ActionResult<List<ExternalEnvoicesSimplifyDto>>> GetAllExternalEnvoices()
            {
                var repositories = await _ExternalEnvoicesServices.GetAllAsync();
                return Ok(repositories);
            }

            [HttpGet("GetExternalEnvoicesById")]
            public async Task<ActionResult<ExternalEnvoicesDetailsDto>> GetExternalEnvoicesById(int id)
            {
                var repository = await _ExternalEnvoicesServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddNewExternalEnvoices")]
            public async Task<ActionResult<int>> AddNewExternalEnvoices(InputExternalEnvoicesDto inDto)
            {
                var repositoryId = await _ExternalEnvoicesServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteExternalEnvoices")]
            public async Task<ActionResult<bool>> DeleteExternalEnvoices(int id)
            {
                var result = await _ExternalEnvoicesServices.DeleteAsync(id);
                if (!result)
                {
                    return Ok(result);
                }
                return Ok(result);
            }

            [HttpGet("GetAllDailies_Fordesktop")]
            public async Task<ActionResult<List<ExternalEnvoicesDetailsDto>>> GetAllDailies_Fordesktop(string hash)
            {
                var repositories = await _ExternalEnvoicesServices.GetAllAsync_Fordesktop();
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
