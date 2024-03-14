using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GuildWars2APIWpfApp
{
    /// <summary>
    /// Scrape Guild Wars 2 API html for api info
    /// </summary>
    public class GuildWars2ApiScrape
    {
        // Declare a static HttpClient field
        private static readonly HttpClient httpClient = new();
        private const string pattern = @"/v2/([\w/-]+)\s+\[([\w,]+)]";
        public static class RegexPatterns
        {
            public static string Pattern { get; } = pattern;
        }

        // Define a public static property to access the dictionary
        public static Dictionary<string, (List<char> Keys, string Name)>? ApiData { get; private set; }

        public static async Task<(Dictionary<string, (List<char> Keys, string Name)>? ApiData, string? ErrorMessage)> ScrapeGuildWars2ApiPage(string url)
        {
            Dictionary<string, (List<char> Keys, string Name)>? apiData = null;
            string? errorMessage = null;

            try
            {
                Debug.WriteLine($"The URL is: {url}");
                HttpResponseMessage response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Throw exception if not successful

                string htmlContent = await response.Content.ReadAsStringAsync();

                MatchCollection matches = Regex.Matches(htmlContent, RegexPatterns.Pattern);

                // Initialize the dictionary to store extracted data
                apiData = new Dictionary<string, (List<char> Keys, string Name)>(matches.Count); // Provide an initial capacity

#pragma warning disable U2U1007 // Do not call redundant functions
                foreach (Match match in matches.Cast<Match>())
                {
                    string path = match.Groups[1].Value.Trim();
                    string legends = match.Groups[2].Value.Trim();

                    // Extract the keys from the legend
                    List<char> legendKeys = new(legends.Length); // Initialize with smaller initial capacity
                    foreach (char legend in legends)
                    {
                        if (legend != ',')
                        {
                            legendKeys.Add(legend);
                        }
                    }

                    // Prepend the base URL to the path
                    string fullPath = "https://api.guildwars2.com/" + path;

                    // Check if the path already exists in the dictionary
                    if (!apiData.TryGetValue(fullPath, out (List<char> Keys, string Name) value))
                    {
                        string name = GetEndpointName(path);
                        value = (new List<char>(legends.Length), name); // Initialize list with initial capacity
                                                                        // If the path doesn't exist, create a new list to store keys and names
                        apiData[fullPath] = value;
                    }

                    value.Keys.AddRange(legendKeys);
                }
#pragma warning restore U2U1007 // Do not call redundant functions
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
            // Convert the path from "stories/seasons" to "Stories:Seasons"
            string[] parts = path.Split('/');
            for (int i = 0; i < parts.Length; i++)
            {
                ref string part = ref parts[i];
                part = char.ToUpper(part[0]) + part[1..];
            }
            return string.Join(":", parts);
        }
    }
}
