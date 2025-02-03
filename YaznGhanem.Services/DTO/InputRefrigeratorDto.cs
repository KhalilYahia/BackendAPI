using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class InputRefrigeratorDto
    {
        public int Id { set; get; }
        public string BuyerName { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public List<InputRefrigeratorDetailsDto> Details { get; set; }
    }

    public class InputRefrigeratorDetailsDto
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }

        /// <summary>
        /// عدد الصناديق لهذا النوع
        /// </summary>
        public decimal TotalBoxes { get; set; }

        /// <summary>
        /// كرت القبان لهذا النوع
        /// </summary>
        public decimal BalanceCardWeight { get; set; }

        /// <summary>
        /// وزن الطبلية
        /// </summary>
        public decimal EmptyBoxesWeight { get; set; }
      

        public decimal SalesPriceOfUnit { get; set; }


    }
}
