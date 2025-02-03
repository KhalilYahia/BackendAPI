using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface IBoxesFieldService
    {
        #region Buy section for add to repository
        Task<int> AddAsync(InputBoxesDto inDto);


        #endregion



        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);



        Task<GetAllBoF_MainDto> GetBoFMainPage();

        Task<GetAllBoF_OperationsDto> GetBoFOperationsPage_ByUserId(int UserId);

        Task<BoF_DetailedDto> GetByIdAsync(int Operation_id);
        Task<BoF_InfoDTO> GetBoF_Info();
    }
}
