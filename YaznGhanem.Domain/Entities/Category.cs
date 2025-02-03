using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class Category
    {
        

        public int Id { get; set; }

        public int Sort { get; set; }

        public string CategoryName { set; get; }

        public string Unit { set; get; }

        public virtual ICollection<RepositoryMaterials> RepositoryMaterials { set; get; }

        public virtual ICollection<Repository> Repositories { set; get; }
    }
}
