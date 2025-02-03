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
    /// جلب المعلومات التفصيلية
    /// </summary>
    public class DetailedHomeDto
    {
        public int Id { get; set; }

        /// <summary>
        /// نوع الاعلان 
        /// بيع  
        ///أجار  
        /// </summary>
        public TypeOfAdvertisment Type { get; set; }
        
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

        public DateTime AddingDate { get; set; }
        public float Area { get; set; }
        /// <summary>
        /// اكتب هنا ماتود ان يظهر للمستخدم على الواجهة
        /// </summary>
        public string AreaAsText { get; set; }
        public int CityId { get; set; }        
        public List<string> Images { get; set; }
        public bool? IsInfavorite { get; set; }

        public List<PropertyForDetailsDto> Properties { get; set; }


    }
}
