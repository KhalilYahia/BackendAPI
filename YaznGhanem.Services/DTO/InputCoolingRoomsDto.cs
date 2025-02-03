using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class InputCoolingRoomsDto
    {
        public int Id { set; get; }
        public string Room { set; get; }

        public int RepositoryMaterialId { set; get; }

        public int TotalBoxes { get; set; }
        /// <summary>
        /// الوزن الصافي
        /// </summary>
        public decimal Weight { get; set; }


        /// <summary>
        /// التكلفة للكيلو
        /// </summary>
        public decimal CostOfUnit { get; set; }

        public string ClientName { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }
    }
}
