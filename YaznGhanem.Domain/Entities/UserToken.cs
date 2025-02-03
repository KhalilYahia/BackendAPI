using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstProject.Domain.Entities
{
    public class UserToken : UserTokenKey
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class UserTokenKey
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
    }
}
