using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    /// <summary>
    /// City
    /// </summary>
    public class CityDto
    {
        /// <summary>
        /// City Id 
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// City name
        /// </summary>
        public string CityName { set; get; }
        ///// <summary>
        ///// List of towns that belong to this city
        ///// </summary>
        //public List<TownDto> TownsDto { set; get; }

    }
}
