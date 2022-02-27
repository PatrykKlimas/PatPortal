using FluentValidation;
using FluentValidation.Results;
using PatPortal.Domain.Entities.Users;
using PatPortal.Domain.Entities.Users.Requests;
using PatPortal.Domain.Validators.Factories.Interfaces;
using PatPortal.Domain.Validators.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Domain.Validators.Factories
{
    public class UserValidatorFaactory : IUserValidatorFactory
    {
        private readonly IValidator<UserCreate> _createUserValidator;
        private readonly IValidator<UserUpdate> _updateUserValidator;

        public UserValidatorFaactory(
            IValidator<UserCreate> createUserValidator, 
            IValidator<UserUpdate> updateUserValidator) 
        {
            _createUserValidator = createUserValidator;
            _updateUserValidator = updateUserValidator;
        }
        public async Task<ValidationResult> Validate(UserCreate userCreate)
        {
            return await _createUserValidator.ValidateAsync(userCreate);
        }

        public async Task<ValidationResult> Validate(UserUpdate userUpdate)
        {
            return await _updateUserValidator.ValidateAsync(userUpdate);
        }
    }
}
