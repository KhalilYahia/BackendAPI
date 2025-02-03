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
    public interface ICoolingRoomsService
    {
        #region Add
       Task<int> AddAsync(InputCoolingRoomsDto inDto);


        #endregion

        Task<bool> DeleteAsync(int id);

        Task<List<CoolingRoomsSimplifyDto>> GetAllAsync();

        Task<CoolingRoomsDetailsDto> GetByIdAsync(int id);
        Task<List<CoolingRoomsDetailsDto>> GetAllAsync_Fordesktop();
    }
}
