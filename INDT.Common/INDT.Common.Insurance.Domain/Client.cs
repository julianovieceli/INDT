namespace INDT.Common.Insurance.Domain
{
    public class Client: BaseClass
    {
        private string _docto;

        public string Name { get; set; }

        public string Docto 
        {
            get { return _docto; }
            set
            {
               if(!DocumentValidator.IsCpfCnpjValid(value))
                    throw new Exception("Invalid Document!");

                _docto = value;
            }
        }
        public int Age { get; set; }

        public Client()
        {
        }
        public Client(string name, string docto, int age) 
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
