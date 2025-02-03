using YaznGhanem.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Inerfaces
{
    public interface IRoleRepository : IGenericRepository<CustomRole>
    {
        CustomRole FindByName(string roleName);
        Task<CustomRole> FindByNameAsync(string roleName);

        Task<CustomRole> FindByNameAsync(System.Threading.CancellationToken cancellationToken, string roleName);
    }
}
