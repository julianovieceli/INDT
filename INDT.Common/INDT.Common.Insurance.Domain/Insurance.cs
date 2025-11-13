using Personal.Common.Domain;

namespace INDT.Common.Insurance.Domain
{
    public class Insurance: BaseDomain
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }


        public Insurance()
        {
                
        }

        public Insurance(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            

            this.Name = name;

            this.CreationDate = System.DateTime.Now.ToUniversalTime();    
        }
    }
}
