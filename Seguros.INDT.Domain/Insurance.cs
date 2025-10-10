namespace Insurance.INDT.Domain
{
    public class Insurance: BaseClass
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }  

        

        public Insurance(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            

            this.Name = name;

            this.CreationDate = System.DateTime.Now.ToUniversalTime();    
        }
    }
}
