using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class Refrigerator
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
        public virtual Buyers Buyer { set; get; }
      

        public DateTime Date { get; set; }

        public string Notes { get; set; }
    
        public virtual ICollection<RefrigeratorDetails> RefrigeratorDetails { set; get; }


    } 
}
