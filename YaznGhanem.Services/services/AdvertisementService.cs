using YaznGhanem.Domain;
using YaznGhanem.Services.Iservices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using YaznGhanem.Common;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;
using Microsoft.AspNetCore.Hosting;
using LLama.Common;
using LLama;

namespace YaznGhanem.Services.services
{
    public class AdvertisementService : IAdvertisementService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _host;
        public AdvertisementService(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _host = host;
        }

        public async Task<int> AddNewHome(InputAdvertismentDto dto)
        {
            var model =_mapper.Map<Advertisement>(dto);
            model.AddingDate = Utils.ServerNow;
            List<Properties> props_= (await _unitOfWork.repository<Properties>().GetAllAsync()).Where(m => dto.Properties.Keys.Contains(m.Id)).ToList();
          
            model.AdvertisementProperties= new List<AdvertisementProperties>();
            foreach(var single_propId in dto.Properties)
            {
                model.AdvertisementProperties.Add(
                    new AdvertisementProperties()
                    { 
                        PropertyId = single_propId.Key,
                        Value = single_propId.Value
                    });
            }
            model.AdvertismentSearch = new AdvertismentSearch();
            model.AdvertismentSearch.Data = GetEmbeddings(dto.Name + ", " + dto.Address + ", " + dto.Description);

            _unitOfWork.repository<Advertisement>().Add(model);
            await _unitOfWork.Complete();

            return model.Id;

        }
        public async Task<bool> ChangeHomeStatus(int Id)
        {
            var models = (await _unitOfWork.repository<Advertisement>().Get(m=>m.Id==Id)).ToList();
            if(models.Any())
            {
                var model = models.FirstOrDefault();
                if(model.Status== StatusOfAdvertisment.ACTIVE)
                    model.Status = StatusOfAdvertisment.STOPPED;
                else
                    model.Status = StatusOfAdvertisment.ACTIVE;
                _unitOfWork.repository<Advertisement>().Update(model);
                await _unitOfWork.Complete();
                return true;

           }
            return false;
        }

        public async Task<bool> DeleteHome(int Id)
        {
            var models = (await _unitOfWork.repository<Advertisement>().Get(m => m.Id == Id)).ToList();
            if (models.Any())
            {
                var model = models.FirstOrDefault();
                await DeleteImages(Id);
                foreach (var item_model in model.AdvertisementProperties)
                {
                    _unitOfWork.repository<AdvertisementProperties>().Delete(item_model);
                }

                if(model.AdvertismentSearch!=null)
                    _unitOfWork.repository<AdvertismentSearch>().Delete(model.AdvertismentSearch);

                _unitOfWork.repository<Advertisement>().Delete(model);
                await _unitOfWork.Complete();
                return true;

            }
            return false;
        }

        /// <summary>
        ///  هذا التابع فقط للاستخدام هنا
        ///  Return a vector for searching
        /// </summary>
        /// <param name="embeddings_"></param>
        /// <returns></returns>
        public float[] GetEmbeddings(string embeddings_)
        {
            /* 1- Install-Package LLamaSharp
             * 2- install-package LLamaSharp.Backend.Cpu
             * documntation https://github.com/SciSharp/LLamaSharp
             */
            // var modelPath = @"D:\Projects\MVC Projects\learning core\LLama\Models\all-MiniLM-L12-v2.Q8_0.gguf";
           // var p = _host.WebRootPath + Utils.PhysicalLLAMA_Model;
            var modelPath = @"all-MiniLM-L12-v2.Q8_0.gguf";
          //  modelPath = p + modelPath;
            var @params = new ModelParams(modelPath) { EmbeddingMode = true };
            using var weights = LLamaWeights.LoadFromFile(@params);
            var embedder = new LLamaEmbedder(weights, @params);
            float[] embeddings_result = embedder.GetEmbeddings(embeddings_).Result;

            return embeddings_result;// string.Join(", ", embeddings_result);
        }

        #region Get

        /// <summary>
        /// For admin
        /// </summary>
        /// <returns></returns>
        public async Task<List<SimplifyHomesDto>> GetAllHomesForAdmin()
        {
            var models = (await _unitOfWork.repository<Advertisement>().GetAllAsync(includeProperties: "City")).ToList();
            var res=_mapper.Map< List <Advertisement> ,List <SimplifyHomesDto>>(models);
            
            return res;
        }

        /// <summary>
        /// For Normal User
        /// </summary>
        /// <returns></returns>
        public async Task<List<SimplifyHomesDto>> GetAllHomesForNormalUser(string UserId)
        {
            var models = (await _unitOfWork.repository<Advertisement>().Get(m=>m.Status==StatusOfAdvertisment.ACTIVE)).OrderByDescending(ord=>ord.AddingDate).OrderBy(r=>r.Class).ToList();
            var res = _mapper.Map<List<Advertisement>, List<SimplifyHomesDto>>(models);
            int index_in_models = 0;
            foreach(var single_model in models)
            {
                if (single_model.Favorites.Where(m => m.UserId == UserId).Any())
                    res[index_in_models].IsInfavorite = true;
                index_in_models++;
            }
            return res;
        }

