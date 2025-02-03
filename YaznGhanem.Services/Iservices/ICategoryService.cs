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
    public interface ICategoryService
    {

        int Add(InputCategoryDto dto);

        bool Edit(InputCategoryDto dto);

        Task<bool?> Delete(int id);


        Task<List<CategoryDto>> GetCategories();


    }
}
