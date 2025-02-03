using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class TotalFundsDto
    {
        public int Id { set; get; }

        public decimal TotalIn { get; set; }

        public decimal TotalOut { get; set; }
        /// <summary>
        /// كامل الأرباح
        /// 
        /// TotalIn-TotalOut        
        /// </summary>
        public decimal Profits { get; set; }


        /// <summary>
        /// الارباح المستوفية فقط
        /// </summary>
        public decimal EarnedProfits { get; set; }

        /// <summary>
        /// المبلغ الحالي في الصندوق
        /// </summary>
        public decimal CurrentFund { get; set; }
    }
}
