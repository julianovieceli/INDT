using Insurance.INDT.Domain.Interfaces.Repository;
using Insurance.INDT.Mysql.Repository.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace Insurance.INDT.Mysql.Repository
{
    public class MySqlDbcontext: IDisposable, IDbContext
    {
        private IDbConnection? _connection;

        private readonly ILogger<MySqlDbcontext> _logger;
        private readonly MySqlDbcontextSettings _mySqlDbcontextSettings;

        public MySqlDbcontext(IOptions<MySqlDbcontextSettings> options, ILogger<MySqlDbcontext> logger)
        {
            _mySqlDbcontextSettings  = options.Value;
            _logger = logger;   
        }

        public IDbConnection? Connect()
        {
            try
            {
                if ((_connection is null) || (_connection.State is not ConnectionState.Open))
                {
                    _connection = new MySqlConnection(_mySqlDbcontextSettings.ConnectionUrl);
                    _connection.Open();
                }
                else
                if (_connection.State is not ConnectionState.Open)
                {
                    _connection.Open();
                }

                _logger.LogInformation("Conneccted");

                return _connection;

            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro ao conectar");
                throw;
            }


        }

        public void Dispose()
        {
            if (_connection is not null)
                _connection.Close();
        }

    }
}
