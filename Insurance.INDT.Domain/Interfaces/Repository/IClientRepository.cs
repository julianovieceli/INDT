namespace Insurance.INDT.Domain.Interfaces.Repository
{
    public interface IClientRepository
    {
        Task<Client> GetByDocto(string docto);

        Task<int> GetCountByDocto(string docto);

        Task<bool> Register(Client client);

        Task<List<Client>> GetAll();

        Task<Domain.Client> GetById(int id);
    }
}
