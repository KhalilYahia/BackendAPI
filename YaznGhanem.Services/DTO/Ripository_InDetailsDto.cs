using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static YaznGhanem.Common.Utils;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class Ripository_InDetailsDto
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }
        public string Name { set; get; }


        public decimal Amount { get; set; }

        public decimal BuyPriceOfUnit { get; set; }

        public decimal BuyPriceOfAll { get; set; }


        public string SupplierName { get; set; }
        public int SupplierId { get; set; }


        public DirectionType Direction { get; set; }


        public DateTime Date { get; set; }

        public string Notes { get; set; }


    }
}
