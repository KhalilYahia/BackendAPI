using YaznGhanem.Common;
using YaznGhanem.Services.DTO;
using YaznGhanem.Services.Iservices;
using LLama;
using LLama.Common;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Hosting;
using NuGet.Packaging.Signing;
using System.Net.NetworkInformation;



namespace YaznGhanem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminCityController : ApiBaseController
    {
        private readonly ICityService _CityService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;
        public AdminCityController(ICityService _CityService, Microsoft.AspNetCore.Hosting.IHostingEnvironment host)
        {
            this._CityService = _CityService;
            _host = host;
        }

        [HttpPost ("AddCity")]
        public async Task<int> AddCity(InputCityDto dto)
        {//LanguageHelper Language,
         //var RestaurantInfo= _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return await _CityService.Add(CurrentLanguage,dto);
            // return _languageService.Add(dto);
        }

        [HttpPost("EditCity")]
        public async Task<bool> EditCity(InputCityDto dto)
        {
            // var RestaurantInfo = _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return await _CityService.Edit(CurrentLanguage,dto);
        }

        [HttpPost("Delete")]
        public async Task<ActionResult<bool?>> Delete(int id)
        {
            var delete = _CityService.Delete(id);
            if (delete != null)
                return await delete;
            else
                return BadRequest(new { message = "لايمكن حذف هذه المدينة" });
        }

     
        
        [HttpGet("GetAllCities")]
        public async Task<ActionResult<List<CityDto>>> GetAllCities()
        {
            var model = await _CityService.GetAllCities(CurrentLanguage);
            if (model.Any())
                return model;
            else
               return NotFound(new { Message = "لايوجد مدن" });

        }

        [HttpGet("GetCityById")]
        public async Task<ActionResult<CityDto>> GetCityById(int CityId)
        {
            var model = await _CityService.GetCityById(CurrentLanguage, CityId);
            if (model!=null)
                return model;
            else
                return NotFound(new { Message = "لايوجد مدن" });

        }


    }
}
