namespace MapleDev83.Data
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using MySql.Data.MySqlClient;
    
    /// <summary>
    /// Data provider from the MySQL server
    /// </summary>
    public class MySqlDataProvider
    {
        /// <summary>
        /// The MySQL server connection
        /// </summary>
        private MySqlConnection connection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySqlDataProvider"/> class.
        /// </summary>
        /// <param name="mySqlUser">The MySQL server's user's username</param>
        /// <param name="mySqlPassword">The MySQL server's user's password</param>
        /// <param name="mySqlHost">The MySQL server's host</param>
        /// <param name="mySqlDatabase">The MySQL server's database to connect to</param>
        public MySqlDataProvider(string mySqlUser, string mySqlPassword, string mySqlHost, string mySqlDatabase)
        {
            this.connection = new MySqlConnection($"SERVER={mySqlHost}; DATABASE={mySqlDatabase}; UID={mySqlUser}; PASSWORD={mySqlPassword}");
        }

        /// <summary>
        /// Opening the connection to the MySQL server
        /// </summary>
        /// <returns>Return true if the connection successfully opened and false if not</returns>
        public bool Open()
        {
            try
            {
                this.connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"An error occurred trying open the connection to the MySql server:\r\n{e.ToString()}");
                return false;
            }
        }

        /// <summary>
        /// Closing the connection to the MySQL server
        /// </summary>
        /// <returns>Returns true if the connection successfully closed and false if not</returns>
        public bool Close()
        {
            try
            {
                this.connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine($"An error occurred trying close the connection to the MySql server:\r\n{e.ToString()}");
                    return false;
            }
        }

        /// <summary>
        /// Executing an select query from the MySQL server
        /// </summary>
        /// <param name="query">The query to execute</param>
        /// <returns>List of rows as Dictionary to the field and its value</returns>
        public List<dynamic> Select(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            var list = new List<dynamic>();

            while (dataReader.Read())
            {
                var obj = new ExpandoObject();
                var d = obj as IDictionary<string, object>;
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    d[dataReader.GetName(index)] = dataReader.GetString(index);
                }

                list.Add(obj);
            }

            return list;
        }

        public void Update(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.ExecuteNonQuery();
        }

        public void Insert(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.connection);
            cmd.ExecuteNonQuery();
        }

    }
}
