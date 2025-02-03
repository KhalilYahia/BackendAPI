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
    public class FinancialEntitlementsController : ApiBaseController
    {
        private readonly IFinancialEntitlementService _financialEntitlementService;

        public FinancialEntitlementsController(IFinancialEntitlementService financialEntitlementService)
        {
            _financialEntitlementService = financialEntitlementService;
        }

        [HttpGet("GetAllFinancialEntitlements")]
        public async Task<ActionResult<List<AllFinancialEntitlementDto>>> GetAllFinancialEntitlements()
        {
            var entitlements = await _financialEntitlementService.GetAllFinancialEntitlementsAsync();
            return Ok(entitlements);
        }


        [HttpGet("GetAllAsync_ForDesktop")]
        public async Task<ActionResult<List<AllFinancialEntitlementDto>>> GetAllAsync_ForDesktop(string hash)
        {
            var repositories = await _financialEntitlementService.GetAllFinancialEntitlementsAsync();
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
