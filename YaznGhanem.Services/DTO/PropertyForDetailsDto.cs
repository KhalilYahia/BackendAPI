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
    /// PropertyForDetailsDto
    /// </summary>
    public class PropertyForDetailsDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// اسم الخاصية
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// قيمة هذه الخاصية
        /// </summary>
        public string Value { get; set; }


    }
}
