using PatPortal.Domain.Common;
using PatPortal.Domain.ValueObjects;
using PatPortal.SharedKernel.Extensions;

namespace PatPortal.Domain.Entities.Users
{
    public class User : UserBase
    {
        public byte[] Photo { get; private set; }
        public string Profession { get; private set; }

        public User(
            Guid id,
            string firstName, 
            string lastName, 
            Email email, 
            string profession, 
            DateTime dayOfBirht, 
            byte[] photo) : base(id, firstName, lastName, email, dayOfBirht)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Profession = profession;
            DayOfBirht = dayOfBirht;
            Photo = photo;
        }

        public void UpdateLastName(string lastName)
        {
            LastName = lastName.FirstToUpper();
        }

        public void UpdateEmail(Email email)
        {
            Email = email;
        }

        public void UpdateProfesion(string profesion)
        {
            Profession = profesion;
        }

        public void UpdateDayOfBirht(DateTime dayOfBirth)
        {
            DayOfBirht = dayOfBirth;
        }

        public void UpdatePhoto(byte[] photo)
        {
            Photo = photo;
        }
    }
}
