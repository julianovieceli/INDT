using INDT.Common.Insurance.Domain;

namespace INDT.Common.Insurance.Application.Services
{
    public interface ITokenService
    {
        Result GenerateToken(string userName);
    }
}
