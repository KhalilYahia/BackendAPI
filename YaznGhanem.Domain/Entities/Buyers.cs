using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class Buyers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// هذا مهم للمقارنة معه عند عملية الادخال
        /// ان كان الاسم يحوي فراغات فيجب حذف الفراغات والمقارنة مع هذا الاسم
        /// 
        /// </summary>
        public string BuyerNameWithoutSpaces { get; set; }

        public virtual ICollection<Refrigerator> Refrigerators { set; get; }
        public virtual ICollection<ExternalEnvoices> ExternalEnvoices { set; get; }
        public virtual ICollection<CoolingRooms> CoolingRooms { set; get; }
        public virtual ICollection<OtherSales> OtherSales { set; get; }
        //public virtual ICollection<Daily> Dailies { set; get; }
    }
}
