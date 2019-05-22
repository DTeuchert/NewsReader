using NewsReader.Models;
using NewsReader.Services;
using Xunit;

namespace NewsReader.Tests.Services
{
    public class ConfigurationServiceTest
    {
        [Fact]
        public void GetLanguage()
        {
            // Act
            var language = ConfigurationService.Language;

            // Assert
            Assert.NotNull(language);
            Assert.NotEqual(string.Empty, language);
        }

        [Fact]
        public void GetLinks()
        {
            // Act
            var links = ConfigurationService.Links;

            // Assert
            Assert.NotNull(links);
            Assert.IsType<RssLinkCollection>(links);
        }

        [Fact]
        public void GetVisibleCategories()
        {
            // Act
            var visibleCategories = ConfigurationService.VisibleCategories;

            // Assert
            Assert.NotNull(visibleCategories);
        }
    }
}
