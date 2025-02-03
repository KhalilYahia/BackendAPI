using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    /// <summary>
    /// المستحقات المالية
    /// </summary>
    public class FinancialEntitlement
    {
        // Unique identifier for the entitlement
        public int Id { get; set; }

        /// <summary>
        /// المعلومات الخاصة بالمورد
        /// </summary>
        public int? SupplierId { get; set; }

        public string SupplierName { get; set; }
        public virtual Supplier Supplier { get; set; }

        public int? SupplierOfFarmId { get; set; }
        public virtual SupplierOfFarms SupplierOfFarms { get; set; }

        // Amount of the entitlement
        public decimal TotalAmount { get; set; }
        // مقدار النقود المستوفية
        public decimal TotalPayments { get; set; }
        /// <summary>
        /// الباقي
        /// </summary>
        public decimal Remainder { get; set; }

        public DateTime Date { get; set; }
         

        // Optional: Any additional notes or comments
        public string Notes { get; set; }


        public virtual ICollection<FinancialPayment> Paymenties { get; set; }
        public virtual ICollection<Repository_InOut> Repository_Ins { get; set; }
        public virtual ICollection<Fuel> Fuels { get; set; }
        public virtual ICollection<Cars> Cars { get; set; }
        public virtual ICollection<Daily> Dailies { get; set; }
        public virtual ICollection<Daily> SupplierOfFarmsDailies { get; set; }
        public virtual ICollection<Daily> WaxingFactoryDailies { get; set; }
    }
}
