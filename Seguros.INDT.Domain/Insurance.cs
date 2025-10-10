namespace Insurance.INDT.Domain
{
    public class Insurance: BaseClass
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }  

        public decimal Value { get; set; }


        public Insurance(string id, string name, decimal value)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            if (value <= 0)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this.Name = name;
            this.Value = value;
        }
    }
}
