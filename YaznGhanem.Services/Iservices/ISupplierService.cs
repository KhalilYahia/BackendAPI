using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface ISupplierService
    {
        Task<int> AddAsync(SupplierDto supplierDto);

        Task<bool> UpdateAsync(SupplierDto supplierDto);

        Task<bool> DeleteAsync(int id);


        Task<List<SupplierDto>> GetAllAsync();
        Task<List<SupplierDto>> GetAllSupplierOfFarmsAsync();

        Task<SupplierDto> GetByIdAsync(int id);
    }
}
