﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class CategoryGetDto
    {
        /// <summary>
        /// Category/Guide Id, you can put it null if you want to get base category
        /// </summary>
        public int? Id { get; set; }
    }
}
