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
   
        public class RepositoryInOutController : ApiBaseController
        {
            private readonly IRepositoryInOutServices _repositoryServices;

            public RepositoryInOutController(IRepositoryInOutServices repositoryServices)
            {
                _repositoryServices = repositoryServices;
            }

            [HttpGet("GetAllRepositories")]
            public async Task<ActionResult<List<Ripository_InSimplifyDto>>> GetAllRepositories()
            {
                var repositories = await _repositoryServices.GetAllAsync();
                return Ok(repositories);
            }

            [HttpGet("GetRepositoryById")]
            public async Task<ActionResult<Ripository_InDetailsDto>> GetRepositoryById(int id)
            {
                var repository = await _repositoryServices.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpPost("AddRepository")]
            public async Task<ActionResult<int>> AddRepository(Input_RepositoryInDto inDto)
            {
                var repositoryId = await _repositoryServices.AddAsync(inDto);
                return repositoryId;
            }

            [HttpOptions("DeleteRepository")]
            public async Task<ActionResult<bool>> DeleteRepository(int id)
            {
                var result = await _repositoryServices.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }


        [HttpGet("GetAll_Desktop")]
        public async Task<ActionResult<List<Ripository_InDetailsDto>>> GetAll_Desktop(string hash)
        {
            hash = Uri.UnescapeDataString(hash);
            var repositories = await _repositoryServices.GetAll_Desktop();

            var dataString = JsonConvert.SerializeObject(repositories);
            string Newhash_ = Hash_kh.GetHash(JsonConvert.SerializeObject(dataString));
            if (Newhash_ == hash)
            {
                return Ok(new { Data = "", Hash = Newhash_ });
            } 
            else
            {
                return Ok(new { Data = repositories, Hash = Newhash_ });
            }

        }

      
    }


}
