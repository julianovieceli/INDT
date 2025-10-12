using System.Data;

namespace INDT.Common.Insurance.Domain.Interfaces.Repository
{
    public interface IDbContext
    {
        IDbConnection? Connect();

        void Dispose();
    }
}
