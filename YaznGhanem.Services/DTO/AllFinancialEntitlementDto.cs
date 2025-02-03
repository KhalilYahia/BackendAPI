using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class AllFinancialEntitlementDto
    {
        // Unique identifier for the entitlement
        public int Id { get; set; }

        /// <summary>
        /// المعلومات الخاصة بالمورد
        /// </summary>
        public int SupplierId { get; set; }

        public string SupplierName { get; set; }

        // Amount of the entitlement
        public decimal TotalAmount { get; set; }

        // مقدار النقود المستوفية
        public decimal TotalPayments { get; set; }
        /// <summary>
        /// الباقي
        /// </summary>
        public decimal Remainder { get; set; }

        /// <summary>
        /// العمليات التي تشكل الكمية لاجمالية
        /// </summary>
        public List<string> Operations { get; set; }

    }
}
