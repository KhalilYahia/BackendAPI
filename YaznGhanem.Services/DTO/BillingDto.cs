using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class BillingDto
    {
        public int DealerId { set; get; }
        public string DelearName { get; set; }

        /// <summary>
        /// المورد للرزق اليومي
        /// </summary>
        public int? SupplierId { set; get; }

        /// <summary>
        /// اسم مورد الرزق اليومي فقط
        /// </summary>
        public string SupplierName { set; get; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool CoolingRooms { get; set; }

        public bool Refrigerator { get; set; }

        public bool ExternalEnvoices { get; set; }
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

        public List<DetailsBillingDto> DetailsBillingDto { get; set; }

        /// <summary>
        /// هذا فقط من أجل العمال
        /// </summary>
        public List<DailyChekEmployeesDto> DailyChekEmployeesDtos { get; set; }
        public decimal Total { get; set; }

        ///// <summary>
        ///// مجموع الباقي من تاريخ سابق
        ///// هذا في حالة تحديد مدة زمنية
        ///// </summary>
        //public decimal RemainderFromPast { get; set; }
        /// <summary>
        /// تفاصيل الدفعات خلال المدة الزمنية المحددة
        /// </summary>
        public List<PaymentsBillingDto> PaymentsBillingDtos { get; set; }
        /// <summary>
        /// مجموع الدفعات ضمن التاريخ المحدد
        /// </summary>
        public decimal TotalPayments { get; set; }

        /// <summary>
        /// الباقي من السابق
        /// </summary>
        public decimal Remainder_Past { get; set; }
        /// <summary>
        /// المتبقي النهائي
        /// </summary>
        public decimal Remainder { get; set; }

        public decimal TotalBoxes { get; set; }
        public decimal WeightAfterDiscount_2Percent { get; set; }
        public decimal TotalBalanceCardWeight { get; set; }

        public decimal TotalCuttingCostOfAll { get; set; }

        public List<RefrigeratorDto> RefrigeratorDtos { get; set; }
 

    }
    public class DetailsBillingDto
    {
        public DateTime Date { set; get; }
        /// <summary>
        /// اسم المزارع في الرزق اليومي فقط
        /// </summary>
        public string DelearName { get; set; }

        public string Statement { get; set; }

        public decimal Count { get; set; }
        public decimal BalanceCardWeight { get; set; }

        /// <summary>
        /// الوزن النهائي
        /// </summary>
        public decimal WeightAfterDiscount_2Percent { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
        /// <summary>
        /// كلفة القص هذه تعطى للمورد
        /// </summary>
        public decimal CuttingCostOfUnit { get; set; }
        /// <summary>
        /// كلفة القص الكلية تعطى للمورد
        /// </summary>
        public decimal CuttingCostOfAll { get; set; }
    }

    public class PaymentsBillingDto
    {
        public decimal Payment { get; set; }
        public DateTime Date { get; set; }
    }
}
