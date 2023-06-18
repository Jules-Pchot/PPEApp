using System;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Fond_Documentaire
{
    public  class DBConnection
    {

        public DBConnection()
        {
        }
        public DBConnection( string method)
        {
            Server = "13.81.251.33";
            DatabaseName = "julesp_ppe";
            UserName = "developer";
            Password = "cerfal1313";
        }

        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring = string.Format("Server={0}; Port=19812; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }
        public static DBConnection Connection_auto()
        {
            DBConnection connection = new DBConnection();
            /*connection.Server = "127.0.0.1";
            connection.DatabaseName = "fond_doc";
            connection.UserName = "visualstudio";
            connection.Password = "ppevallade";*/
            connection.Server = "13.81.251.33";
            connection.DatabaseName = "julesp_ppe";
            connection.UserName = "developer";
            connection.Password = "cerfal1313";
            return connection;
        }

        
        public void Close()
        {
            Connection.Close();
        }
    }
}
