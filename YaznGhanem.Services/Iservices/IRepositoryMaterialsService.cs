using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface IRepositoryMaterialsService
    {

        Task<int> AddAsync(RepositoryMaterialsDto materialDto);

        Task<bool> UpdateAsync(RepositoryMaterialsDto materialDto);

        Task<bool> DeleteAsync(int id);


        Task<List<RepositoryMaterialsDto>> GetAllAsync();
        Task<List<RepositoryMaterialsDto>> GetAllByCategoryId(int catId);

        Task<RepositoryMaterialsDto> GetByIdAsync(int id);

    }
}
