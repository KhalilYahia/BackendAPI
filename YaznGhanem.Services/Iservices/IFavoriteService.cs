using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.Iservices
{
    public interface IFavoriteService
    {
        #region InputFavoriteDto
        Task<int> AddToFavorite(FavoriteDto dto);


        Task<bool> RemoveFromFavorite(int favoriteId, string UserId);

       

        #endregion


        #region FavoriteDto

        Task<List<FavoriteDto>> GetFavorites(string UserGuid);

        #endregion
    }
}
