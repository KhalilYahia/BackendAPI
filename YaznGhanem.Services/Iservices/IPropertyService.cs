using YaznGhanem.Services.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.Iservices
{
    public interface IPropertyService
    {
        Task<int> AddNewProperty(PropertyDto dto);

        Task<bool> EditProperty(PropertyDto dto);

        Task<bool> RemoveProperty(int PropertyId);

        Task<List<PropertyDto>> GetAllPropertiesForAdmin();
        Task<List<PropertyDto>> GetAllPropertiesForUser();
    }
}
