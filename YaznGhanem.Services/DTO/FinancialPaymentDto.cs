using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class FinancialPaymentDto
    {
        public int Id { get; set; }

        // Identifier linking to the corresponding entitlement
        public int EntitlementId { get; set; }

        // Amount paid
        public decimal AmountPayment { get; set; }

        // Date of payment
        public DateTime PaymentDate { get; set; }


        // Optional: Any additional notes or comments
        public string Notes { get; set; }
    }
}
