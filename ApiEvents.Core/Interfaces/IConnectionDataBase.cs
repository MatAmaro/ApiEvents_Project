using System.Data;

namespace ApiEvents.Core.Interfaces
{
    public interface IConnectionDataBase
    {
        IDbConnection CreateConnection();
    }
}