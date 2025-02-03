using FirstProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Domain.Inerfaces
{
    public interface IRoleClaimRepository : IGenericRepository<RoleClaim>
    {
        IEnumerable<RoleClaim> FindByRoleId(string roleId);
    }
}
