using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class GetAllBoF_OperationsDto
    {
        public string UserName { get; set; }
        public List<GetBoF_ColorGroupDto> TopOfThePage { get; set; }
        public List<GetBoF_AllOperationsDto> BottomOfThePage { get; set; }

    }
 
    public class GetBoF_AllOperationsDto
    {
        public int Id { set; get; }

        public int Count { set; get; }

        /// <summary>
        /// داخل ممتلئ
        /// داخل فارغ
        /// خارج
        /// </summary>
        public string Direction { set; get; }

        public DateTime Date { get; set; }
    }
}
