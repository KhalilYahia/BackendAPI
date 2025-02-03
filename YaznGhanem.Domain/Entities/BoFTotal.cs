using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    /// <summary>
    /// عدد الصناديق الحقلية الاجمالي
    /// </summary>
    public class BoFTotal
    {
        public int Id { set; get; }

        public int TotalIn { get; set; }

        public int TotalOut { get; set; }
        

        /// <summary>
        /// عدد الصناديق الحالي
        /// </summary>
        public int Current { get; set; }

    }
}
