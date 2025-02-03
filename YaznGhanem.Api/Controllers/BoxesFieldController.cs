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
    [Authorize(Roles = "Admin,NormalUser")]
    [ApiController]
    [Route("[controller]")]
   
        public class BoxesFieldController : ApiBaseController
        {
            private readonly IBoxesFieldService _BoxesFieldService;

            public BoxesFieldController(IBoxesFieldService IBoxesFieldService)
            {
                 _BoxesFieldService = IBoxesFieldService;
            }

            [HttpGet("GetBoFOperationsPage_ByUserId")]
            public async Task<ActionResult<GetAllBoF_OperationsDto>> GetBoFOperationsPage_ByUserId(int userId)
            {
                var repositories = await _BoxesFieldService.GetBoFOperationsPage_ByUserId(userId);
                return Ok(repositories);
            }
          

            [HttpGet("GetBoFMainPage")]
            public async Task<ActionResult<GetAllBoF_MainDto>> GetBoFMainPage()
            {
                var repositories = await _BoxesFieldService.GetBoFMainPage();
                return Ok(repositories);
            }
       
            [HttpGet("GetOperationDetailed")]
            public async Task<ActionResult<BoF_DetailedDto>> GetOperationDetailed(int id)
            {
                var repository = await _BoxesFieldService.GetByIdAsync(id);
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }

            [HttpGet("GetBoF_Info")]
            public async Task<ActionResult<BoF_InfoDTO>> GetBoF_Info()
            {
                var repository = await _BoxesFieldService.GetBoF_Info();
                if (repository == null)
                {
                    return NotFound();
                }
                return Ok(repository);
            }
        

             [HttpPost("AddAsync")]
            public async Task<ActionResult<int>> AddAsync(InputBoxesDto inDto)
            {
                var repositoryId = await _BoxesFieldService.AddAsync(inDto);
                return repositoryId;
            }
      
            [HttpOptions("DeleteAsync")]
            public async Task<ActionResult<bool>> DeleteAsync(int id)
            {
                var result = await _BoxesFieldService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }


        //[HttpGet("GetAllAsync_ForDesktop")]
        //public async Task<ActionResult<List<Cars_InDetailsDto>>> GetAllAsync_ForDesktop(string hash)
        //{
        //    var repositories = await _CarsServices.GetAllAsync_ForDesktop();
        //    var dataString = JsonConvert.SerializeObject(repositories);
        //    var Newhash_ = Hash_kh.GetHash(JsonConvert.SerializeObject(dataString));
        //    if (Newhash_ == hash)
        //        return Ok(new { Data = "", Hash = Newhash_ });
        //    else
        //    {
        //        return Ok(new { Data = repositories, Hash = Newhash_ });
        //    }
        //}


    }


}
