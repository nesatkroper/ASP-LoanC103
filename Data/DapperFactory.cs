using Microsoft.Data.SqlClient;
using System.Data;

namespace ASPLoanMSC103.Data
{
  public sealed class DapperFactory(IConfiguration config)
  {
    private IDbConnection? _connection = null;

    public IDbConnection CreateConnection()
    {
      if(_connection is null)
        _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
      
      return _connection;
    }
  }
}
