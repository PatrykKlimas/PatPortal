using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Identity.Domain.Exceptions
{
    internal class UnauthorizedException : Exception
    {
        public UnauthorizedException()
        {
        }

        public UnauthorizedException(string? message) 
            : base(message)
        {
        }

        protected UnauthorizedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
