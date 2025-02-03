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
    public class InputAdvertismentDto
    {
        public int Id { get; set; }

        /// <summary>
        /// نوع الاعلان 
        /// بيع  
        ///أجار  
        /// </summary>
        public TypeOfAdvertisment Type { get; set; }
        /// <summary>
        /// ان كان من الكلاس A 
        /// هذا يعني انه سيظهر بأول النتائج
        /// </summary>
        public ClassOfAdvertisment Class { get; set; }
        public StatusOfAdvertisment Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        /// <summary>
        /// اكتب هنا ماتود ان يظهر للمستخدم على الواجهة
        /// </summary>
        public string PriceAsText { get; set; }

        public string Address { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }


        public string PhoneNumber_Call { get; set; }
        public string WhatsApp { get; set; }
        public string Telegram { get; set; }
        public string Facebook { get; set; }
        public float Area { get; set; }
        /// <summary>
        /// اكتب هنا ماتود ان يظهر للمستخدم على الواجهة
        /// </summary>
        public string AreaAsText { get; set; }

        // public DateTime AddingDate { get; set; }

        public int CityId { get; set; }
       
        public int CategoryId { get; set; }

        public string AdvertiserUserId { get; set; }

        //public ICollection<ImageOfAdvertisment> Images { get; set; }
        
        public Dictionary<int,string> Properties { get; set; }

        //public AdvertismentSearch? AdvertismentSearch { set; get; }
    }
}
