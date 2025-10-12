using INDT.Common.Insurance.Domain.Enums;

namespace INDT.Common.Insurance.Domain
{
    public class ProposalHire : BaseClass
    {
        public Proposal Proposal { get; set; }

        public string Description { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        
        public ProposalHire()
        {
            
        }
        public ProposalHire(Proposal proposal, DateTime expirationDate, string description)
        {
            ArgumentNullException.ThrowIfNull(proposal, nameof(proposal));
            
            this.Proposal = proposal;
            
            this.Description = description;
            if (string.IsNullOrWhiteSpace(description))
                throw new Exception("Hire Proposal description required!");


            ExpirationDate = expirationDate;

            this.CreationDate = System.DateTime.Now.ToUniversalTime();

            if (proposal.StatusId != ProposalStatus.Approved)
                throw new Exception("Proposal must be approved to be hired");
        }

    }
}
