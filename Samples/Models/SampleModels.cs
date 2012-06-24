using TestDrive;
using NUnit.Framework;

namespace Samples
{
public class HelloWorldModel : Tag //Models inherit from Tag
{
    public TextBox SayTextBox; //input with name="Say"

    public SubmitButton SayButton; //first submit button

    [Selector("#display")]
    public Tag Display; //div with id="display"
}


[Id("list")]
public class ListModel : Tag
{
    public TagCollection<Row> Rows;

    public class Row : Tag
    {
        [Class("title")]
        public Tag Title;

        [Class("description")]
        public Tag Description;
    }
}
}
