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
            
            if(expirationDate <= proposal.ExpirationDate)
                throw new Exception("Hire Proposal Expiration date must be greather than Proposal exirationDate");

            if (string.IsNullOrWhiteSpace(description))
                throw new Exception("Hire Proposal description required!");


            ExpirationDate = expirationDate;

            this.CreationDate = System.DateTime.Now.ToUniversalTime();

            if (proposal.StatusId != ProposalStatus.Approved)
                throw new Exception("Proposal must be approved to be hired");
        }

    }
}
