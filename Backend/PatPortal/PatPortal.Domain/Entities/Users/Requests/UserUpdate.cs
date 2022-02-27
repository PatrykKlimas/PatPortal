using PatPortal.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Entities.Users.Requests
{
    public class UserUpdate : UserBase
    {
        public byte[] Photo { get; private set; }
        public string Profession { get; set; }

        public UserUpdate(
            Guid id, 
            string firstName, 
            string lastName, 
            Email email, 
            string profession, 
            DateTime dayOfBirht,
            byte[] photo) : 
            base(id, firstName, lastName, email, dayOfBirht)
        {
            Photo = photo;
            Profession = profession;
        }
    }
}
