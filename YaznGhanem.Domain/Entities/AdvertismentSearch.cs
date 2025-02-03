using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class AdvertismentSearch
    {
        public int Id { get; set; }

        [NotMapped]
        public float[] Data
        {
            get
            {
                string[] tab = this.InternalData.Split(',');
                return new float[] { float.Parse(tab[0]), float.Parse(tab[1]) };
            }
            set
            {
                this.InternalData = string.Format("{0},{1}", value[0], value[1]);
            }
        }
        public string InternalData { get; set; }


        public int AdvertisementId { get; set; }
        public virtual Advertisement Advertisement { get; set; }
    }
}
