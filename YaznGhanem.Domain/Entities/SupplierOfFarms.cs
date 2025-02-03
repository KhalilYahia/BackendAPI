using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    /// <summary>
    /// هذا الجدول متعلق بالرزق اليومي فقط
    /// يحوي معلومات المورد
    /// </summary>
    public class SupplierOfFarms
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// هذا مهم للمقارنة معه عند عملية الادخال
        /// ان كان الاسم يحوي فراغات فيجب حذف الفراغات والمقارنة مع هذا الاسم
        /// 
        /// </summary>
        public string NameWithoutSpaces { get; set; }

        public virtual ICollection<Daily> Dailies { set; get; }
        public virtual ICollection<FinancialEntitlement> FinancialEntitlements { set; get; }
    }
}
