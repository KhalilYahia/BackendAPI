using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class Report_DesktopDto
    {
        public earnsdto earns { get; set; }
        public TheSafeDto TheSafe { get; set; }

        public List<Operations_Desktop> Operations { get; set; }

    }
    /// <summary>
    /// كامل العمليات + العمليات الاسبوعية
    /// </summary>
    public class Operations_Desktop
    {
        public DateTime Date { get; set; }
        public decimal MoneyAmount { get; set; }
        
        /// <summary>
        /// إيراد
        /// مصاريف
        /// دفعة ورشة
        /// </summary>
        public string Type { get; set; }

        public string Details { get; set; }
        public string NameOfClient { get; set; }

    }

}

