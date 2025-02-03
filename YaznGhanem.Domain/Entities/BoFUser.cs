using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    /// <summary>
    /// المستخدمين الخاصين بالصناديق الحقلية
    /// </summary>
    public class BoFUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        /// <summary>
        /// هذا مهم للمقارنة معه عند عملية الادخال
        /// ان كان الاسم يحوي فراغات فيجب حذف الفراغات والمقارنة مع هذا الاسم
        /// 
        /// </summary>
        public string UserNameWithoutSpaces { get; set; }

        public virtual ICollection<BoFOperations> BoFOperations { set; get; }
        //public virtual ICollection<Repository_InOut> Repository_InOuts { set; get; }
        //public virtual ICollection<Fuel> Fuels { set; get; }
        //public virtual ICollection<Cars> Cars { set; get; }
        //public virtual ICollection<Daily> Dailies { set; get; }
    }
}
