using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Domain.Entities
{
    public class Employees
    {
        public int Id { set; get; }

        public string workshopName { get; set; }

        
        /// <summary>
        /// سعر الساعة النظامي للشباب
        /// </summary>
        public decimal NormHWageM { get; set; }
        /// <summary>
        /// سعر الساعة النظامي للبنات
        /// </summary>
        public decimal NormHWageG { get; set; }
        /// <summary>
        /// سعر الساعة الاضافي
        /// </summary>
        public decimal AdditionalWorkingHourWage { get; set; }
        /// <summary>
        /// الراتب الكلي
        /// </summary>
        public decimal TotalWage { get; set; }
        /// <summary>
        /// مجموع المكافآت
        /// </summary>
        public decimal TotalRewards { get; set; }
        /// <summary>
        /// مجموع الحسومات
        /// </summary>
        public decimal TotalDiscount { get; set; }
        /// <summary>
        /// المبلغ المطلوب بعد المكافآت والحسومات
        /// </summary>
        public decimal TotalWageAfterDiscount { get; set; }
        /// <summary>
        /// مجموع الدفعات
        /// </summary>
        public decimal Payments { get; set; }
        /// <summary>
        /// المتبقي
        /// </summary>
        public decimal Remainder { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

        public virtual ICollection<DailyChekEmployees> DailyChekEmployees { get; set; }

    }
}
