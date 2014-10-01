using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;

namespace SoftMachine.Utilities
{
    public static class Connection
    {

       static string server;
       static string port;
       static string userId;
       static string password;
       static string database;

       public static MySqlConnection thisConnection;

       public static MySqlConnection SetConnection(string _server, string _port, string _userId, string _password, string _database)
       {
           server = _server;
           port = _port;
           userId = _userId;
           password = _password;
           database = _database;
           return GetConnection();
       }
       public static MySqlConnection GetConnection()
        {
            string charset = "utf8";
            string connectionString = string.Format("server={0}; port={1}; uid={2}; pwd={3}; DataBase={4}; charset={5};", server, port, userId, password, database, charset);
            thisConnection = new MySqlConnection(connectionString);
            return thisConnection;
        }
        public static void QueryDB(DataTable oDataTable, string query, string database)
        {
            //if (database == null) thisConnection = GetConnection();
            //try             {
            if (thisConnection.State == ConnectionState.Closed)
            {
                thisConnection.Open();
            }
            MySqlCommand cmd = new MySqlCommand(query, thisConnection);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            adapter.Fill(oDataTable);
            /*
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
            }
            finally
            {
                thisConnection.Close();
            }//*/
            thisConnection.Close();
            //thisConnection = null;
        }

    }
}
