using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface IBuyerService
    {
        Task<int> AddAsync(BuyerDto BuyerDto);

        Task<bool> UpdateAsync(BuyerDto BuyerDto);

        Task<bool> DeleteAsync(int id);


        Task<List<BuyerDto>> GetAllAsync();

        Task<BuyerDto> GetByIdAsync(int id);
    }
}
