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
using Microsoft.AspNetCore.Authorization;



namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]
    public class AdminCategoryController : ApiBaseController
    {
        private readonly ICategoryService _CategoryService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;
        public AdminCategoryController(ICategoryService _CategoryService, Microsoft.AspNetCore.Hosting.IHostingEnvironment host)
        {
            this._CategoryService = _CategoryService;
            _host = host;
        }

        [HttpPost ("AddCategory")]
        public int AddCategory(InputCategoryDto dto)
        {//LanguageHelper Language,
         //var RestaurantInfo= _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return _CategoryService.Add(dto);
            // return _languageService.Add(dto);
        }

        [HttpPost("EditCategory")]
        public bool EditCategory(InputCategoryDto dto)
        {
            // var RestaurantInfo = _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return _CategoryService.Edit(dto);
        }

        [HttpOptions("Delete")]
        public async Task<ActionResult<bool?>> Delete(int id)
        {
            var delete = _CategoryService.Delete(id);
            if (delete != null)
                return await delete;
            else
                return BadRequest(new { message = "هذه الفئة لها أولاد، قم بحذف أولادها أولا" });
        }

     

        
        [HttpPost("GetAll")]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            var model = await _CategoryService.GetCategories();
            if (model.Any())
                return model;
            else
               return NotFound(new { Message = "لايوجد فئات" });

        }
       

    }
}
