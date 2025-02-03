using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class BoF_InfoDTO
    {
        public List<BoF_UserDTO> Users { get; set; }        
    }
    public class BoF_UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }

}
