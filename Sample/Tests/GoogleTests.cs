using TestDrive;
using NUnit.Framework;

namespace Samples
{
    [TestFixture]
    public class GoogleTests : TestBase
    {
        [Test]
        public void GoogleSearchTest()
        {
            Browser.GoTo("http://www.google.com");

            //Search for selenium
            GoogleSearch.SearchTextBox.Type("selenium test framework");

            //Should be 10 search results
            Assert.That(GoogleSearch.Results.Count == 10);

            //Check first search result, should be selenium web site
            var firstResult = GoogleSearch.Results.First();
            Assert.That(firstResult.Title.HasText("Selenium - Web Browser Automation"));
            Assert.That(firstResult.Description.HasText("Selenium automates browsers."));

            //Check second search result, should be Wikipedia
            var secondResult = GoogleSearch.Results.Get(1);
            Assert.That(secondResult.Title.HasText("Selenium (software) - Wikipedia"));
        }

    }
}