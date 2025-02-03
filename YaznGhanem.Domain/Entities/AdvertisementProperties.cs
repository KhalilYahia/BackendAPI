using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class AdvertisementProperties
    {
        public int Id { get; set; }

        public int AdvertismentId { get; set; }
        public virtual Advertisement Advertisement { get; set; }

        public string Value { get; set; }

        public int PropertyId { get; set; }
        public virtual Properties Property { get; set; }


    }
}
