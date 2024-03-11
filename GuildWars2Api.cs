using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GuildWars2APIWpfApp
{
    public class GuildWars2ApiScrape
    {
        public static async Task<(Dictionary<string, (List<char> Keys, string Name)>? ApiData, string? ErrorMessage)> ScrapeGuildWars2ApiPage(string url)
        {
            Dictionary<string, (List<char> Keys, string Name)>? apiData = null;
            string? errorMessage = null;

            try
            {
                Debug.WriteLine($"The URL is: {url}");

                using var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throw exception if not successful

                string htmlContent = await response.Content.ReadAsStringAsync();

                // Extract API usage information using regex
                string pattern = @"/v2/([\w/-]+)\s+\[([\w,]+)]";
                MatchCollection matches = Regex.Matches(htmlContent, pattern);

                // Initialize the dictionary to store extracted data
                apiData = new Dictionary<string, (List<char> Keys, string Name)>();

                foreach (Match match in matches.Cast<Match>())
                {
                    string path = match.Groups[1].Value.Trim();
                    string legends = match.Groups[2].Value.Trim();

                    // Extract the keys from the legend
                    List<char> legendKeys = new List<char>();
                    foreach (char legend in legends)
                    {
                        if (legend != ',')
                        {
                            legendKeys.Add(legend);
                        }
                    }

                    string name = GetEndpointName(path);

                    // Prepend the base URL to the path
                    string fullPath = "https://api.guildwars2.com/" + path;

                    // Check if the path already exists in the dictionary
                    if (!apiData.TryGetValue(fullPath, out (List<char> Keys, string Name) value))
                    {
                        value = (new List<char>(), name);
                        // If the path doesn't exist, create a new list to store keys and names
                        apiData[fullPath] = value;
                    }

                    value.Keys.AddRange(legendKeys);
                }
            }
            catch (Exception ex)
            {
                // Print out additional details about the exception
                Debug.WriteLine($"Exception: {ex.Message}");
                Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

                errorMessage = ex.Message;
            }

            // Output debug information to the debug output window
            //Debug.WriteLine("API Data: ");
            if (apiData != null)
            {
                foreach (var kvp in apiData)
                {
                    // Concatenate keys into a single string for display
                    string keysString = string.Join(",", kvp.Value.Keys);

                    // Output the path, concatenated keys, and name
                    Debug.WriteLine($"Path: {kvp.Key}\nKeys: {keysString}\nName: {kvp.Value.Name}");
                }
            }
            else
            {
                Debug.WriteLine("No API Data available.");
            }

            Debug.WriteLine("Error Message: ");
            Debug.WriteLine(errorMessage);

            return (apiData, errorMessage);
        }


        private static string GetEndpointName(string path)
        {
            // Convert the path to the desired format
            // For example, convert "stories/seasons" to "Stories:Seasons"
            string[] parts = path.Split('/');
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = char.ToUpper(parts[i][0]) + parts[i][1..];
            }
            return string.Join(":", parts);
        }
    }
}
