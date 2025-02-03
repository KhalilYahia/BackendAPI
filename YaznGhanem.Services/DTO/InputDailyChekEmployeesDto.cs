using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class InputDailyChekEmployeesDto
    {
        public int Id { set; get; }

        public int EmployeeId { get; set; }

        public int GirlsCount { get; set; }
        public int MenCount { get; set; }
        /// <summary>
        /// عدد ساعات العمل النظامي
        /// </summary>
        public decimal NormJobHCount { get; set; }
        /// <summary>
        /// عدد ساعات العمل الاضافي
        /// </summary>
        public decimal AddJobHCount { get; set; }
        
        /// <summary>
        /// المكافأة اليومية
        /// </summary>
        public decimal Reward { get; set; }
        /// <summary>
        /// الحسم اليومي
        /// </summary>
        public decimal Discount { get; set; }
        
        /// <summary>
        /// المبلغ المدفوع
        /// هذا الحقل من أجل دفع أجر
        /// </summary>
        public decimal PaidWage { get; set; }
        /// <summary>
        /// تاريخ اليوم
        /// </summary>
        public DateTime Date { get; set; }

        public string Notes { get; set; }

    }
}
