using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Services.DTO
{
    public class InputBoxesDto
    {
     
        public int Id { set; get; }

       

        //public int BoFUserId { get; set; }
        public string BoFUserName { get; set; }

        public DateTime Date { get; set; }

        public string Notes { get; set; }

     
        public List<InputBoxesDetailsDto> BoFOpDetails { get; set; }
    }

    public class InputBoxesDetailsDto
    {
        public int Id { set; get; }
        /// <summary>
        /// داخل ممتلئ
        /// داخل فارغ
        /// خارج
        /// </summary>
        public string Direction { set; get; }

        public int Count { set; get; }

        public string ColorType { set; get; }


    }
}
