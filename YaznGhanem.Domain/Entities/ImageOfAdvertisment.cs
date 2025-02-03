using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class ImageOfAdvertisment
    {
      
        public int Id { get; set; }
        public string Path { get; set; }
        public bool IsPrimary { get; set; }


        public int AdvertismentId { get; set; }
        public virtual Advertisement Advertisement { get; set; }


    }
}
