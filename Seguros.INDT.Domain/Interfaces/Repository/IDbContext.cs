using System.Data;

namespace Insurance.INDT.Domain.Interfaces.Repository
{
    public interface IDbContext
    {
        IDbConnection? Connect();

        void Dispose();
    }
}
