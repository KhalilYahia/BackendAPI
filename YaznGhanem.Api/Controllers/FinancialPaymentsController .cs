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
    public class FinancialPaymentsController : ApiBaseController
    {
        private readonly IFinancialPaymentService _financialPaymentService;

        public FinancialPaymentsController(IFinancialPaymentService financialPaymentService)
        {
            _financialPaymentService = financialPaymentService;
        }

        [HttpGet("GetAllFinancialPayments")]
        public async Task<ActionResult<List<FinancialPaymentDto>>> GetAllFinancialPayments(int EntitlementId)
        {
            var payments = await _financialPaymentService.GetAllFinancialPaymentsAsync(EntitlementId);
            return Ok(payments);
        }

        [HttpPost("AddFinancialPayment")]
        public async Task<ActionResult<int>> AddFinancialPayment(FinancialPaymentDto newPayment)
        {
            var paymentId = await _financialPaymentService.AddFinancialPaymentAsync(newPayment);
            return paymentId;
        }

        [HttpOptions("DeleteFinancialPayment")]
        public async Task<ActionResult<bool>> DeleteFinancialPayment(int id)
        {
            var result = await _financialPaymentService.DeleteFinancialPaymentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
