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
using Newtonsoft.Json;
using YaznGhanem.Services.services;
using YaznGhanem.WebApi;
using Microsoft.AspNetCore.Authorization;



namespace YaznGhanem.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("[controller]")]   
        public class EmployeeController : ApiBaseController
        {
            private readonly IEmployeeService _EmployeeServices;

            public EmployeeController(IEmployeeService repositoryServices)
            {
                _EmployeeServices = repositoryServices;
            }

        [HttpPost("AddNewEmployee")]
        public async Task<ActionResult<int>> AddNewEmployee(InputEmployeeDto inDto)
        {
            var repositoryId = await _EmployeeServices.AddNewEmployee(inDto);
            return repositoryId;
        }
        [HttpPut("EditEmpolyee")]
        public async Task<ActionResult<bool>> EditEmpolyee(InputEmployeeDto inDto)
        {
            var repositoryId = await _EmployeeServices.Edit(inDto);
            return repositoryId;

        }

        [HttpOptions("DeleteEmployee")]
        public async Task<ActionResult<bool>> DeleteEmployee(int id)
        {
            var result = await _EmployeeServices.Delete(id);
            if (!result)
            {
                return Ok(false);
            }
            return Ok(result);
        }

        /// <summary>
        /// Get Employees by name ,
        /// اذا اردت جميع العمال ضع الاسم فارغ
        /// </summary>
        /// <param name="employeeName"></param>
        /// <returns></returns>
        [HttpGet("GetEmployees")]
            public async Task<ActionResult<List<EmployeeDto>>> GetEmployees(string employeeName=null)
            {
                var repositories = await _EmployeeServices.GetEmployees(employeeName);
                return Ok(repositories);
            }

        [HttpGet("GetAllAsync_ForDesktop")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAllAsync_ForDesktop(string hash)
        {
            var repositories = await _EmployeeServices.GetEmployees_ForDesktop();
            var dataString = JsonConvert.SerializeObject(repositories);
            var Newhash_ = Hash_kh.GetHash(JsonConvert.SerializeObject(dataString));
            if (Newhash_ == hash)
                return Ok(new { Data = "", Hash = Newhash_ });
            else
            {
                return Ok(new { Data = repositories, Hash = Newhash_ });
            }
        }

        [HttpGet("GetEmployee_ByEmployeeId")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee_ByEmployeeId(int employeeId)
        {
            var repositories = await _EmployeeServices.GetEmployee_ByEmployeeId(employeeId);
            return Ok(repositories);
        }

        #region DailyCheck

        [HttpPost("AddNewDailyCheck")]
        public async Task<ActionResult<int>> AddNewDailyCheck(InputDailyChekEmployeesDto dto)
        {
            var repositoryId = await _EmployeeServices.AddNewDailyCheck(dto);
            return repositoryId;
        }

        [HttpOptions("DeleteDailyCheck")]
        public async Task<ActionResult<bool>> DeleteDailyCheck(int DailyCheckId)
        {
            var result = await _EmployeeServices.DeleteDailyCheck(DailyCheckId);
            if (!result)
            {
                return Ok(false);
            }
            return Ok(result);
        }


        [HttpGet("GetDailyCheckEmployeeByEmployeeId")]
        public async Task<ActionResult<List<DailyChekEmployeesDto>>> GetDailyCheckEmployeeByEmployeeId(int employeeId)
        {
            var repository = await _EmployeeServices.GetDailyCheckEmployeeByEmployeeId(employeeId);
            if (repository == null)
            {
                return NotFound();
            }
            return Ok(repository);
        }

        #endregion

    }


}
