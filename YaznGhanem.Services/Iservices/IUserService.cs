using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.Iservices
{
    public interface IUserService
    {
       Task<List<UserDto>> GetAllUsers_forAdmin();

       Task<UserDto> GetUserById_forAdmin(string userId);


       Task<List<RoleDto>> GetAllRoles_forAdmin();


    }
}
