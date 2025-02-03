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
using System.Globalization;
using System.Security.Claims;



namespace YaznGhanem.Controllers
{
  
    public class ApiBaseController : ControllerBase
    {
        protected string lang = "ar";

        public LanguageHelper CurrentLanguage
        {
            get
            {
                if (Request.Headers.ContentLanguage.ToString() == "en")
                    return LanguageHelper.ENGLISH;
                else
                    return LanguageHelper.ARABIC;
            }
        }
        public string GetuserId
        {
            get
            {
               return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }
        }
        

    }
}
