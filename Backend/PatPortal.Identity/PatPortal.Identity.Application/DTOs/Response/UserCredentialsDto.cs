using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Application.DTOs.Response
{
    public class UserCredentialsDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
