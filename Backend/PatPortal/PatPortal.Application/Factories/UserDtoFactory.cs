using PatPortal.Application.DTOs.Request.Users;
using PatPortal.Application.Factories.Interfaces;
using PatPortal.Domain.Entities.Users.Requests;
using PatPortal.Domain.ValueObjects;
using PatPortal.SharedKernel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.Factories
{
    internal class UserDtoFactory : IUserDtoFactory
    {
        public UserUpdate Create(Guid id, UserForUpdateDto userForUpdate)
        {
            return new UserUpdate(
                id, 
                userForUpdate.FirstName, 
                userForUpdate.LastName, 
                new Email(userForUpdate.Email),
                userForUpdate.Profession,
                userForUpdate.DayOfBirht.ParseToDateTime(), 
                userForUpdate.Photo ?? new byte[] { });
        }
    }
}
