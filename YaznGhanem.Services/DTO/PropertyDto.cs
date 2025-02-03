using YaznGhanem.Common;
using YaznGhanem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    /// <summary>
    /// PropertyDto
    /// </summary>
    public class PropertyDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// اسم الخاصية
        /// </summary>
        public string Name { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// type of this property,
        /// 0--> int, 1--> decimal, 2-->decimal, 3--> bool, 4--> DateTime, 5--> string, 6-->Guid 
        /// </summary>
        public TypeOfProperty Type { get; set; }
        /// <summary>
        /// type to show
        /// Type show for user display
        /// 0--> checkbox, 1--> dropdownlist, 2--> TextBox, 3--> Calender
        /// </summary>
        public TypeShowOfProperty TypeShow { get; set; }

        /// <summary>
        /// Type show for Manager display
        /// 0--> checkbox, 1--> dropdownlist, 2--> TextBox, 3--> Calender
        /// </summary>
        public TypeShowOfProperty TypeManagerShow { get; set; }

        
    }
}
