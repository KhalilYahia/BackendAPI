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
   // [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class TotalFundsController : ApiBaseController
    {
        private readonly ITotalFundsService _totalFundsService;

        public TotalFundsController(ITotalFundsService totalFundsService)
        {
            _totalFundsService = totalFundsService;
        }

        [HttpGet("GetTotalFunds")]
        public async Task<ActionResult<TotalFundsDto>> GetTotalFunds()
        {
            var totalFunds = await _totalFundsService.GetAllAsync();
            return Ok(totalFunds);
        }

        [HttpGet("GetAllOpt")]
        public async Task<ActionResult<ReportDto>> GetAllOpt()
        {
            var totalFunds = await _totalFundsService.GetAllOpt();
            return Ok(totalFunds);
        }
        [HttpGet("GetAllOpt_UsingSQLView")]
        public async Task<ActionResult<ReportDto>> GetAllOpt_UsingSQLView()
        {
            var totalFunds = await _totalFundsService.GetAllOpt_UsingSQLView();
            return Ok(totalFunds);
        }
        [HttpGet("GetAllOpt_UsingSP")]
        public async Task<ActionResult<ReportDto>> GetAllOpt_UsingSP()
        {
            var totalFunds = await _totalFundsService.GetAllOpt_UsingSP();
            return Ok(totalFunds);
        }

        [HttpGet("GetAllOpt_ForDesktop")]
        public async Task<ActionResult<List<Report_DesktopDto>>> GetAllOpt_ForDesktop(string hash)
        {
            var repositories = await _totalFundsService.GetAllOpt_Fordesktop();
            var res_= new List<Report_DesktopDto>();
            res_.Add(repositories);
            var dataString = JsonConvert.SerializeObject(res_);
            var Newhash_ = Hash_kh.GetHash(JsonConvert.SerializeObject(dataString));
            if (Newhash_ == hash)
                return Ok(new { Data = "", Hash = Newhash_ });
            else
            {
                return Ok(new { Data = res_, Hash = Newhash_ });
            }
        }
        //[HttpPut("update")]
        //public async Task<ActionResult<bool>> update(TotalFundsDto dto)
        //{
        //    var totalFunds = await _totalFundsService.UpdateFunds(dto);
        //    return Ok(totalFunds);
        //}

    }
}
