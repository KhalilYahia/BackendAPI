using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;
using static YaznGhanem.Common.Utils;

namespace YaznGhanem.Services.DTO
{
    public class Input_FuelDto
    {
        public int Id { set; get; }

        public string SourceName { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }
        public decimal PriceOfOne { get; set; }

        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
