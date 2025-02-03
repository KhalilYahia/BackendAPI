using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class Fuel
    {
        public int Id { set; get; }

        public string SourceName { get; set; }
        public int SourceId { get; set; }
        public virtual Supplier Source { set; get; }

        public string Type { get; set; }

        public decimal Amount { get; set; }
        public decimal PriceOfOne { get; set; }
        public decimal TotalPrice { get; set; }        
              
        public int EntitlementId { get; set; }
        public virtual FinancialEntitlement FinancialEntitlement { get; set; }

        public DateTime Date { get; set; }
        public string Notes { get; set; }
              
    }
}
