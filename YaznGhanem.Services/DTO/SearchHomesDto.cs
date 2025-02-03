using YaznGhanem.Common;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    /// <summary>
    /// جلب المعلومات السريعة
    /// </summary>
    public class SearchHomesDto
    {
        public int Id { get; set; }

        /// <summary>
        /// نوع الاعلان 
        /// بيع  =0
        ///أجار  =1
        ///all =2
        /// </summary>
        public int Type { get; set; }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public float MinArea { get; set; }
        public float MaxArea { get; set; }

        public int page { get; set; }

    }
}