        /// <summary>
        /// Search fun For Normal User
        /// </summary>
        /// <returns></returns>
        public async Task<List<SimplifyHomesDto>> SearchForNormalUser(SearchHomesDto dto,string UserId)
        {
            var models = (await _unitOfWork.repository<Advertisement>().Get(
                                           m => m.Status == StatusOfAdvertisment.ACTIVE && 
                                                m.Price>dto.MinPrice && m.Price<dto.MaxPrice &&
                                                m.Area>dto.MinArea && m.Area<dto.MaxArea ))
                                                .OrderByDescending(ord => ord.AddingDate).OrderBy(r => r.Class).ToList();
            if(dto.Type==0)
            {
                models = models.Where(m => m.Type == TypeOfAdvertisment.SELLING).ToList();
            }
            else if(dto.Type==1) 
            {
                models = models.Where(m => m.Type == TypeOfAdvertisment.RENT).ToList();
            }
            var res = _mapper.Map<List<Advertisement>, List<SimplifyHomesDto>>(models);
            int index_in_models = 0;
            foreach (var single_model in models)
            {
                if (single_model.Favorites.Where(m => m.UserId == UserId).Any())
                    res[index_in_models].IsInfavorite = true;
                index_in_models++;
            }
            return res;
        }

        /// <summary>
        /// جلب كل المواقع على الخريطة
        /// </summary>
        /// <returns></returns>
        public async Task<List<LocationsOfHomesDto>> GetLocationsOfHomes()
        {
            var models = (await _unitOfWork.repository<Advertisement>().Get(m => m.Status == StatusOfAdvertisment.ACTIVE)).ToList();
            var res = _mapper.Map<List<Advertisement>, List<LocationsOfHomesDto>>(models);
            
            return res;
        }

        /// <summary>
        /// For all
        /// جلب المعلومات التفصيلية لمنزل
        /// </summary>
        /// <returns></returns>
        public async Task<DetailedHomeDto> GetDetailedHome(int Id,string UserId=null)
        {
            var models = (await _unitOfWork.repository<Advertisement>().Get(m => m.Id==Id)).ToList();
            if(models.Any())
            {
                var model = models.FirstOrDefault();
                var res = _mapper.Map<Advertisement, DetailedHomeDto>(model);
                res.Properties = new List<PropertyForDetailsDto>();
                foreach (var prop_ in model.AdvertisementProperties)
                {
                    res.Properties.Add(new PropertyForDetailsDto()
                    {
                        Name = prop_.Property.Name,
                        Id = prop_.Id,
                        Value = prop_.Value
                    });
                }
               // res.Images.ForEach(image => image= Utils.ImageAdvertismentURL+image);
                if (UserId != null)
                {
                    if (model.Favorites.Where(m => m.UserId == UserId).Any())
                        res.IsInfavorite = true;
                }

                return res;
            }
            return null;
        }

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
        public async Task<bool> AddImagesToProduct(int Id, string ImageName, bool IsPrimary)
        {
            List<Advertisement> model11;

            model11 = (await _unitOfWork.repository<Advertisement>().Get(m => m.Id == Id)).ToList();

            if (!model11.Any())
                return false;
            else
            {
                var model = model11.FirstOrDefault();
                //if (model.Restaurant.UserId != RestaurantManagerId)
                //    return false;
                if (model.Images != null)
                {
                    List<ImageOfAdvertisment> images_db = model.Images.ToList();
                    //if (images_db.Count > 2)
                    //{
                    //    return false;
                    //}
                    if (IsPrimary)
                    {
                        foreach (var im in images_db)
                        {
                            im.IsPrimary = false;
                        }
                    }
                    if (images_db.Count == 0)
                    {
                        model.Images = new List<ImageOfAdvertisment>()
                        {
                            new ImageOfAdvertisment()
                            {
                               IsPrimary = true,                               
                               Path = ImageName
                            }
                        };
                    }
                    else
                    {
                        model.Images.Add(new ImageOfAdvertisment()
                        {

                            IsPrimary = IsPrimary,
                            Path = ImageName

                        });

                    }
                   

                }
                else
                {

                    model.Images = new List<ImageOfAdvertisment>()
                        {
                            new ImageOfAdvertisment()
                            {
                               IsPrimary = true,
                               Path = ImageName
                            }
                        };

                }

                _unitOfWork.repository<Advertisement>().Update(model);
                await _unitOfWork.Complete();
                return true;
                //}
                //else
                //{
                //    return false;
                //}

            }
        }

        /// <summary>
        /// Delete Images from home
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteImages(int Id)
        {
            //var ImageName = ImageUrl.Replace(Utils.ImageRestaurantProductsURL, "");
            //List<ProductsImages> model11;
            string pa = "";
            var model11_temp = (await _unitOfWork.repository<ImageOfAdvertisment>().Get(m => (m.AdvertismentId == Id))).ToList();
            //model11 = model11_temp.Where(m => m.Name == ImageName).ToList();
            // var model11 = model101.Where(v => v.Name == ImageName);
            //var  p = ImageUrl.Replace(Utils.ImageRestaurantProductsURL, System.Web.HttpContext.Current.Server.MapPath("RestaurantsImages\\Products\\"));
            //  p = p.Replace("web\\AdminProducts\\DeletePhoto", "");
            if (!model11_temp.Any())
                return false;
            else
            {
                foreach(var image_ in model11_temp)
                {
                    var p = _host.WebRootPath + Utils.PhysicalImageAdvertisment + image_.Path;
                    try
                    {
                        File.Delete(p);
                    }
                    catch (Exception e1) { }

                    _unitOfWork.repository<ImageOfAdvertisment>().Delete(image_);
                }
                await _unitOfWork.Complete();
               
                return true;

            }

        }


        #endregion
    }
}
