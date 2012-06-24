using TestDrive;

namespace Samples
{
    public class AmazonSearchModel : Tag 
    {
        [Name("field-keywords")]
        public TextBox SearchTextBox;

        public SubmitButton SearchButton;
    }

    [Id("rightResultsATF")]
    public class AmazonResultsModel : Tag
    {
        public H2 Count;

        [Id("result_", MatchType.StartsWith)]
        public TagCollection<Row> Rows;

        public class Row : Tag
        {
            [Selector("h3 a")]
            public Link Title;
        }
    }

    [Selector("#handleBuy")]
    public class AmazonDetailsModel : Tag
    {
        [Id("btAsinTitle")]
        public Tag Title;

        [Id("listPriceValue")]
        public Tag ListPrice;
        public decimal? ListPriceValue 
        {
            get { return decimal.Parse(ListPrice.Text.Trim('$')); }
        }

        [Id("actualPriceValue")]
        public Tag ActualPrice;
        public decimal? ActualPriceValue
        {
            get { return decimal.Parse(ActualPrice.Text.Trim('$')); }
        }

        [Class(".prod_image_selector")]
        public Image MainImage;

        [Selector("#PIAltImagesDiv img")]
        public TagCollection<Image> Thumbs;
    }

    [Class("ap_popover")]
    public class AmazonDetailsPopupModel : Tag
    {
        [Class("ap_closebutton")]
        public Tag CloseButton;
    }
}
