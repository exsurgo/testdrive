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

        [Class("fstRow")]
        public Row FirstRow;

        public class Row : Tag
        {
            public H3 Title;
        }
    }

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

        [Selector("PIAltImagesDiv > div > div")]
        public TagCollection<Div> Thumbs;

        [Class("ap_popover")]
        public Tag Popup;

        [Class("ap_closebutton")]
        public Tag PopupCloseButton;
    }
}
