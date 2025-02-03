using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int Sort { set; get; }
        public virtual ICollection<CityDescription> CityDescription { set; get; }
        public virtual ICollection<Advertisement> Advertisements { set; get;}
    }
}
