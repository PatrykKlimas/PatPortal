using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.DTOs.Response.Users
{
    public class UserForViewDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Profession { get; set; }
        public string DayOfBirht { get; set; }
        public byte[] Photo { get; set; }
    }
}
