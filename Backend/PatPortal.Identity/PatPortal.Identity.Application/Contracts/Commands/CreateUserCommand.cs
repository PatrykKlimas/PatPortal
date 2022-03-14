using MediatR;
using PatPortal.Identity.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Application.Contracts.Commands
{
    public class CreateUserCommand : IRequest<string>
    {
        public CreateUserCommand(UserForCreationDto userCreate)
        {
            UserCreate = userCreate;
        }

        public UserForCreationDto UserCreate { get; }
    }
}
