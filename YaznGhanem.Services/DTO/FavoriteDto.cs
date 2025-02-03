using YaznGhanem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class FavoriteDto
    {
        public int Id { set; get; }


        public DateTime AddingDate { set; get; }


        public string UserId { get; set; }

        public int AdvertismentId { set; get; }
    }
}
