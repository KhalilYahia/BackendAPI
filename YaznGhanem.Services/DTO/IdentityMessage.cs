using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaznGhanem.Services.DTO
{
//
// Summary:
//     Represents a message
public class IdentityMessage
    {
        //
        // Summary:
        //     Destination, i.e. To email, or SMS phone number
        public string Destination { get; set; }

        //
        // Summary:
        //     Subject
        public string Subject { get; set; }

        //
        // Summary:
        //     Message contents
        public string Body { get; set; }
    }
}
