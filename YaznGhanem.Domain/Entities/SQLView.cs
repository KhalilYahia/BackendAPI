using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class SQLView_TotalOperations
    {
        public DateTime Date { get; set; }
        public decimal Cost { get; set; }
        public int IsProfit { get; set; }
    }
    public class SQLView_TheSafe
    {
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
      
    }
    public class SQLView_Operations_Last7Days
    {
        public DateTime OperationDate { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }
    }
}
