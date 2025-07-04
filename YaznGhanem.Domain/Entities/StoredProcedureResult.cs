using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    // ReportResult.cs
    public class ReportResult
    {
        public TotalFundsResult TotalFunds { get; set; }
        public TheSafeResult TheSafe { get; set; }
        public List<OperationResult> Operations { get; set; }
    }

    public class TotalFundsResult
    {
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public decimal Profits { get; set; }
        public decimal CurrentFund { get; set; }
    }

    public class TheSafeResult
    {
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
    }

    public class OperationResult
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool IsProfit { get; set; }
    }
}
