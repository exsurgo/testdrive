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
            Assert.That(AmazonResults.Count.IsVisible);

            //Check first row for model number
            Assert.That(AmazonResults.FirstRow.Title.HasText("TR1256B"));

            //View details
            AmazonResults.FirstRow.Title.Click();

            //Check prices
            if (AmazonDetails.ActualPrice.IsVisible && AmazonDetails.ListPrice.IsVisible)
            {
                Assert.That(AmazonDetails.ActualPriceValue < AmazonDetails.ListPriceValue);
            }



        }
    }
}