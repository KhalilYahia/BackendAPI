
using Microsoft.Extensions.DependencyInjection;

using System.Data;
using YaznGhanem.Domain;
using YaznGhanem.Data;
using YaznGhanem.Services.Iservices;
using YaznGhanem.Services.services;
using AutoMapper;
using YaznGhanem.Services;
using FluentValidation;
using YaznGhanem.Domain.Entities;



namespace Services.DependenyInjection
{
    public static class IServiceDependenyInjection
    {
        public static void SetDependencies(this IServiceCollection serviceCollection/*, IConfigurationRoot configuration*/)
        {
            #region important region

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ITokenService, TokenService>();

            serviceCollection.AddAutoMapper(typeof(MappingProfiles));

            #endregion

            //serviceCollection.AddScoped<IValidator<AddBlogDto>, AddBlogDtoValidator>();

            //serviceCollection.AddScoped<IBlogsService,BlogsService>();
            //serviceCollection.AddScoped<IPostsService, PostsService>();

            serviceCollection.AddScoped<ICategoryService, CategoryService>();

            serviceCollection.AddScoped<ISupplierService, SupplierService>();
            serviceCollection.AddScoped<IEmailService, EmailService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IRepositoryMaterialsService, RepositoryMaterialsService>();
            serviceCollection.AddScoped<IFinancialEntitlementService, FinancialEntitlementService>();
            serviceCollection.AddScoped<IFinancialPaymentService, FinancialPaymentService>();
            serviceCollection.AddScoped<ITotalFundsService, TotalFundsService>();
            serviceCollection.AddScoped<IRepositoryInOutServices, RepositoryInOutService>();
            serviceCollection.AddScoped<IDailyService, DailyService>();
            serviceCollection.AddScoped<ICarsService, CarsService>();
            serviceCollection.AddScoped<IFuelService, FuelService>();
            serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
            serviceCollection.AddScoped<IBuyerService, BuyerService>();

            serviceCollection.AddScoped<IRefrigeratorService, RefrigeratorService>();
            serviceCollection.AddScoped<IExternalEnvoicesService, ExternalEnvoicesService>();
            serviceCollection.AddScoped<ICoolingRoomsService, CoolingRoomsService>();
            serviceCollection.AddScoped<IOtherSalesService, OtherSalesService>();
            serviceCollection.AddScoped<IBillingService, BillingService>();
            serviceCollection.AddScoped<IDatabaseBackupService, DatabaseBackupService>();

            serviceCollection.AddScoped<IBoxesFieldService, BoxesFieldService>();

            //
        }


    }
}
