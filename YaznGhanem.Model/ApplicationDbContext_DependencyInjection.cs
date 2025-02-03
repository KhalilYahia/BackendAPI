using YaznGhanem.Domain.Entities;
using YaznGhanem.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace YaznGhanem.Model.ApplicationDbContext_DependencyInjection
{
    public static class ApplicationDbContext_DependencyInjection
    {
        public static void AddDbContext_Khalil(this IServiceCollection bld,string connectionString)
        {
            bld.AddDbContext<ApplicationDbContext>(options =>
            {
                //options.EnableSensitiveDataLogging();
                options.UseSqlServer(connectionString);
            });
        }

        public static void AddIdentityOptions_Khalil(this IServiceCollection bld)
        {
            bld.AddIdentity<CustomUser,CustomRole>(options =>
            {
                //Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            //.AddIdentity<CustomUser, CustomRole>(options =>
            // {
            //     // Password settings
            //     options.Password.RequireDigit = false;
            //     options.Password.RequiredLength = 6;
            //     options.Password.RequireNonAlphanumeric = false;
            //     options.Password.RequireUppercase = false;
            //     options.Password.RequireLowercase = false;


            // })

            //bld.Configure<IdentityOptions>(options =>
            //{
            //    // Password settings
            //    options.Password.RequireDigit = false;
            //    options.Password.RequiredLength = 6;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequireLowercase = false;


            //});
        }
    }
}
