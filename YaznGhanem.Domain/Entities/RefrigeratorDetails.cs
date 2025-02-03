using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class RefrigeratorDetails
    {
        public int Id { set; get; }
        public int RefrigeratorId { get; set; }

        public int RepositoryMaterialId { set; get; }

        public string MaterialName { set; get; }
        /// <summary>
        /// عدد الطبالي لهذا النوع
        /// </summary>
        public decimal CountOfBoxes { get; set; }

        public decimal BalanceCardWeight { get; set; }

        /// <summary>
        /// وزن الطبلية
        /// </summary>
        public decimal EmptyBoxesWeight { get; set; }
        /// <summary>
        /// الوزن النهائي
        /// </summary>
        public decimal WeightAfterDiscount_2Percent { get; set; }
       
        public decimal SalesPriceOfUnit { get; set; }

        public decimal SalesPriceOfAll { get; set; }


        public virtual RepositoryMaterials RepositoryMaterial { set; get; }
        public virtual Refrigerator Refrigerator { set; get; }


    } 
}
