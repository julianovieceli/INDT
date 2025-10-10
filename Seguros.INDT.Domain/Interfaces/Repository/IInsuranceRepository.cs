using System.ComponentModel.Design.Serialization;

namespace Insurance.INDT.Domain.Interfaces.Repository
{
    public interface IInsuranceRepository
    {
        Task<Insurance> GetByName(string name);

        Task<int> GetCountByName(string name);

        Task<bool> Register(Insurance insurance);

        Task<List<Insurance>> GetAll();
    }
}
