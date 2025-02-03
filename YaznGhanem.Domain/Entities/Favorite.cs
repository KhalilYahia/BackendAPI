using YaznGhanem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class Favorite
    {
        public int Id { set; get; }
        
        
        public DateTime AddingDate { set; get; }


        public string UserId { get; set; }
        public virtual CustomUser User { set; get; }

        public int AdvertismentId { set; get; }
        public virtual Advertisement Advertisement { set; get; }
    }
}
