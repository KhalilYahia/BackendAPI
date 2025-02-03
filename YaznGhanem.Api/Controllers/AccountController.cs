using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

using FluentValidation.AspNetCore;
using System.Drawing.Printing;
using PagedList;
using YaznGhanem.WebApi.AssestanceClasses;
using NuGet.Common;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using YaznGhanem.Domain.Entities;
using AutoMapper;
using YaznGhanem.Domain.Entities;
using YaznGhanem.Services.Iservices;
using YaznGhanem.Services.DTO;
using YaznGhanem.Common;
using YaznGhanem.Services.services;


namespace YaznGhanem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly ITokenService _tokenGenerator;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _host;
        private readonly IEmailService _IEmailService;
        private readonly IUserService _IUserService;

        private readonly IRepositoryMaterialsService _repositoryMaterialsService;
        private readonly ISupplierService _supplierService;
        private readonly IBuyerService _BuyerService;
        private readonly IEmployeeService _EmployeeService;

        private readonly IMapper _mapper;
        public AccountController(UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager,
            ITokenService _tokenGenerator, Microsoft.AspNetCore.Hosting.IHostingEnvironment _host, IEmailService _IEmailService, IUserService iUserService,
            IRepositoryMaterialsService repositoryMaterialsService, ISupplierService supplierService, IBuyerService buyerService,
            IEmployeeService EmployeeService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this._tokenGenerator = _tokenGenerator;
            this._host = _host;
            this._IEmailService = _IEmailService;
            _IUserService = iUserService;
            _mapper = mapper;

            _repositoryMaterialsService = repositoryMaterialsService;
            _supplierService = supplierService;
            _BuyerService = buyerService;
            _EmployeeService = EmployeeService;
        }

        // Get user by email
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound($"User with email {email} not found.");
            }
            var role = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                Token = "",
                Role = role.FirstOrDefault(),
                PhoneNumber = user.PhoneNumber,
                DisplayName=user.DisplayName==null?"Name": user.DisplayName
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("EditUserInfo")]
        public async Task<ActionResult<bool>> EditUserInfo(UserDto dto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);

                if (user == null)
                {
                    return NotFound("User not found.");
                }
                user.PhoneNumber = dto.PhoneNumber;
                user.DisplayName = dto.DisplayName;

                var old_role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                if (old_role != dto.Roll)
                {
                    await _userManager.RemoveFromRoleAsync(user, old_role);
                    await _userManager.AddToRoleAsync(user, dto.Roll);
                }
                await _userManager.UpdateAsync(user);

                return Ok(true);
            }
            return BadRequest(ModelState);
        }

        //[Authorize(Roles="Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int page)
        {
            int pageSize = 10;
            var user = await _IUserService.GetAllUsers_forAdmin();
           
            if (user == null)
            {
                return NotFound($"not users");
            }
            var res_ = user.ToPagedList(page, pageSize);
           
            return Ok(new PagedResponse<List<UserDto>>(res_.ToList(), page,pageSize,res_.PageCount, res_.TotalItemCount));
        }

        [HttpGet("GetAllUsersByRole")]
        public async Task<IActionResult> GetAllUsersByRole(int page,string roleName)
        {
            int pageSize = 100;
            var user = await _userManager.GetUsersInRoleAsync(roleName);

            if (user == null)
            {
                return NotFound($"not users");
            }
            var dto = _mapper.Map<List<UserDto>>(user);
            var res_ = dto.ToPagedList(page, pageSize);

            return Ok(new PagedResponse<List<UserDto>>(res_.ToList(), page, pageSize, res_.PageCount, res_.TotalItemCount));
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            int pageSize = 10;
            var roles_ = await _IUserService.GetAllRoles_forAdmin(); 

            if (roles_ == null)
            {
                 return NotFound($"not roles");
            }
         

            return Ok(roles_);
        }

        // Register action
        [HttpPost("RegisterNormalUser")]
        public async Task<ActionResult<CustomUser>> RegisterNormalUser(RegsiterDto model)
        {
            if (ModelState.IsValid)
            {
                var user = new CustomUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.NormalUserRole);

                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return CreatedAtAction(nameof(GetUser), new { email = user.Email }, user);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return ValidationProblem(ModelState);
            }

            return BadRequest(ModelState);
        }


        [HttpPost]
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return BadRequest(ModelState);
            }

            //var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            //var userIdentity = new ClaimsIdentity(claims.ToArray(), "Login");
            //var principal = new ClaimsPrincipal(new[] { userIdentity });

            var role = await _userManager.GetRolesAsync(user);
            var token = _tokenGenerator.CreateToken(user, role.FirstOrDefault());

            #region common area

            var materials = await _repositoryMaterialsService.GetAllAsync();
            var suppliers = await _supplierService.GetAllAsync();
            var buyers = await _BuyerService.GetAllAsync();
            var employees = await _EmployeeService.GetEmployees("");
            var SOFarms = await _supplierService.GetAllSupplierOfFarmsAsync();

            var res_ = new GetAllDto { Materials = materials, Suppliers = suppliers, Buyers = buyers, Employees = employees, SOFarms = SOFarms };

            #endregion

            return Ok(new
            {
                Id = user.Id,
                Email = user.Email,
                Token = token,
                Role = role.FirstOrDefault(),
                GetAllDto= res_
            });
        }


        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Revoke the token by setting another expiration time in the past
            var expiration = DateTime.UtcNow.AddDays(-15);
            var jwt_SH = new JwtSecurityTokenHandler();
            var token = jwt_SH.WriteToken(new JwtSecurityToken(
                expires: expiration
            ));

            return Ok(new { message = "Logged out successfully" });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(model.Email);
                //  var xx = await UserManager.IsEmailConfirmedAsync(user.Id);
                if (user == null)
                {
                    return NotFound("هذا الحساب غير موجود");
                }
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("Addnewpassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Scheme);
                string body_message = _host.WebRootPath + "Message confirm/forgot password/forgotpassword.html";

                // body_message = body_message.Replace("\\Account", "");

                IdentityMessage msg_ = new IdentityMessage()
                {
                    Subject = callbackUrl,
                    Destination = model.Email,
                    Body = body_message
                };

                await _IEmailService.SendAsync(msg_);

                //ViewBag.IfConfirmtext = "تم إرسال رسالة تأكيد إلى البريد الاكتروني\n الرجاء تأكيد الحساب قبل تسجيل الدخول";
                return Ok("تم إرسال رسالة تأكيد إلى البريد الالكتروني");
            }
            return NotFound("حدث خطأ");

        }

        [HttpPost("ForgotPasswordConfirmation")]
        public async Task<ActionResult> ForgotPasswordConfirmation([CustomizeValidator(RuleSet = "ForgotPasswordConfirmation")] SetForgotPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                // ModelState.AddModelError(model.ConfirmPassword, "The new password and confirmation password do not match");
                return NotFound("حدث خطأ");
            }
            var user = await _userManager.FindByIdAsync(model.userId);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, model.code, model.NewPassword);
            
            if (result.Succeeded)
            {
                return Ok("تم تغيير كلمة المرور");
            }
            return NotFound("حدث خطأ");

        }

        [HttpGet("Addnewpassword")]
        public ActionResult Addnewpassword(string userId, string code)
        {
            return Ok();
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (ModelState.IsValid)
            {               
                // Find the user by email
                var user = await _userManager.FindByIdAsync(GetuserId);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                if (user.Email != model.Email)
                {
                    return NotFound("User not found");
                }
                // Change the password
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return Ok("Password changed successfully");
                }

                // If there were errors, add them to ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return ValidationProblem(ModelState);
            }

            return BadRequest(ModelState);
        }
    }
   
}
