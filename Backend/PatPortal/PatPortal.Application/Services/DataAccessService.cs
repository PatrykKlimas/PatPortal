using PatPortal.Application.Services.Interfaces;
using PatPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatPortal.Application.Services
{
    public class DataAccessService : IDataAccessService
    {
        public DataAccess ParseFromString(string dataAccess)
        {
            DataAccess result;
            var ableToConvert = Enum.TryParse<DataAccess>(dataAccess,ignoreCase: true, out result);
            return ableToConvert ? result : DataAccess.Undefined;
        }
    }
}
