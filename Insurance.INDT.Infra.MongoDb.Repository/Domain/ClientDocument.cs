
using Personal.Common.Infra.MongoDb.Repository.Repository;

namespace Insurance.INDT.Infra.MongoDb.Repository.Domain
{
    public class ClientDocument : MongoDbEntityBase
    {
        public string Name { get; set; }

        public string Docto { get; set; }
        public int Age { get; set; }
    }
}
