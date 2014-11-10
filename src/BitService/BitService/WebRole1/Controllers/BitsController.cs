
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using WebRole1.Models;
using WebRole1.Filters;
using System.Data.SqlClient;
using System.Configuration;

namespace WebRole1.Controllers
{
    [System.Web.Mvc.Authorize]
    [ValidateHttpAntiForgeryToken]
    public class BitsController : ApiController
    {

        private string GetToken()
        {
            IEnumerable<string> values;
            bool foundToken = this.Request.Headers.TryGetValues("AccessKey", out values);
            if (!foundToken)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }        
            string token = values.First<string>();

            // If the id string has quotes around it, strip them off.
            if (token.StartsWith("\""))
            {
                token = token.Substring(1, token.Length - 2);
            }

            return token;
        }
 
        // DELETE api/bits/{name}
        [HttpDelete]
        public bool Delete(string name)
        {
            string token = GetToken();

            // The bit is no longer needed, so delete it from the database.
            // Use the provided token to identify the row to delete in the database.
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                try
                {
                    connection.Open();

                    // TODO protect against SQL Command spoofing.
                    SqlCommand command = new SqlCommand("SELECT * FROM NamedBits WHERE Name = '" + name + "' AND Guid = '" + token + "'", connection);
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {

                        reader.Close();

                        string commandText = String.Format("DELETE FROM NamedBits WHERE Guid = '{0}' ", token);
                        SqlCommand updateCommand = new SqlCommand(commandText, connection);

                        updateCommand.ExecuteNonQuery();
                        return true;


                    }
                    else
                    {
                        reader.Close();
                        throw new Exception("No resource with given name and token found.");
                    }
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        // POST api/bits/{name}
        public string Post(string name)
        {
            // User must provide a unique name which acts as a key.
            // We need to insert a record into a table which lists their unique name, the key, and the Boolean value (initially false)
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {

                connection.Open();

                // TODO protect against SQL Command spoofing.
                SqlCommand command = new SqlCommand("SELECT * FROM NamedBits WHERE Name = '" + name + "'", connection);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Close();
                    string errorMessage = String.Format("The name {0} is already in use. Please choose another name to provision a new bit.", name);
                    throw new InvalidOperationException(errorMessage); // consider HttpResponseException
                }
                else
                {
                    reader.Close();
                    System.Guid guid = System.Guid.NewGuid();
                    // Get the Id of the authenticated user.
                    string owner = this.User.Identity.Name;
                    SqlCommand findUser = new SqlCommand("SELECT Id FROM AspNetUsers WHERE UserName = '" + owner + "'", connection);
                    string userId = (string) findUser.ExecuteScalar();
                        
                    string commandText = String.Format("INSERT INTO NamedBits (Name, Owner, Guid, Bit) VALUES('{0}','{1}','{2}', 0)", name, userId, guid.ToString());
                    SqlCommand insertCommand = new SqlCommand(commandText);
                    insertCommand.Connection = connection;
                    int result = insertCommand.ExecuteNonQuery();
                    if (result != 1)
                    {
                        return "";
                    }
                    else
                        return guid.ToString();
                }
                
            }
        }
        

        // PUT api/bits/{name}?value={true|false}
        public bool Put(string name, bool value)
        {
            string token = GetToken();

            // Use the provided token to identify the value to update in the database.
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {

                connection.Open();

                // TODO protect against SQL Command spoofing.
                SqlCommand command = new SqlCommand("SELECT * FROM NamedBits WHERE Name = '" + name + "' AND Guid = '" + token + "'", connection);
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    reader.Read();
                    object[] values = new object[5];
                    reader.GetValues(values);
                    bool bit = (bool) values[4];
                    reader.Close();
                    if (bit == value)
                    {
                        return bit;
                    }
                    else
                    {
                        reader.Close();
                        int valueAsInt = value ? 1 : 0;
                        string commandText = String.Format("UPDATE NamedBits SET Bit = {0} WHERE Guid = '{1}' ", valueAsInt, token);
                        SqlCommand updateCommand = new SqlCommand(commandText, connection);

                        updateCommand.ExecuteNonQuery();
                        return true;
                    }
                        
                }
                else
                {
                    reader.Close();
                    throw new Exception("No resource with given name and token found.");
                }

            }

        }

        // GET api/bits/{name}
        public bool Get(string name)
        {
            string token = GetToken();

   

            // Now, use the provided token to look up the value in the database.
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                try
                {
                    connection.Open();

                    // TODO protect against SQL Command spoofing.
                    SqlCommand command = new SqlCommand("SELECT * FROM NamedBits WHERE Name = '" + name + "' AND Guid = '" + token + "'", connection);
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {
                        reader.Read();
                        object[] values = new object[5];
                        reader.GetValues(values);
                        bool bit = (bool)values[4];
                        reader.Close();
                        return bit;

                    }
                    else
                    {
                        reader.Close();
                        throw new Exception("No resource with given name and token found.");
                    }
                }
                catch (Exception exception)
                {
                    return false;
                }
            }
        }


        // GET api/bits
        public IEnumerable<string> GetServices()
        {
            // Query the database for all services belonging to the currently logged in user.

            // Step 1: Get the user.

            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()))
            {
                try
                {
                    connection.Open();
                    // Get the Id of the authenticated user.
                    string owner = this.User.Identity.Name;
                    // TODO protect against SQL Command spoofing.
                    SqlCommand command = new SqlCommand("SELECT * FROM NamedBits JOIN AspNetUsers ON Tokens.Owner = AspNetUsers.Id WHERE AspNetUsers.UserName = '" + owner + "'", connection);
                    SqlDataReader reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            names.Add((string)reader.GetValue(1));
                        }

                    }

                    reader.Close();
                }
                catch (Exception e)
                {
                    return names;
                }
                return names;
            }
        }
    }
}
