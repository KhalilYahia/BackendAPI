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
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Security.Claims;




namespace YaznGhanem.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class UserAdvertisementController : ApiBaseController
    {
        private readonly IAdvertisementService _AdvertisementService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;
        public UserAdvertisementController(IAdvertisementService _AdvertisementService, Microsoft.AspNetCore.Hosting.IHostingEnvironment host)
        {
            this._AdvertisementService = _AdvertisementService;
            _host = host;
        }

       


        #region GetAll, by_Id, GetAll_Locations and get_Image

        
        [HttpGet("GetAllHomesForNormalUser")]
        public async Task<ActionResult<List<SimplifyHomesDto>>> GetAllHomesForNormalUser(int page)
        {
           
            int pageSize = 10;
            var model = await _AdvertisementService.GetAllHomesForNormalUser(GetuserId);
            if (model.Any())
            {
                var res_ = model.ToPagedList(page, pageSize);

                return Ok(new PagedResponse<List<SimplifyHomesDto>>(model, page, pageSize, res_.PageCount, res_.TotalItemCount));
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
            var model = await _AdvertisementService.GetDetailedHome(AdvertismentId, GetuserId);
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

        [HttpPost("SearchForNormalUser")]
        public async Task<ActionResult<List<SimplifyHomesDto>>> SearchForNormalUser(SearchHomesDto dto)
        {
            if(dto.MinArea==0)
                dto.MinArea = -1;
            if (dto.MinPrice == 0) dto.MinPrice = -1;
            int pageSize = 10;
            var model = await _AdvertisementService.SearchForNormalUser(dto, GetuserId);
            
            if (model.Any())
            {
                var deleteme = HttpContext.User; // delete this
                var res_ = model.ToPagedList(dto.page, pageSize);

                return Ok(new PagedResponse<List<SimplifyHomesDto>>(res_.ToList(), dto.page, pageSize, res_.PageCount, res_.TotalItemCount));
            }

            else
                return NotFound(new { Message = "لايوجد نتائج" });

        }
        //Task<List<SimplifyHomesDto>> SearchForNormalUser(SearchHomesDto dto, string UserId);

        [HttpGet("GetImg")]
        public IActionResult GetImg(string imagePath,int? Width,int? Height)
        {
            try
            {
                if (Width == null)
                    Width = 0;
                if(Height == null) Height = 0;
                //var image = System.IO.File.OpenRead(Utils.PhysicalImageAdvertisment + imagePath);
                var path = _host.WebRootPath + Utils.PhysicalImageAdvertisment + imagePath;
                Image img = Image.FromFile(path);
                if (Width == 0)
                    Width = (int)(((double)img.Height / img.Width) * Height);
                else if (Height == 0)
                    Height = (int)(((double)img.Height / img.Width) * Width);
                if (Height == 0 && Width == 0)
                {
                    Width = img.Width;
                    Height = img.Height;
                }

                Image _img = new Bitmap(Width.Value, Height.Value);
                //long len = new System.IO.FileInfo(context.Server.MapPath(context.Request.FilePath.ToString().Replace("kiwi.ashx", ""))).Length;

                Graphics graphics = Graphics.FromImage(_img);

                var extension = Path.GetExtension(path);

                //Resize picture according to size

                // graphics.DrawImage(img, 0, 0, Width, Height);
                Rectangle rect = new Rectangle(0, 0, Width.Value, Height.Value);
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.DrawImage(img, rect);
                //graphics.DrawImageUnscaled()


                graphics.Dispose();





                return File(converterDemo(_img), "image/jpeg");
            }
            catch
            {
                return NotFound(new { Message = "الصورة غير موجودة" });
            }
          
        }

        public static byte[] converterDemo(Image x)
        {
            ImageConverter _imageConverter = new ImageConverter();
            byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));
            return xByte;
        }

        #endregion
    }
}
