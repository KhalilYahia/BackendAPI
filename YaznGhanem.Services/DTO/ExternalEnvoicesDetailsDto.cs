using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class ExternalEnvoicesDetailsDto
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }

        public string MaterialName { set; get; }

        public int TotalBoxes { get; set; }
        /// <summary>
        /// الوزن الصافي
        /// </summary>
        public decimal Weight { get; set; }


        public string CodeNumber { get; set; }


        public decimal SalesPriceOfUnit { get; set; }

        public decimal SalesPriceOfAll { get; set; }


        public string BuyerName { get; set; }
        public int BuyerId { get; set; }


        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}
