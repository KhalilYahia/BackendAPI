using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class InputDailyDto
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }

        public int TotalBoxes { get; set; }
        public decimal BalanceCardWeight { get; set; }

        public decimal EmptyBoxesWeight { get; set; }
        public string CodeNumber { get; set; }
        public decimal BuyPriceOfUnit { get; set; }

        public string FarmerName { get; set; }

        /// <summary>
        /// كلفة القص هذه تعطى للمورد
        /// </summary>
        public decimal CuttingCostOfUnit { get; set; }

        /// <summary>
        /// كلفة تشميع الكيلو تعطى للشماعة
        /// </summary>
        public decimal WaxingCostOfUnit { get; set; }
        

        /// <summary>
        /// اسم المورد
        /// المحاسبة فقط على اسم المزارع هنا
        /// </summary>
        public string Supplier { get; set; }

        

        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}
