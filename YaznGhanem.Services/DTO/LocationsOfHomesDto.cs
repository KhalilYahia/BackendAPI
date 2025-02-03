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
    /// جلب مواقع البيوت
    /// </summary>
    public class LocationsOfHomesDto
    {
        public int Id { get; set; }

        /// <summary>
        /// نوع الاعلان 
        /// بيع  
        ///أجار  
        /// </summary>
        public TypeOfAdvertisment Type { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }



    }
}
