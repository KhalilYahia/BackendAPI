using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class CategoryDescription
    {
        public int Id { set; get; }
        public int CategoryId { set; get; }
        public int LanguageId { set; get; }
        public string Unit { set; get; }
        public string CategoryName { set; get; }

        public virtual Language Language { set; get; }
        public virtual Category Category { set; get; }
    }
}
