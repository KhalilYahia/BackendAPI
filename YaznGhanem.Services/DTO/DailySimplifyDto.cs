using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class DailySimplifyDto
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }

        public string MaterialName { set; get; }

        public int TotalBoxes { get; set; }
        

        public decimal WeightAfterDiscount_2Percent { get; set; }

        public decimal BuyPriceOfUnit { get; set; }

        public decimal BuyPriceOfAll { get; set; }


        public string FarmerName { get; set; }
        public int FarmerId { get; set; }
        

        /// <summary>
        /// اسم المورد
        /// المحاسبة فقط على اسم المزارع هنا
        /// </summary>
        public string Supplier { get; set; }

        
        public DateTime Date { get; set; }

       

    }
}
