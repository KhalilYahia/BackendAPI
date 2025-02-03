using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class InputEmployeeDto
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
        
        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}
