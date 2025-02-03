using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class DailyChekEmployees
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
        /// سعر الساعة النظامي للشباب
        /// </summary>
        public decimal NormHWageM { get; set; } // new added 
        /// <summary>
        /// سعر الساعة النظامي للبنات
        /// </summary>
        public decimal NormHWageG { get; set; } // new added 
        /// <summary>
        /// سعر الساعة الاضافي
        /// </summary>
        public decimal AdditionalWorkingHourWage { get; set; } // new added 
        /// <summary>
        /// الأجر اليومي
        /// </summary>
        public decimal TotalWage { get; set; }
        /// <summary>
        /// المكافأة اليومية
        /// </summary>
        public decimal Reward { get; set; }
        /// <summary>
        /// الحسم اليومي
        /// </summary>
        public decimal Discount { get; set; }
        /// <summary>
        ///  صافي المبلغ الناتج
        /// </summary>
        public decimal ResultWage { get; set; }
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

      

        public virtual Employees Employee { get; set; }

    }
}
