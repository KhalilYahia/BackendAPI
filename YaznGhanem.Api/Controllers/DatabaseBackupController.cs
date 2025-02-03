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
    public class DatabaseBackupController : ApiBaseController
    {
        private readonly IDatabaseBackupService _DatabaseBackupService;
        public DatabaseBackupController(IDatabaseBackupService DatabaseBackupService)
        {
            _DatabaseBackupService = DatabaseBackupService;
        }

       
        [HttpGet("ExportDatabaseToJsonAsync")]
        public async Task<string> ExportDatabaseToJsonAsync()
        {
            return await _DatabaseBackupService.ExportDatabaseToJsonAsync();
           
        }

       


    }
}
