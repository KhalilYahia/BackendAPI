using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]        
        public string Email { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "يجب أن يكون طول الاسم بين 3 و30 حرفا")]
        public string DisplayName { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "رقم الهاتف طويل جدا")]
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        [Required]
        public string Roll { get; set; }

        public GetAllDto GetAllDto { get; set; }
        

    }
}
