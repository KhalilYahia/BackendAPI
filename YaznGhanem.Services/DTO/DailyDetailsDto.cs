using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class DailyDetailsDto
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }

        public string MaterialName { set; get; }

        public int TotalBoxes { get; set; }
        public decimal BalanceCardWeight { get; set; }

        public decimal EmptyBoxesWeight { get; set; }

        public decimal WeightAfterDiscount_2Percent { get; set; }
        public string CodeNumber { get; set; }


        public decimal BuyPriceOfUnit { get; set; }

        public decimal BuyPriceOfAll { get; set; }


        public string FarmerName { get; set; }
        public int FarmerId { get; set; }
      
        /// <summary>
        /// اسم المورد
        /// المحاسبة فقط على اسم المزارع هنا
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// كلفة القص هذه تعطى للمورد
        /// </summary>
        public decimal CuttingCostOfUnit { get; set; }
        /// <summary>
        /// كلفة القص الكلية تعطى للمورد
        /// </summary>
        public decimal CuttingCostOfAll { get; set; }

        /// <summary>
        /// كلفة تشميع الكيلو تعطى للشماعة
        /// </summary>
        public decimal WaxingCostOfUnit { get; set; }
        /// <summary>
        /// كلفة التشميع الكلية تعطى للشماعة
        /// </summary>
        public decimal WaxingCostOfAll { get; set; }

        public int EntitlementId { get; set; }
      
        public DateTime Date { get; set; }

        public string Notes { get; set; }


    }
}
