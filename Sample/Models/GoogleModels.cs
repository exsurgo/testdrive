using TestDrive;

namespace Samples
{
    public class GoogleSearchModel : Tag  //models inherit from tag, body by default
    {
        [Name("q")]
        public TextBox SearchTextBox;  //input field with name="q"

        public SubmitButton SignInButton;   //first submit button

        [Selector("#search li")]
        public TagCollection<GooleResult> Results; //all list items in search div

        public class GooleResult : Tag  //Sub-model to represent nested tags
        {
            public Link Title;  //first link

            [Class("st")]
            public Tag Description; //tag with class "st"
        }    
    }
}
