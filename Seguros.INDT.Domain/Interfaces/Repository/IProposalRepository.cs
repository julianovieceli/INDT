namespace Insurance.INDT.Domain.Interfaces.Repository
{
    public interface IProposalRepository
    {
        Task<Domain.Proposal> GetById(int id);

        //Task<int> GetCountByName(string name);

        //Task<bool> Register(Insurance insurance);

        //Task<List<Insurance>> GetAll();
    }
}
