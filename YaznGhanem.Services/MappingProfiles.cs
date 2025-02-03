using AutoMapper;
using AutoMapper.Features;
using YaznGhanem.Common;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;
using YaznGhanem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Dto to Entity
            //CreateMap<AddBlogDto, Blog>();
            CreateMap<InputCategoryDto, Category>();

            CreateMap<Input_RepositoryInDto, Repository_InOut>()
                .ForMember(dest=>dest.Date,opt=>opt.MapFrom(src=>src.Date.AddHours(1)));
            CreateMap<InputDailyDto, Daily>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));
            CreateMap<Input_CarDto, Cars>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));
            CreateMap<Input_FuelDto, Fuel>()
                .ForMember(dest=>dest.Date,opt=>opt.MapFrom(src=>src.Date.AddHours(1)));

            CreateMap<InputEmployeeDto, Employees>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));
            CreateMap<InputDailyChekEmployeesDto, DailyChekEmployees>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));

            
            CreateMap<InputRefrigeratorDto, Refrigerator>()
                   .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));
            CreateMap<InputRefrigeratorDetailsDto, RefrigeratorDetails>()
                 .ForMember(dest => dest.CountOfBoxes, opt => opt.MapFrom(src => src.TotalBoxes)); ;

            CreateMap<InputExternalEnvoicesDto, ExternalEnvoices>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));
            CreateMap<InputCoolingRoomsDto, CoolingRooms>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));
            CreateMap<InputOtherSalesDto, OtherSales>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.AddHours(1)));
            //<BoFOperations>(inDto);

            CreateMap<InputBoxesDto, BoFOperations>();
            CreateMap<InputBoxesDetailsDto, BoFOpDetails>();
            //
            #endregion

            //






            #region Entity To Dto 
            CreateMap<Category, EditCategoryDto>();
            CreateMap<Category, CategoryDto>();
            

            CreateMap<CustomUser, UserDto>();
            CreateMap<CustomRole, RoleDto>();
            
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<SupplierOfFarms, SupplierDto>()
                .ForMember(dest => dest.SupplierName, opt => opt.MapFrom(src => src.Name));
            CreateMap<RepositoryMaterials, RepositoryMaterialsDto>().ReverseMap();

            //
            
            CreateMap<Repository_InOut, Ripository_InDetailsDto>();
            CreateMap<Repository_InOut, Ripository_InSimplifyDto>();

            CreateMap<FinancialEntitlement, AllFinancialEntitlementDto>()
                .ForMember(dest => dest.Operations,
              opts => opts.MapFrom(src => src.Repository_Ins
        .Select(ri => $"{ri.Date.ToString("dd/MM/yyyy")} : شراء {ri.Name} - {ri.BuyPriceOfAll} ل.س")
        .Concat(src.Fuels
            .Select(f => $"{f.Date.ToString("dd/MM/yyyy")} : شراء {f.Type} - {f.TotalPrice} ل.س"))
        .Concat(src.Cars
            .Select(f => $"{f.Date.ToString("dd/MM/yyyy")} : اجار سيارة - {f.TotalPrice} ل.س"))
        .Concat(src.Dailies
            .Select(f => $"{f.Date.ToString("dd/MM/yyyy")} : شراء {f.MaterialName} - {f.BuyPriceOfAll} ل.س"))
         .Concat(src.SupplierOfFarmsDailies
            .Select(f => $"{f.Date.ToString("dd/MM/yyyy")} : كلفة قص {f.MaterialName} - {f.CuttingCostOfAll} ل.س"))
         .Concat(src.WaxingFactoryDailies
            .Select(f => $"{f.Date.ToString("dd/MM/yyyy")} : كلفة التشميع {f.MaterialName} - {f.WaxingCostOfAll} ل.س"))
        .ToList()));

            CreateMap<Daily, List<Operations_Desktop>>()
               .AfterMap((src, dest) =>
               {
                   // First entry
                   var entry1 = new Operations_Desktop
                   {
                       Details = "شراء " + src.MaterialName,
                       NameOfClient = src.FarmerName,
                       MoneyAmount = src.BuyPriceOfAll,
                       Date = src.Date,
                       Type = "مصاريف"
                   };

                   // Second entry
                   var entry2 = new Operations_Desktop
                   {
                       Details = "كلفة قص " + src.MaterialName,
                       NameOfClient = src.Supplier,
                       MoneyAmount = src.CuttingCostOfAll,
                       Date=src.Date,
                       Type = "مصاريف"
                   };

                   // Third entry
                   var entry3 = new Operations_Desktop
                   {
                       Details = "كلفة التشميع " + src.MaterialName,
                       NameOfClient = src.WaxingFactory_As_dealer!=null? src.WaxingFactory_As_dealer.SupplierName:"",
                       MoneyAmount = src.WaxingCostOfAll,
                       Date = src.Date,
                       Type = "مصاريف"
                   };

                   // Add both entries to the destination list
                   dest.Add(entry1);
                   dest.Add(entry2);
                   dest.Add(entry3);
               });


            CreateMap<Cars, Operations_Desktop>()
                .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "أجار سيارة " ))
               .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.DriverName))
               .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.TotalPrice))
               .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "مصاريف"));
           
            // يجب التأكد منها في السيرفس
            // فقط في حالة دفع أجار
            CreateMap<DailyChekEmployees, Operations_Desktop>()
               .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "دفع راتب لورشة"))
              .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.Employee.workshopName))
              .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.PaidWage))
              .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "دفعة لورشة"));


            CreateMap<Fuel, Operations_Desktop>()
                .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "شراء " + src.Type))
               .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.SourceName))
               .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.TotalPrice))
               .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "مصاريف"));

            CreateMap<Repository_InOut, Operations_Desktop>()
               .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "شراء " + src.Name))
              .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.SupplierName))
              .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.BuyPriceOfAll))
              .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "مصاريف"));

            CreateMap<Refrigerator, Operations_Desktop>()
              .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "بيع براد" ))
             .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.BuyerName))
             .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.TotalSalesPriceOfAll))
             .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "إيراد"));

            CreateMap<ExternalEnvoices, Operations_Desktop>()
              .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "فواتير خارجية، مبيع "+src.MaterialName))
             .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.BuyerName))
             .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.SalesPriceOfAll))
             .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "إيراد"));

            CreateMap<CoolingRooms, Operations_Desktop>()
              .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "أجار غرفة تبريد، الغرفة  " + src.Room+ " / المادة: "+src.MaterialName))
             .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.ClientName))
             .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.CostOfAll))
             .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "إيراد"));

            CreateMap<OtherSales, Operations_Desktop>()
             .ForMember(dest => dest.Details, opts => opts.MapFrom(src => "سعر مبيع: " + src.MaterialName))
            .ForMember(dest => dest.NameOfClient, opts => opts.MapFrom(src => src.BuyerName))
            .ForMember(dest => dest.MoneyAmount, opts => opts.MapFrom(src => src.SalesPriceOfAll))
            .ForMember(dest => dest.Type, opts => opts.MapFrom(src => "إيراد"));

            //

            CreateMap<FinancialPayment, FinancialPaymentDto>().ReverseMap();
            CreateMap<TotalFunds, TotalFundsDto>().ReverseMap();

            CreateMap<Daily, DailyDetailsDto>();
            CreateMap<Daily, DailySimplifyDto>();

            CreateMap<Cars, Cars_InDetailsDto>();
            CreateMap<Cars, Cars_InSimplifyDto>();

            CreateMap<Fuel, Fuel_InDetailsDto>();
            CreateMap<Fuel, Fuel_InSimplifyDto>();

            CreateMap<DailyChekEmployees, DailyChekEmployeesDto>();
            CreateMap<Employees, EmployeeDto>();

            CreateMap<Buyers, BuyerDto>().ReverseMap();

            CreateMap<Refrigerator, RefrigeratorDto>();
            CreateMap<RefrigeratorDetails, RefrigeratorDetailsDto>();
            CreateMap<Refrigerator, RefrigeratorSimplifyDto>();

            CreateMap<ExternalEnvoices, ExternalEnvoicesDetailsDto>();
            CreateMap<ExternalEnvoices, ExternalEnvoicesSimplifyDto>();

            CreateMap<CoolingRooms, CoolingRoomsDetailsDto>();
            CreateMap<CoolingRooms, CoolingRoomsSimplifyDto>();

            CreateMap<OtherSales, OtherSalesDetailsDto>();
            CreateMap<OtherSales, OtherSalesSimplifyDto>();
            CreateMap<InputDailyDto, InputDailyDto>();

            CreateMap<BoFTotal, GetAllBoF_TotalDto>();
            CreateMap<BoFOperations, GetBoF_AllOperationsDto>()
               .ForMember(dest => dest.Direction, opts => opts.MapFrom(src =>  (src.BoFOpDetails.Any(m=>m.Direction=="خارج"))?"خارج":(
               (src.BoFOpDetails.Any(m => m.Direction == "داخل فارغ")&& src.BoFOpDetails.Any(m => m.Direction == "داخل ممتلئ"))?"داخل فارغ | داخل ممتلئ": src.BoFOpDetails.First().Direction)));
           
            CreateMap<BoFOperations, BoF_DetailedDto>();
            CreateMap<BoFOpDetails, BoF_OpDetailsDto>();
            CreateMap<BoFUser, BoF_UserDTO>();
            //
            #endregion

        }

    }
}
