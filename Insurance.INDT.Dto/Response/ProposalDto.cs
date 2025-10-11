namespace Insurance.INDT.Dto.Response
{
    public class ProposalDto
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Value { get; set; }

        public string ClientName { get; set; }

        public string InsuranceName { get; set; }
    }
}
