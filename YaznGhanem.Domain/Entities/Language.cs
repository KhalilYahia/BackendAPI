﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }

       
    }
}
