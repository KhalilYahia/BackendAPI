using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface ITotalFundsService
    {
        Task<TotalFundsDto> GetAllAsync();
        Task<ReportDto> GetAllOpt();
        Task<bool> UpdateFunds(TotalFundsDto dto);
        Task<Report_DesktopDto> GetAllOpt_Fordesktop();
        Task<ReportDto> GetAllOpt_UsingSQLView();
        Task<ReportDto> GetAllOpt_UsingSP();
    }
}
