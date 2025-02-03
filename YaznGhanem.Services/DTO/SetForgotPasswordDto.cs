using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    /// <summary>
    /// SetForgotPasswordDto
    /// </summary>
    public class SetForgotPasswordDto
    {
        public string userId { get; set; }

        public string code { get; set; }


        public string NewPassword { get; set; }


        public string ConfirmPassword { get; set; }

    }
}
