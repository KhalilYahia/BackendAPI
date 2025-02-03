using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Services.DTO
{
    public class Input_RepositoryInDto
    {
        public int CategoryId { get; set; }
        public int Id { get; set; }
        /// <summary>
        /// معرف المادة
        /// </summary>
        public int RepositoryMaterialId { get; set; }
        /// <summary>
        /// اسم المادة
        /// </summary>
        public string RepositoryMaterialName { get; set; }

        public decimal Amount { get; set; }
        public decimal BuyPriceOfUnit { get; set; }
        /// <summary>
        /// اسم المصدر
        /// </summary>
        public string SupplierName { get; set; }
                
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
