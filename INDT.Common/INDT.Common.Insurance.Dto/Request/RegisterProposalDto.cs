namespace INDT.Common.Insurance.Dto.Request
{
    public class RegisterProposalDto
    {
        public int InsuranceId { get; set; }

        public int ClientId { get; set; }

        public decimal Value { get; set; }

        public DateTime ExpirationDate{ get; set; }
    }



   
}
