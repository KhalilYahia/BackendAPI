using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.Iservices
{
    public interface IFinancialEntitlementService
    {

        Task<List<AllFinancialEntitlementDto>> GetAllFinancialEntitlementsAsync();


    }
}
