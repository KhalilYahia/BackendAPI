using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Domain.Entities
{
    public class UserLogin : UserLoginKey
    {
        public int Id { get; set; }
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }
    }

    public class UserLoginKey
    {
        public string LoginProvider;
        public string ProviderKey;
    }
}
