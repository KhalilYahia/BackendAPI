using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class GetAllBoF_MainDto
    {
        public GetAllBoF_TotalDto TopOfThePage { get; set; }
        public List<GetBoF_ColorGroupDto> MiddleOfThePage { get; set; }
        public List<GetBoF_UserGroupDto> BottomOfThePage { get; set; }

    }
    public class GetAllBoF_TotalDto
    {
        public int TotalIn { get; set; }

        public int TotalOut { get; set; }

        /// <summary>
        /// عدد الصناديق الحالي
        /// </summary>
        public int Current { get; set; }

    }
    public class GetBoF_ColorGroupDto
    {
        public string Color { get; set; }
        public int TotalIn { get; set; }
        public int TotalOut { get; set; }
        public int Remainder { get; set; }
    }
    public class GetBoF_UserGroupDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public int TotalOut { get; set; }
        public int TotalIn { get; set; }        
        public int Remainder { get; set; }
    }
}
