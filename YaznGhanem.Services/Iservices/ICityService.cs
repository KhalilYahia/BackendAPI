using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.Iservices
{
    public interface ICityService
    {
        #region InputCityDto
        /// <summary>
        /// Add City
        /// </summary>
        /// <param name="Language"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<int> Add(LanguageHelper Language, InputCityDto dto);
        /// <summary>
        /// Update exist city
        /// </summary>
        /// <param name="Language"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> Edit(LanguageHelper Language, InputCityDto dto);
        /// <summary>
        /// Delete city by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns> </returns>
        Task<bool?> Delete(int Id);

        #endregion

        #region CityDto
        Task<List<CityDto>> GetAllCities(LanguageHelper language);
        Task<CityDto> GetCityById(LanguageHelper language, int id);
        #endregion

        //IPagedList<CityDto> searchAndOrder(List<CityDto> allCities, string CurrentFilter, int? Page, string SearchString, NawafizApp.Common.Sort sortOrder = Sort.IdDown_Up);


        #region Validator

        Task<bool> IsNameUnique(string name, int? id);

        Task<bool> IsExistId(int id);

        #endregion


    }
}
