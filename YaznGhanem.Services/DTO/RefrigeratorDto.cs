using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class RefrigeratorDto
    {
        public int Id { set; get; }

        public decimal TotalBoxes { get; set; }
        public decimal TotalBalanceCardWeight { get; set; }

        /// <summary>
        /// مجموع وزن الطبالي 
        /// </summary>
        public decimal TotalEmptyBoxesWeight { get; set; }
        /// <summary>
        /// مجموع الوزن النهائي 
        /// </summary>
        public decimal TotalWeightAfterDiscount_2Percent { get; set; }
        public string CodeNumber { get; set; }

        public decimal TotalSalesPriceOfAll { get; set; }


        public string BuyerName { get; set; }
        public int BuyerId { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public List<RefrigeratorDetailsDto> RefrigeratorDetailsDtos { get; set; }
    }
    public class RefrigeratorDetailsDto
    {
        public int RepositoryMaterialId { set; get; }

        public string MaterialName { set; get; }
        /// <summary>
        /// عدد الصناديق لهذا النوع
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
    }
}
