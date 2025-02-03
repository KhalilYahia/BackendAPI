using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class Cars_InSimplifyDto
    {
        public int Id { set; get; }

        public string DriverName { get; set; }
        public int DriverId { get; set; }
      
        public int LoadsPerDay { get; set; }
        public decimal PriceOfOne { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }

    }
}
