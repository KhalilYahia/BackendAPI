using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface IBillingService
    {
        Task<BillingDto> GetBilling(Search_BillingDto dto);
    }
}
