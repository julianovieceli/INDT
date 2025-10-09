using Insurance.INDT.Domain.Enums;

namespace Insurance.INDT.Domain
{
    public class Proposal: BaseClass
    {
        public Insurance Insurance { get; set; }

        public DateTime CreationDate { get; set; }

        public Client client { get; set; }

        public InsuranceStatus Status { get; set; }

        public Proposal(string id, Insurance insurance, Client client): base(id)
        {
            ArgumentNullException.ThrowIfNull(insurance, nameof(insurance));
            ArgumentNullException.ThrowIfNull(client, nameof(client));

            this.client = client;
            this.Insurance = insurance;

            Status = InsuranceStatus.Analysing;
        }

    }
}
