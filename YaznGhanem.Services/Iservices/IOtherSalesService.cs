using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface IOtherSalesService
    {
        #region Add
        Task<int> AddAsync(InputOtherSalesDto inDto);

        #endregion

        Task<bool> DeleteAsync(int id);

        Task<List<OtherSalesSimplifyDto>> GetAllAsync();

        Task<OtherSalesDetailsDto> GetByIdAsync(int id);
        Task<List<OtherSalesDetailsDto>> GetAllAsync_Fordesktop();

    }
}
