using TestDrive;
using NUnit.Framework;

namespace Samples
{
    [TestFixture]
    public class AmazonTests : TestBase
    {
        [Test]
        public void FindToaster()
        {
            Browser.GoTo("http://www.amazon.com");

            //Search for toaster
            AmazonSearch.SearchTextBox.Type("Black & Decker TR1256B");
            AmazonSearch.SearchButton.Click();

            //Check for search result count
            AssertTag.IsVisible(AmazonResults.Count);

            //Check first row for model number
            var firstRow = AmazonResults.Rows.First();
            AssertTag.HasText(firstRow.Title, "Black & Decker");

            //View details
            Browser.GoTo(firstRow.Title.Href);
            AssertTag.IsVisible(AmazonDetails);

            //Check prices
            if (AmazonDetails.ActualPrice.IsVisible && AmazonDetails.ListPrice.IsVisible)
            {
                Assert.That(AmazonDetails.ActualPriceValue < AmazonDetails.ListPriceValue);
            }

            //Click on thumb
            AmazonDetails.Thumbs.First().Click();

            //Popup should be visible
            AssertTag.IsVisible(AmazonDetailsPopup);
        }
    }
}