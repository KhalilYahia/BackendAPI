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
    public interface IExternalEnvoicesService
    {
        #region Add
        Task<int> AddAsync(InputExternalEnvoicesDto inDto);

        #endregion

        Task<bool> DeleteAsync(int id);

        Task<List<ExternalEnvoicesSimplifyDto>> GetAllAsync();

        Task<ExternalEnvoicesDetailsDto> GetByIdAsync(int id);
        Task<List<ExternalEnvoicesDetailsDto>> GetAllAsync_Fordesktop();

    }
}
