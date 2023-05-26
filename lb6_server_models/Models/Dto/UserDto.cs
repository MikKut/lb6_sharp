using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb6_server.Models.Dto
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
