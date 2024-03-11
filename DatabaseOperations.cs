using System;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;

namespace GuildWars2APIWpfApp
{
    public class DatabaseOperations(string connectionString)
    {
        private readonly string connectionString = connectionString;

        public List<string> GetApiNames()
        {
            List<string> apiNames = new List<string>(); // Initialize the list

            try
            {
                using SqlConnection connection = new(connectionString);
                // Open the connection
                connection.Open();

                string query = "SELECT Name FROM dbo.guildwars2api";
                using SqlCommand command = new(query, connection);
                using SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    apiNames.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");

                // You can return a specific error message here if needed
                apiNames.Add($"Error: {ex.Message}");
            }

            return apiNames;
        }

        public void UpdateApiData(string path, string keys, string name)
        {
            try
            {
                using SqlConnection connection = new(connectionString);
                // Open the connection
                connection.Open();

                // Assuming dbo.guildwars2api is the name of your table
                string query = "INSERT INTO dbo.guildwars2api (Path, Keys, Name) VALUES (@Path, @Keys, @Name)";
                using SqlCommand command = new(query, connection);
                command.Parameters.AddWithValue("@Path", path);
                command.Parameters.AddWithValue("@Keys", keys);
                command.Parameters.AddWithValue("@Name", name);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"An error occurred while updating API data: {ex.Message}");
            }
        }
    }
}
