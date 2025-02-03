using YaznGhanem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Domain.Inerfaces
{
    public interface IUserRepository:IGenericRepository<CustomUser>
    {
        //List<IdentityUser> GetUsers(string searchString, int pageIndix, int pagesize);
        //Task<IdentityUser> GetUsersbyID(string UserId);
        //void UpdateUser(IdentityUser dto);
        //Task<bool> DeleteUSerAsync(string UserId);
        //bool DeleteUSer(IdentityUser UserId);
        //int Userscount();

        CustomUser FindByUserName(string username);
        Task<CustomUser> FindByUserNameAsync(string username);
        Task<CustomUser> FindByUserNameAsync(CancellationToken cancellationToken, string username);
        CustomUser FindByEmail(string email);
        Task<CustomUser> FindByEmailAsync(string email);
        Task<CustomUser> FindByEmailAsync(CancellationToken cancellationToken, string email);
    }
}
