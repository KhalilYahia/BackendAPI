using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class OtherSalesSimplifyDto
    {
        public int Id { set; get; }

        public int RepositoryMaterialId { set; get; }
        public string MaterialName { set; get; }
        public decimal SalesPriceOfAll { get; set; }
        public string BuyerName { get; set; }

        public DateTime Date { get; set; }


    }
}
