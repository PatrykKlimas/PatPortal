using PatPortal.Domain.Enums;

namespace PatPortal.Application.Services.Interfaces
{
    public interface IDataAccessService
    {
        DataAccess ParseFromString(string dataAccess);
    }
}
