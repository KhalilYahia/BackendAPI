using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using YaznGhanem.Domain.Entities;
using System.Text.RegularExpressions;

namespace YaznGhanem.Services.Iservices
{
    public interface IRepositoryInOutServices
    {

        #region Buy section for add to repository
        Task<int> AddAsync(Input_RepositoryInDto inDto);


        #endregion



        /// <summary>
        /// هذا التابع لم أقم بفحصه بعد
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);



        Task<List<Ripository_InSimplifyDto>> GetAllAsync();

        Task<Ripository_InDetailsDto> GetByIdAsync(int id);

        Task<List<Ripository_InDetailsDto>> GetAll_Desktop();

    }
}
