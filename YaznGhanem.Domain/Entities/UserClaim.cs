using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Domain.Entities
{
    public class UserClaim : ClaimBase
    {
        public string UserId { get; set; }
    }
}
