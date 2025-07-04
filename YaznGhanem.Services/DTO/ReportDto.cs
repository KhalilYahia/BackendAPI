using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class ReportDto
    {
        public earnsdto earns { get; set; }
        public TheSafeDto TheSafe { get; set; }
        /// <summary>
        /// إيرادات اخر 7 ايام
        /// </summary>
        public List<TotalOperations> Last7Days_revenue_Opt { get; set; }
        /// <summary>
        /// مصاريف اخر 7 ايام
        /// </summary>
        public List<TotalOperations> Last7Days_expenses_Opt { get; set; }
        public List<TotalOperations> AllOpt { get; set; }
    }
    /// <summary>
    /// جميع العمليات
    /// </summary>
    public class earnsdto
    {
        /// <summary>
        /// اجمالي حركة الدخول من جدول 
        /// TotalFunds
        /// </summary>
        public decimal TotalIn { get; set; }
        /// <summary>
        /// from TotalFunds table
        /// </summary>
        public decimal TotalOut { get; set; }
        /// <summary>
        /// from TotalOut table
        /// </summary>
        public decimal Profits { get; set; }
    }

    /// <summary>
    /// الصندوق
    /// </summary>
    public class TheSafeDto
    {
        /// <summary>
        /// مجموع البرادات
        /// غرف تبريد
        /// فواتير خارجية
        /// فواتير أخرى
        /// </summary>
        public decimal TotalIn { get; set; }
        /// <summary>
        /// مجموع دفعات الديون
        /// دفعات العمال        
        /// </summary>
        public decimal TotalOut { get; set; }
        /// <summary>
        /// الموجود حاليا في الصندوق
        /// From table AllFunds
        /// </summary>
        public decimal Current { get; set; }
    }
    /// <summary>
    /// كامل العمليات + العمليات الاسبوعية
    /// </summary>
    public class TotalOperations
    {
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }
        public bool IsProfit { get; set; }
    }


    public class Operations_Last7DaysDto
    {
        public DateTime OperationDate { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
    }
}

