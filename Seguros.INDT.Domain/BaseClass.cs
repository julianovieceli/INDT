namespace Insurance.INDT.Domain
{
    public abstract class BaseClass
    {
        public string Id { get; set; }

        protected BaseClass(string id)
        {
         ArgumentNullException.ThrowIfNullOrWhiteSpace(id, nameof(id)); 
            this.Id = id;
        }
    }

}
