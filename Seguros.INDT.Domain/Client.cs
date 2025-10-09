namespace Insurance.INDT.Domain
{
    public class Client: BaseClass
    {
        public string Name { get; set; }

        public string Docto { get; set; }
        public int Age { get; set; }

        public Client(string id, string name, string docto, int age) : base(id)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(docto, nameof(docto));

            if (age < 18)
            {
                throw new Exception("Age must be greater than 18");
            }

            this.Name = name;
            this.Age = age;
            this.Docto = docto;
        }
    }
}
