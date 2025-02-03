using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class RefrigeratorSimplifyDto
    {
        public int Id { set; get; }
        public decimal TotalBoxes { get; set; }
        /// <summary>
        /// مجموع الوزن النهائي 
        /// </summary>
        public decimal TotalWeightAfterDiscount_2Percent { get; set; }
        public decimal TotalSalesPriceOfAll { get; set; }
        /// <summary>
        /// اسم التاجر
        /// </summary>
        public string BuyerName { get; set; }

        public DateTime Date { get; set; }

    }
}
