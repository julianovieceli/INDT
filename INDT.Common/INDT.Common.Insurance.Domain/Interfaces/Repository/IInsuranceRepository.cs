namespace INDT.Common.Insurance.Domain.Interfaces.Repository
{
    public interface IInsuranceRepository
    {
        Task<Insurance> GetByName(string name);

        Task<Domain.Insurance> GetById(int id);
        Task<int> GetCountByName(string name);

        Task<bool> Register(Insurance insurance);

        Task<List<Insurance>> GetAll();
    }
}
