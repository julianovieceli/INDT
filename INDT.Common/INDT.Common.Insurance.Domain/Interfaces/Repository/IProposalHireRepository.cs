namespace INDT.Common.Insurance.Domain.Interfaces.Repository
{
    public interface IProposalHireRepository
    {

        Task<List<ProposalHire>> GetAll();

        //Task<bool> Register(ProposalHire proposalHire);

    }
}
