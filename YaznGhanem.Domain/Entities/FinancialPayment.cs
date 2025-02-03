using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class FinancialPayment
    {
        // Unique identifier for the payment
        public int Id { get; set; }

        // Identifier linking to the corresponding entitlement
        public int EntitlementId { get; set; }
        public virtual FinancialEntitlement FinancialEntitlement { get; set; }

        // Amount paid
        public decimal AmountPayment { get; set; }

        // Date of payment
        public DateTime PaymentDate { get; set; }

       

        // Optional: Any additional notes or comments
        public string Notes { get; set; }


    }
}
