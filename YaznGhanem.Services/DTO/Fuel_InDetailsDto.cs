using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class Fuel_InDetailsDto
    {
        public int Id { set; get; }

        public string SourceName { get; set; }
        public int SourceId { get; set; }
        public string Type { get; set; }

        public decimal Amount { get; set; }
        public decimal PriceOfOne { get; set; }
        public decimal TotalPrice { get; set; }

        public int EntitlementId { get; set; }

        public DateTime Date { get; set; }
        public string Notes { get; set; }

    }
}
