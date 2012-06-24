using TestDrive;
using NUnit.Framework;

namespace Samples
{
[TestFixture]
public class HelloWorldTests : TestBase
{
    [Test]
    public void SayHello()
    {
        //Type hello world in textbox
        //Models or auto-bound to UI
        //HelloWorld property is auto-generated
        HelloWorld.SayTextBox.Type("Hello World!");

        //Click save button
        HelloWorld.SayButton.Click();

        //Check for displayed result
        //Timing is handled automatically
        TagAssert.HasText(HelloWorld.Display, "Hello World");
    }

[Test]
public void ListTest()
{
    var count = List.Rows.Count;
    var firstRow = List.Rows.First();
    var secondRow = List.Rows.First();
    TagAssert.IsVisible(firstRow.Title);
    TagAssert.HasText(secondRow.Description, "Hello");
}
}



}
