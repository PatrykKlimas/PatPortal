using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.DTOs.Request.Users
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
        public string DayOfBirht { get; set; }
        public byte[]? Photo { get; set; }
    }
}
