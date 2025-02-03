using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Entities
{
    public class RepositoryMaterials
    {
        public int Id { set; get; }

        public int CategoryId { set; get; }

        public string Name { set; get; }

        public decimal? DefaultPrice { set; get; }

        public decimal? DefaultSoldPrice { set; get; }

       

        public int Sort { get; set; }

        public virtual Category Category { set; get; }

        public virtual ICollection<Repository> Repositories { set; get; }
        public virtual ICollection<Repository_InOut> Repository_InOuts { set; get; }
        public virtual ICollection<Daily> Dailies { set; get; }
        public virtual ICollection<RefrigeratorDetails> RefrigeratorDetails { set; get; }
        public virtual ICollection<ExternalEnvoices> ExternalEnvoices { set; get; }
        public virtual ICollection<CoolingRooms> CoolingRooms { set; get; }
        public virtual ICollection<OtherSales> OtherSales { set; get; }
        //
    }

}
