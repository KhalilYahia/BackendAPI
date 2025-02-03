using firstProject.Model;
using FirstProject.Domain.Entities;
using FirstProject.Domain.Inerfaces;
using FirstProject.Model.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FirstProject.Data.Repositories
{
    internal class RoleClaimRepository : GenericRepository<RoleClaim>, IRoleClaimRepository
    {
        public RoleClaimRepository(ApplicationDbContext storeContext)
          : base(storeContext)
        { }
        public IEnumerable<RoleClaim> FindByRoleId(string roleId)
        {
            return Set.Where(m => m.RoleId == roleId).ToList();           
        }
    }
}
