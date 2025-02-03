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
    public interface IRefrigeratorService
    {
        #region Add
        Task<int> AddAsync(InputRefrigeratorDto inDto);


        #endregion

        Task<bool> DeleteAsync(int id);

        Task<List<RefrigeratorSimplifyDto>> GetAllAsync();

        Task<RefrigeratorDto> GetByIdAsync(int id);

        Task<List<RefrigeratorDto>> GetAllAsync_ForDesktop();

    }
}
