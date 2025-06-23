using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;

namespace queryrunner.Services
{
    public static class OracleService
    {
        public static OracleConnection Connect()
        {
            // Ambil dari Properties.Settings
            string userId = Properties.Settings.Default.OracleUserId;
            string password = Properties.Settings.Default.OraclePassword;
            string host = Properties.Settings.Default.OracleHost;
            string port = Properties.Settings.Default.OraclePort;
            string serviceName = Properties.Settings.Default.OracleService;

            string connectionString =
           $"User Id={userId};Password={password};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={serviceName})))";

            var connection = new OracleConnection(connectionString);


            //var connection = new OracleConnection(connectionString.Trim());

            try
            {
                connection.Open(); // 👈 jangan lupa open
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Gagal koneksi ke database Oracle:\n" + ex.Message);
            }
        }

        public static int ExecuteScalarCount(string query)
        {
            using (var conn = Connect())
            using (var cmd = new OracleCommand($"SELECT COUNT(*) FROM ({query})", conn))
            {
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }
    }
}
