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
            GoogleSearch.SearchButton.Click();

            //Check that search result list is visible
            AssertTag.IsVisible(GoogleResults);

            //Should be more than 1 result
            Assert.That(GoogleResults.Results.Count > 1);

            //Check first search result, should be selenium web site
            var firstResult = GoogleResults.Results.First();
            AssertTag.HasText(firstResult.Title, "Selenium - Web Browser Automation");
            AssertTag.HasText(firstResult.Description, "Selenium automates browsers.");

            //Check second search result, should be Wikipedia
            var secondResult = GoogleResults.Results.Get(1);
            AssertTag.HasText(secondResult.Title, "Wikipedia");
        }

    }
}