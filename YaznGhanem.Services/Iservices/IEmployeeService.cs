using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.DTO;

namespace YaznGhanem.Services.Iservices
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Add new Employee, 
        /// إضافة ورشة جديدة
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<int> AddNewEmployee(InputEmployeeDto dto);

        /// <summary>
        /// update exist Employee,
        /// التعديل سيكون فقط على الحقول التي يتم إدخالها
        /// ولن تؤثر على الأجور السابقة للعامل أي انه
        /// اي زيادة في الأجر لن تنعكس على عدد ساعات الدوام السابقة
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> Edit(InputEmployeeDto dto);

        /// <summary>
        /// delete Employee, 
        /// حذف عامل لن يؤثر على الأموال المدفوعة له أي لن يؤثر على الصندوق أبدا
        /// اذا اعاد هذا التابع null فإنه يجب عليك حذف تفقد العامل
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool>? Delete(int id);

        /// <summary>
        /// Get Employees by name ,
        /// اذا اردت جميع العمال ضع الاسم فارغ
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeeDto>> GetEmployees(string workshopName);
        /// <summary>
        /// Get all Employees  ,
        /// 
        /// </summary>
        /// <returns></returns>
        Task<List<EmployeeDto>> GetEmployees_ForDesktop();

        Task<EmployeeDto> GetEmployee_ByEmployeeId(int EmployeeId);

        /// <summary>
        /// Get Employee by id
        /// </summary>
        /// <returns></returns>
        Task<List<DailyChekEmployeesDto>> GetDailyCheckEmployeeByEmployeeId(int EmployeeId);


        #region Financial

        /// <summary>
        /// إضافة تفقد يومي 
        /// في حال اعاد هذا التابع قيمة صفر فإنه يعني أن هذا الموظف غير موجود
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<int> AddNewDailyCheck(InputDailyChekEmployeesDto dto);



        /// <summary>
        /// حذف تفقد يومي 
        /// </summary>
        /// <param name="DailyCheckId">معرف التفقد اليومي</param>
        /// <returns></returns>
        Task<bool> DeleteDailyCheck(int DailyCheckId);




        #endregion
    }
}
