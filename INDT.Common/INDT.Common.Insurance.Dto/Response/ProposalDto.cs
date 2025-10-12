namespace INDT.Common.Insurance.Dto.Response
{
    public class ProposalDto
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public decimal Value { get; set; }

        public string ClientName { get; set; }

        public string InsuranceName { get; set; }

        public int InsuranceId { get; set; }

        public string Status { get; set; }
    }
}
