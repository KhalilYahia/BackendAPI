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
using Microsoft.AspNetCore.Authorization;



namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class BuyerController : ApiBaseController
    {
        private readonly IBuyerService _BuyerService;
        public BuyerController(IBuyerService BuyerService)
        {
            _BuyerService = BuyerService;
        }

        // POST: api/Buyer
        [HttpPost("AddBuyer")]
        public async Task<ActionResult<int>> AddBuyer(BuyerDto BuyerDto)
        {
            var BuyerId = await _BuyerService.AddAsync(BuyerDto);
            return CreatedAtAction(nameof(GetBuyerById), new { id = BuyerId }, BuyerId);
        }

        // PUT: api/Buyer
        [HttpPut("UpdateBuyer")]
        public async Task<ActionResult<bool?>> UpdateBuyer(BuyerDto BuyerDto)
        {
            var result = await _BuyerService.UpdateAsync(BuyerDto);
            return result;
        }

        // DELETE: api/Buyer/{id}
        [HttpOptions("DeleteBuyer")]
        public async Task<ActionResult<bool?>> DeleteBuyer(int id)
        {
            var result = await _BuyerService.DeleteAsync(id);
            return result;
        }

        // GET: api/Buyer
        [HttpGet("GetAllBuyers")]
        public async Task<ActionResult<List<BuyerDto>>> GetAllBuyers()
        {
            var Buyers = await _BuyerService.GetAllAsync();
            return Ok(Buyers);
        }

        // GET: api/Buyer/{id}
        [HttpGet("GetBuyerById")]
        public async Task<ActionResult<BuyerDto>> GetBuyerById(int id)
        {
            var Buyer = await _BuyerService.GetByIdAsync(id);
            if (Buyer != null)
            {
                return Ok(Buyer);
            }
            return NotFound();
        }


    }
}
