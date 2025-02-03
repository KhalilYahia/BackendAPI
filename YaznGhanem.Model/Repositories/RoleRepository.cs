
using YaznGhanem.Domain.Inerfaces;
using YaznGhanem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Model.Repository;
using Microsoft.EntityFrameworkCore;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Data.Repositories
{
    internal class RoleRepository : GenericRepository<CustomRole>, IRoleRepository
    {
        internal RoleRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public CustomRole FindByName(string roleName)
        {
            return Set.FirstOrDefault(x => x.Name == roleName);
        }

        public Task<CustomRole> FindByNameAsync(string roleName)
        {
            return Set.FirstOrDefaultAsync(x => x.Name == roleName);
        }

        public Task<CustomRole> FindByNameAsync(System.Threading.CancellationToken cancellationToken, string roleName)
        {
            return Set.FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken);
        }


    }
}
