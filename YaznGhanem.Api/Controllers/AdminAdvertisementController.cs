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
using YaznGhanem.WebApi.AssestanceClasses;
using PagedList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;




namespace YaznGhanem.Controllers
{
    [Authorize]
    [ApiController]
    [EnableCors("AllowAll")]
    [Route("[controller]")]
    public class AdminAdvertisementController : ApiBaseController
    {
        private readonly IAdvertisementService _AdvertisementService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;
        public AdminAdvertisementController(IAdvertisementService _AdvertisementService, Microsoft.AspNetCore.Hosting.IHostingEnvironment host)
        {
            this._AdvertisementService = _AdvertisementService;
            _host = host;
        }

        #region Add, Update, Remove and add_Image

        [HttpPost ("AddNewHome")]
        public async Task<int> AddNewHome(InputAdvertismentDto dto)
        {//LanguageHelper Language,
         //var RestaurantInfo= _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return await _AdvertisementService.AddNewHome(dto);
            // return _languageService.Add(dto);
        }
        
        [HttpOptions("ChangeHomeStatus")]
        public async Task<bool> ChangeHomeStatus(int id)
        {
            // var RestaurantInfo = _IRestaurantService.GetRestaurantDetailed_Info(dto.RestaurantID);
            return await _AdvertisementService.ChangeHomeStatus(id);
        }

        [HttpOptions("DeleteHome")]
        public async Task<ActionResult<bool?>> DeleteHome(int id)
        {
            var delete = _AdvertisementService.DeleteHome(id);
            if (delete != null)
                return await delete;
            else
                return BadRequest(new { message = "حدث خطأ عند الحذف" });
        }

        [HttpPost("AddImagesToProduct")]
        public async Task<ActionResult<string>> AddImagesToProduct(IFormFile file, int AdvertismentId, bool IsPrimary)
        {
            
            if(file.ContentType!= "image/jpeg" && file.ContentType != "image/jpg " && file.ContentType != "image/png" && file.ContentType != "image/Gif")
            {
                return Content("This file not supported");
            }
           
            if (file.FileName == null || file.FileName.Length == 0)
            {
                return Content("File not selected");
            }
            //var path = Path.Combine(_environment.WebRootPath, "Images/", file.FileName);
            Guid g = Guid.NewGuid();
            var directoryPath = _host.WebRootPath + Utils.PhysicalImageAdvertisment;
            // Ensure the directory exists
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string uniqueFileName = g + file.FileName;
            string path = Path.Combine(directoryPath, uniqueFileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Close();
            }
            var res_ = await _AdvertisementService.AddImagesToProduct( AdvertismentId, g.ToString() + file.FileName, IsPrimary);
            if (res_)
            {
                return  g + file.FileName;
            }
            return BadRequest(new { Message = "حدث خطأ أثناء ترفيع الصورة" });

        }

        #endregion


        #region GetAll, by_Id, GetAll_Locations and get_Image

        
        [HttpGet("GetAllHomesForAdmin")]
        public async Task<ActionResult<List<SimplifyHomesDto>>> GetAllHomesForAdmin(int page)
        {
            int pageSize = 10;
            var model = await _AdvertisementService.GetAllHomesForAdmin();
            if (model.Any())
            {
                var res_ = model.ToPagedList(page, pageSize); //ToPagedList

                return Ok(new PagedResponse<List<SimplifyHomesDto>>(res_.ToList(), page, pageSize, res_.PageCount, res_.TotalItemCount));
            }
                
            else
               return NotFound(new { Message = "لايوجد نتائج" });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AdvertismentId"></param>
        /// <returns></returns>
        [HttpPost("GetDetailedHome")]
        public async Task<ActionResult<DetailedHomeDto>> GetDetailedHome(int AdvertismentId)
        {
            var model = await _AdvertisementService.GetDetailedHome(AdvertismentId);
            if (model!=null)
                return model;
            else
                return NotFound(new { Message = "لايوجد نتائج" });

        }
        /// <summary>
        /// جلب كل المواقع على الخريطة
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetLocationsOfHomes")]
        public async Task<ActionResult<List<LocationsOfHomesDto>>> GetLocationsOfHomes()
        {
            var model = await _AdvertisementService.GetLocationsOfHomes();
            if (model != null)
                return model;
            else
                return NotFound(new { Message = "لايوجد نتائج" });

        }


        [AllowAnonymous]
        [HttpGet("GetImg")]
        public IActionResult GetImg(string imagePath)
        {
            try
            {
                var image = System.IO.File.OpenRead(_host.WebRootPath+Utils.PhysicalImageAdvertisment + imagePath);
                return File(image, "image/jpeg");
            }
            catch
            {
                return NotFound(new { Message = "الصورة غير موجودة" });
            }
          
        }

#endregion
    }
}
