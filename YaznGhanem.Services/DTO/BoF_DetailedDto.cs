using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    /// <summary>
    /// جلب التفاصيل الخاصة بالصناديق الحقلية لعملية معينة
    /// </summary>
    public class BoF_DetailedDto
    {
        public int Id { set; get; }

        public int Count { set; get; }

        

        public int BoFUserId { get; set; }
        public string BoFUserName { get; set; }
       
        public DateTime Date { get; set; }

        public string Notes { get; set; }


        public List<BoF_OpDetailsDto> BoFOpDetails { set; get; }

    }

    public class BoF_OpDetailsDto
    {
        public int Id { set; get; }
        /// <summary>
        /// داخل ممتلئ
        /// داخل فارغ
        /// خارج
        /// </summary>
        public string Direction { set; get; }
        public string ColorType { set; get; }

        public int Count { set; get; }       

    }
}
