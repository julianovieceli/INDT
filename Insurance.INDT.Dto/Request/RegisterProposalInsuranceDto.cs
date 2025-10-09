namespace Insurance.INDT.Dto.Request
{
    public class RegisterProposalInsuranceDto
    {
        public ResisterInsuranceDto Insurance { get; set; }

        public RegisterClientDto Client { get; set; }
    }

    public class ResisterInsuranceDto
    {
        public string Name { get; set; }

        public decimal Value { get; set; }
    }

    public class RegisterClientDto
    {
        public string Name { get; set; }

        public string Docto { get; set; }
        public int Age { get; set; }
    }
}
