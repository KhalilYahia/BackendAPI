using YaznGhanem.Domain.Entities;
using YaznGhanem.Model;
using YaznGhanem.Domain.Inerfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Model.Repository
{
    internal class UserRepository : GenericRepository<CustomUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext storeContext) : base(storeContext)
        {
        }

        public CustomUser FindByUserName(string username)
        {
            return Set.FirstOrDefault(x => x.UserName == username);
        }

        public async Task<CustomUser> FindByUserNameAsync(string username)
        {
            return await Set.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public CustomUser FindByEmail(string email)
        {
            return Set.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
        }

        public async Task<CustomUser> FindByEmailAsync(string email)
        {
            return await Set.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public Task<CustomUser> FindByUserNameAsync(System.Threading.CancellationToken cancellationToken, string username)
        {
            return Set.FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }


        public Task<CustomUser> FindByEmailAsync(System.Threading.CancellationToken cancellationToken, string email)
        {
            return Set.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower(), cancellationToken);
        }

        public CustomUser FindByNormalizedUserName(string normalizedUserName)
        {
            throw new NotImplementedException();
        }

        public CustomUser FindByNormalizedEmail(string normalizedEmail)
        {
            throw new NotImplementedException();
        }

        CustomUser IUserRepository.FindByUserName(string username)
        {
            throw new NotImplementedException();
        }

        Task<CustomUser> IUserRepository.FindByUserNameAsync(string username)
        {
            throw new NotImplementedException();
        }

        Task<CustomUser> IUserRepository.FindByUserNameAsync(CancellationToken cancellationToken, string username)
        {
            throw new NotImplementedException();
        }

        CustomUser IUserRepository.FindByEmail(string email)
        {
            throw new NotImplementedException();
        }

        Task<CustomUser> IUserRepository.FindByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        Task<CustomUser> IUserRepository.FindByEmailAsync(CancellationToken cancellationToken, string email)
        {
            throw new NotImplementedException();
        }
    }
}
