using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class Cars
    {
        public int Id { set; get; }

        public string DriverName { get; set; }
        public int DriverId { get; set; }
        public virtual Supplier Driver { set; get; }

        public int LoadsPerDay { get; set; }
        public decimal PriceOfOne { get; set; }
        public decimal TotalPrice { get; set; }        
              

        public int EntitlementId { get; set; }
        public virtual FinancialEntitlement FinancialEntitlement { get; set; }

        public DateTime Date { get; set; }
        public string Notes { get; set; }
              
    }
}
