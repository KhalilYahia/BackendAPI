using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class Repository
    {
        public int Id { set; get; }

        public int CategoryId { set; get; }

        public int RepositoryMaterialsId { set; get; }

       

        public string Name { set; get; }

        public int Sort { get; set; }

        public decimal Amount_In { get; set; }

        public decimal Amount_Out { get; set; }

        public decimal Amount_Remender { get; set; }

       

        public virtual Category Category { set; get; }

        public virtual RepositoryMaterials RepositoryMaterials { set; get; }

       
    }
}
