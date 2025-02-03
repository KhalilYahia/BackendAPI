using YaznGhanem.Common;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;
using LLama.Common;
using LLama;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.Iservices
{
    public interface IAdvertisementService
    {
        Task<int> AddNewHome(InputAdvertismentDto dto);
        Task<bool> ChangeHomeStatus(int Id);

        Task<bool> DeleteHome(int Id);

       

    #region Get

    /// <summary>
    /// For admin
    /// </summary>
    /// <returns></returns>
        Task<List<SimplifyHomesDto>> GetAllHomesForAdmin();

    /// <summary>
    /// For Normal User
    /// </summary>
    /// <returns></returns>
        Task<List<SimplifyHomesDto>> GetAllHomesForNormalUser(string UserId);

    /// <summary>
    /// جلب كل المواقع على الخريطة
    /// </summary>
    /// <returns></returns>
        Task<List<LocationsOfHomesDto>> GetLocationsOfHomes();

        /// <summary>
        /// Search fun For Normal User
        /// </summary>
        /// <returns></returns>
        Task<List<SimplifyHomesDto>> SearchForNormalUser(SearchHomesDto dto, string UserId);

    /// <summary>
    /// For all
    /// جلب المعلومات التفصيلية لمنزل
    /// </summary>
    /// <returns></returns>
        Task<DetailedHomeDto> GetDetailedHome(int Id, string UserId = null);

    #endregion


    #region Image

    /// <summary>
    /// 
    /// This function for admin
    /// Add Image to Home
    /// </summary>
    /// <param name="Id">Product Id</param>
    /// <param name="ImageName"></param>
    /// <param name="IsPrimary"></param>
    /// <returns></returns>
        Task<bool> AddImagesToProduct(int Id, string ImageName, bool IsPrimary);

  
    #endregion
    }
}
