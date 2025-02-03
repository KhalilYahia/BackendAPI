using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface IFinancialPaymentService
    {
        // Add a new FinancialPayment
        Task<int> AddFinancialPaymentAsync(FinancialPaymentDto newPayment);

        /// <summary>
        /// Deletes a financial payment by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the financial payment to be deleted.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a boolean value indicating whether the deletion was successful.
        /// </returns>
        Task<bool> DeleteFinancialPaymentAsync(int id);

        // Get all FinancialPayments
        Task<List<FinancialPaymentDto>> GetAllFinancialPaymentsAsync(int EntitlementId);

    }
}
