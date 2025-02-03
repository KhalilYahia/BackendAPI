using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class Search_BillingDto
    {
        public int DealerId { set; get; }
        public string DealerName { set; get; }

        /// <summary>
        /// المورد للرزق اليومي
        /// </summary>
        public int? SupplierId { set; get; }
        /// <summary>
        /// اسم مورد الرزق اليومي فقط
        /// </summary>
        public string? SupplierName { set; get; }

        public bool CoolingRooms { get; set; }

        public bool Refrigerator { get; set; }

        public bool ExternalEnvoices { get; set; }
        

        //public bool OtherSales { get; set; }
        /// <summary>
        /// الرزق اليومي
        /// </summary>
        public bool Daily { get; set; }
        public bool Tabali { get; set; }
        public bool Plastic { get; set; }
        public bool Karasta { get; set; }
        public bool Fuel { get; set; }
        public bool Cars { get; set; }
        public bool Employees { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

    }
}
