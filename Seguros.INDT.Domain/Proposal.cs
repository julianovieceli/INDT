using Insurance.INDT.Domain.Enums;

namespace Insurance.INDT.Domain
{
    public class Proposal: BaseClass
    {
        public Insurance Insurance { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Client client { get; set; }

        public InsuranceStatus Status { get; set; }

        public decimal Value {  get; set; }

        public Proposal()
        {
            
        }
        public Proposal(Insurance insurance, Client client, DateTime expirationDate, decimal value)
        {
            ArgumentNullException.ThrowIfNull(insurance, nameof(insurance));
            ArgumentNullException.ThrowIfNull(client, nameof(client));

            this.client = client;
            this.Insurance = insurance;

            Value = value;

            if(expirationDate <=  DateTime.UtcNow)
                throw new Exception("Proposal Expiration date must be greather than now");

            ExpirationDate = expirationDate;

            Status = InsuranceStatus.Analysing;
        }

    }
}
