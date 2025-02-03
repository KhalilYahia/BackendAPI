using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class RepositoryMaterialsDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal? DefaultPrice { get; set; }
        public decimal? DefaultSoldPrice { get; set; }
        public int Sort { get; set; }
    }
}
