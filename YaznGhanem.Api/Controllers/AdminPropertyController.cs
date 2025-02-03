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
using Swashbuckle.AspNetCore.Annotations;
using System.Net;



namespace YaznGhanem.Controllers
{

    [ApiController]
    [Route("[controller]")]
 
    public class AdminPropertyController : ApiBaseController
    {
        private readonly IPropertyService _PropertyService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;
        public AdminPropertyController(IPropertyService _PropertyService, Microsoft.AspNetCore.Hosting.IHostingEnvironment host)
        {
            this._PropertyService = _PropertyService;
            _host = host;
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost ("AddNewProperty")]
        public async Task<int> AddNewProperty(PropertyDto dto)
        {
            return await _PropertyService.AddNewProperty(dto);
            // return _languageService.Add(dto);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("EditProperty")]
        public async Task<bool> EditProperty(PropertyDto dto)
        {
            // var RestaurantInfo = _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return await _PropertyService.EditProperty(dto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("RemoveProperty")]
        public async Task<ActionResult<bool?>> RemoveProperty(int id)
        {
            var delete = _PropertyService.RemoveProperty(id);
            if (delete != null)
                return await delete;
            else
                return BadRequest(new { message = "لايمكن حذف هذه الخاصية" });
        }

     
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPropertiesForAdmin")]
        public async Task<ActionResult<List<PropertyDto>>> GetAllPropertiesForAdmin()
        {
            var model = await _PropertyService.GetAllPropertiesForAdmin();
            if (model.Any())
                return model;
            else
               return NotFound(new { Message = "لايوجد خواص" });

        }

        //[HttpPost("GetCityById")]
        //public async Task<ActionResult<CityDto>> GetCityById(int CityId)
        //{
        //    var model = await _CityService.GetCityById(CurrentLanguage, CityId);
        //    if (model!=null)
        //        return model;
        //    else
        //        return NotFound(new { Message = "لايوجد مدن" });

        //}


    }

   
}
