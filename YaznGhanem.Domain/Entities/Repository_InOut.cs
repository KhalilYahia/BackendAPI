using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class Repository_InOut
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }


        public string Name { set; get; }

        public decimal Amount { get; set; }

        public decimal BuyPriceOfUnit { get; set; }

        public decimal BuyPriceOfAll { get; set; }


        public decimal SoldPriceOfUnit { get; set; }

        public decimal SoldPriceOfAll { get; set; }

        public string SupplierName { get; set; }
        public int SupplierId { get; set; }

        public int EntitlementId { get; set; }
        public virtual FinancialEntitlement FinancialEntitlement { get; set; }

        public DirectionType Direction { get; set; }


        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public virtual RepositoryMaterials RepositoryMaterial { set; get; }

        public virtual Supplier Supplier { set; get; }


    }
}
