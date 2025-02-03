using firstProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Domain.Inerfaces
{
    public interface IUserRoleRepository
    {
        void Add(string UserId, string roleName);
        void Remove(string userId, string roleName);
        IEnumerable<string> GetRoleNamesByUserId(string userId);
        IEnumerable<User> GetUsersByRoleName(string roleName);
    }
}
