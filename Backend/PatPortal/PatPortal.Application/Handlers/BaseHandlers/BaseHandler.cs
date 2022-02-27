using PatPortal.Domain.Exceptions;
using PatPortal.SharedKernel.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.Handlers.BaseHandlers
{
    public abstract class BaseHandler
    {
        protected Guid GetGuidOrThrow(string source, string sourceId)
        {
            var id = sourceId.ParseToGuidOrEmpty();
            if (id == Guid.Empty)
                throw new InitValidationException($"Inncorect {source} id: {sourceId}");

            return id;
        }
    }
}
