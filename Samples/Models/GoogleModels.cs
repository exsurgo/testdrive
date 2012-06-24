using TestDrive;

namespace Samples
{
    public class GoogleSearchModel : Tag  //models inherit from tag, body by default
    {
        [Name("q")]
        public TextBox SearchTextBox;  //input field with name="q"

        [Name("btnG")]
        public Tag SearchButton;   //first submit button
    }

    [Selector("#search")]
    public class GoogleResultsModel : Tag
    {
        [Tag("li")]
        public TagCollection<Result> Results; //all list items in search div

        public class Result : Tag  //Sub-model to represent nested tags
        {
            [Selector("a:first")]
            public Link Title;  //first link

            [Class("st")]
            public Tag Description; //tag with class "st"
        }
    }
}
