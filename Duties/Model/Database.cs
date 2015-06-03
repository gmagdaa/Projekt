using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duties
{
    class Database
    {
        private static Database instance;

        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }

        private String connectionString;
        private MySqlConnection connection;

        private Database()
        {
            connectionString = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"];
            OpenConnection();
        }

        ~Database()
        {
            CloseConnection();
        }

        private void OpenConnection()
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Połączenie przerwane: " + ex.Message);
            }
        }

        private void CloseConnection()
        {
            if (connection != null)
            {
                try
                {
                    connection.Close();
                }
                catch
                {
                    /* ignoruj */
                }
            }
        }

        public MySqlConnection Connection
        {
            get
            {
                return connection;
            }
        }

    }
}
