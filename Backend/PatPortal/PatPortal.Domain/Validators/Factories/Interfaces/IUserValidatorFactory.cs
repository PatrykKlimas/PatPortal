using FluentValidation.Results;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Entities.Users.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Validators.Factories.Interfaces
{
    public interface IUserValidatorFactory
    {
        Task<ValidationResult> Validate(UserCreate userCreate);
        Task<ValidationResult> Validate(UserUpdate userUpdate);
    }
}
