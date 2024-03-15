using NUnit.Framework;
using GuildWars2APIWpfApp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuildWars2APIWpfApp.Tests
{
    [TestFixture]
    public class ScrapeAPIFunctionTest
    {
        [Test]
        [Author("Sythsaz")]
        [Category("APIGeneric")]
        [Order(1)]
        public async Task ScrapeGuildWars2ApiPageTest()
        {
            // Arrange
            const string url = "https://example.com"; // Provide a valid URL for testing
            const string? expectedErrorMessage = null; // Provide the expected error message, if any
            const Dictionary<string, (List<char> Keys, string Name)>? expectedApiData = null; // Provide the expected API data

            // Act
            var (apiData, errorMessage) = await GuildWars2ApiScrape.ScrapeGuildWars2ApiPage(url);

            // Assert
            NUnit.Framework.Assert.AreEqual(expectedErrorMessage, errorMessage);
            NUnit.Framework.Assert.AreEqual(expectedApiData, apiData);

        }
    }
}
