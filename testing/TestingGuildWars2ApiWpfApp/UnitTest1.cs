using GuildWars2APIWpfApp;

namespace TestingGuildWars2ApiWpfApp
{
    [TestClass]
    public class GuildWars2ApiScrapeTests
    {
        [TestMethod]
        public async Task TestScrapeGuildWars2ApiPage(GuildWars2ApiScrape guildWars2ApiScrape)
        {
            // Arrange
            string url = "https://example.com"; // Provide a valid URL for testing
            string? expectedErrorMessage = null; // Provide the expected error message, if any
            Dictionary<string, (List<char> Keys, string Name)>? expectedApiData = null; // Provide the expected API data

            // Act
            var (apiData, errorMessage) = await guildWars2ApiScrape.ScrapeGuildWars2ApiPage(url);

            // Assert
            Assert.AreEqual(expectedErrorMessage, errorMessage);
            Assert.AreEqual(expectedApiData, apiData);
        }
    }
}