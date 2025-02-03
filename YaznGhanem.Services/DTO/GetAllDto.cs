using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
    public class GetAllDto
    {
        public List<RepositoryMaterialsDto> Materials { get; set; }
        public List<SupplierDto> Suppliers { get; set; }
        public List<BuyerDto> Buyers { get; set; }
        public List<EmployeeDto> Employees { get; set; }
        public List<SupplierDto> SOFarms { get; set; }
        
    }
}
