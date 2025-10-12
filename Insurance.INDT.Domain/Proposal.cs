using Insurance.INDT.Domain.Enums;

namespace Insurance.INDT.Domain
{
    public class Proposal: BaseClass
    {
        public Insurance Insurance { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Client Client { get; set; }

        public ProposalStatus StatusId { get; set; }

        public decimal Value {  get; set; }

        public Proposal()
        {
            
        }
        public Proposal(Insurance insurance, Client client, DateTime expirationDate, decimal value)
        {
            ArgumentNullException.ThrowIfNull(insurance, nameof(insurance));
            ArgumentNullException.ThrowIfNull(client, nameof(client));

            this.Client = client;
            this.Insurance = insurance;

            Value = value;

            if(expirationDate <=  DateTime.UtcNow)
                throw new Exception("Proposal Expiration date must be greather than now");

            ExpirationDate = expirationDate;

            StatusId = ProposalStatus.Analysing;

            this.CreationDate = System.DateTime.Now.ToUniversalTime();
        }

    }
}
