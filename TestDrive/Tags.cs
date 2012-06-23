using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestDrive
{
    #region Tag

    public class Tag
    {
        #region constructor

        public Tag() : this(null) { }

        public Tag(string selector)
        {
            Selector = selector;
        }

        #endregion

        private string _selector;
        public string Selector
        {
            get 
            {
                return _selector; 
            }
            set
            {
                if (value == null) _selector = null;
                else _selector = value.Trim();

                //Validate the type of tag with selector
                if (_selector != null)
                {
                    var match = GetType().GetCustomAttributes(typeof(TagDefinitionAttribute), true);
                    if (match.Any())
                    {
                        if (Browser.Exists(_selector) && !Browser.IsMatch(_selector, ((TagDefinitionAttribute)match.Single()).Selector))
                            throw new Exception("Incorrect html tag is mapped.");
                    }
                }
            }
        }

        public string TagName
        {
            get 
            {
                OnBeforeAccessingBrowser();
                var val = Browser.GetTagName(Selector);
                OnAfterAccessingBrowser();
                return val;
            }
        }

        public string Id
        {
            get 
            {
                OnBeforeAccessingBrowser();
                var val = Browser.GetAttribute(Selector, "id");
                OnAfterAccessingBrowser();
                return val;
            }
        }

        public string Class
        {
            get 
            {
                OnBeforeAccessingBrowser();
                var val = Browser.GetAttribute(Selector, "class");
                OnAfterAccessingBrowser();
                return val;
            }
        }

        public string Text
        {
            get 
            {
                OnBeforeAccessingBrowser();
                var val = Browser.GetText(Selector);
                OnAfterAccessingBrowser();
                return val;
            }
        }

        public bool Exists
        {
            get 
            {
                OnBeforeAccessingBrowser();
                var val = Browser.Exists(Selector);
                OnAfterAccessingBrowser();
                return val;
            }
        }

        public bool IsVisible
        {
            get
            {
                OnBeforeAccessingBrowser();
                var val = Browser.IsVisible(Selector);
                OnAfterAccessingBrowser();
                return val;
            }
        }

        public bool HasText(string text)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(Text)) return false;
            var regex = new Regex("(?i)" + text);
            return regex.IsMatch(Text);
        }

        public void Click()
        {
            OnBeforeAccessingBrowser();
            Browser.Click(Selector);
            OnAfterAccessingBrowser();
        }

        public void Type(object input)
        {
            Type(input.ToString());
        }

        public void Type(string input)
        {
            OnBeforeAccessingBrowser();
            Browser.Type(Selector, input);
            OnAfterAccessingBrowser();
        }

        public void MouseOver()
        {
            OnBeforeAccessingBrowser();
            Browser.MouseOver(Selector);
            OnAfterAccessingBrowser();
        }

        public void MouseOut()
        {
            OnBeforeAccessingBrowser();
            Browser.MouseOut(Selector);
            OnAfterAccessingBrowser();
        }

        public string GetAttribute(string attrName)
        {
            OnBeforeAccessingBrowser();
            var val = Browser.GetAttribute(Selector, attrName);
            OnAfterAccessingBrowser();
            return val;
        }

        public void SetAttribute(string attrName)
        {
            OnBeforeAccessingBrowser();
            Browser.SetAttribute(Selector, attrName);
            OnAfterAccessingBrowser();
        }

        public void RemoveAttribute(string attrName)
        {
            OnBeforeAccessingBrowser();
            Browser.RemoveAttribute(Selector, attrName);
            OnAfterAccessingBrowser();
        }

        #region Change Events

        private string _originalSelector;

        private void OnBeforeAccessingBrowser()
        {
            //Email selector
            var matches = new Regex(@"^:email\(.+\)").Matches(Selector);
            if (matches.Count > 0)
            {
                _originalSelector = Selector;
                Selector = Regex.Replace(Selector, @"^:email\(.+\)", "body ");
                var val = matches[0].Value;
                val = val.Substring(7, val.Count() - 8);
                Browser.OpenEmail(val);
            }
        }

        private void OnAfterAccessingBrowser()
        {
            //Email selector
            if (!string.IsNullOrEmpty(_originalSelector) && 
                new Regex(@"^:email\(.+\)").Matches(_originalSelector).Count > 0)
            {
                Browser.CloseEmail();
            }
        }

        #endregion

        #region Helpers

        protected static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }

            return sb.ToString();
        }

        #endregion
    }

    public class TagCollection<T> where T : Tag, new()
    {
        public string Selector { get; set; }

        public TagCollection()
        {

        }

        public TagCollection(string selector)
        {
            Selector = selector;
        }

        public int Count
        {
            get { return Browser.Count(Selector); }
        }

        public T First()
        {
            return Browser.GetTag<T>(Selector + ":first");
        }

        public T Last()
        {
            return Browser.GetTag<T>(Selector + ":last");
        }

        public T Get(int index)
        {
            return Browser.GetTag<T>(Selector + ":eq(" + index + ")");
        }

        //Getting all elements is too slow
        #region IEnumerable

        //IEnumerator<T> IEnumerable<T>.GetEnumerator()
        //{
        //    return GetList().GetEnumerator();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return GetList().GetEnumerator();
        //}

        //private List<T> GetList()
        //{
        //    if (string.IsNullOrEmpty(Selector)) throw new Exception("ElementCollection must have a selector.");
        //    return Browser.GetElements<T>(Selector);
        //}

        #endregion
    }

    #endregion

    #region HTML-Specific Tags

    [TagDefinition("b")]
    public class B : Tag
    {

    }

    [TagDefinition("body")]
    public class Body : Tag
    {

    }

    [TagDefinition("input[type=button], input[type=submit], input[type=reset], button")]
    public class Button : Tag
    {

    }

    [Input]
    [TagDefinition("input[type=checkbox]:first")]
    public class CheckBox : Tag
    {
        public bool IsChecked
        {
            get
            {
                var val = Browser.GetAttribute(Selector, "checked");
                return (string.Equals(val, "1", StringComparison.CurrentCultureIgnoreCase) ||
                        string.Equals(val, "checked", StringComparison.CurrentCultureIgnoreCase) ||
                        string.Equals(val, "true", StringComparison.CurrentCultureIgnoreCase)
                        );
            }
        }
    }

    [TagDefinition("div")]
    public class Div : Tag
    {

    }

    [Input]
    [TagDefinition("select:not([multiple])")]
    public class DropDown : Tag
    {
        public bool IsInvalid
        {
            get
            {
                return Class != null && Class.Split(' ').Contains("input-validation-error");
            }
        }

        public bool IsSelected
        {
            get { return !string.IsNullOrEmpty(SelectedValue); }
        }

        public string SelectedValue
        {
            get
            {
                var val = Browser.ExecuteScript("return $('" + Selector + "').val();");
                if (val != null) return (string) val;
                else return null;
            }
            set { SelectValue(value); }
        }

        public string SelectedText
        {
            get
            {
                var val = Browser.ExecuteScript("return $(\"" + Selector + " > option:selected\").text();");
                if (val != null) return (string)val;
                else return null;
            }
        }

        public void SelectValue(object val)
        {
            SelectValue(val.ToString());
        }

        public void SelectValue(string val)
        {
            var selector = string.Format("{0} > option[value='{1}']", Selector, EncodeJsString(val));
            if (Browser.Count(selector) == 0) throw new Exception("DropDown does not contain value \"" + val + "\".");
            var script = string.Format("$(\"{0}\").val(\"{1}\")", Selector, val);
            Browser.ExecuteScript(script);
        }

        public void SelectText(string text)
        {
            //Clear selected
            var selector = string.Format("{0} > option", Selector);
            var script = string.Format("$(\"{0}\").removeAttr(\"selected\");", selector);
            Browser.ExecuteScript(script);

            //Select option
            selector = string.Format("{0} > option:contains('{1}')", Selector, EncodeJsString(text));
            if (Browser.Count(selector) == 0) throw new Exception("DropDown does not contain option with text \"" + text + "\".");
            script = string.Format("$(\"{0}\").attr(\"selected\", true);", selector);
            Browser.ExecuteScript(script);
        }

        public TagCollection<Option> Options { get; set; } 
    }

    [TagDefinition("em")]
    public class EM : Tag
    {

    }

    [TagDefinition("del")]
    public class Del : Tag
    {

    }

    [TagDefinition("form")]
    public class Form : Tag
    {

    }

    [TagDefinition("h1")]
    public class H1 : Tag
    {

    }

    [TagDefinition("h2")]
    public class H2 : Tag
    {

    }

    [TagDefinition("h3")]
    public class H3 : Tag
    {

    }

    [TagDefinition("h4")]
    public class H4 : Tag
    {

    }

    [TagDefinition("h5")]
    public class H5 : Tag
    {

    }

    [TagDefinition("h6")]
    public class H6 : Tag
    {

    }

    [TagDefinition("img")]
    public class Image : Tag
    {

    }

    [TagDefinition("a")]
    public class Link : Tag
    {
        public string Href
        {
            get { return Browser.GetAttribute(Selector, "href"); }
        }
    }

    [TagDefinition("li")]
    public class ListItem : Tag
    {

    }

    [TagDefinition("option")]
    public class Option : Tag
    {
        public bool IsSelected
        {
            get
            {
                return Browser.IsMatch(Selector, ":selected");
            }
        }
    }

    [TagDefinition("p")]
    public class Paragraph : Tag
    {

    }

    [TagDefinition("input[type=radio]")]
    public class Radio : Tag
    {

    }

    [TagDefinition("span")]
    public class Span : Tag
    {

    }

    [TagDefinition("input[type=submit]")]
    public class SubmitButton : Button
    {

    }

    [Input]
    [TagDefinition("input[type=text], input[type=password], textarea")]
    public class TextBox : Tag
    {
        public new string Text
        {
            get
            {
                var text = Browser.GetAttribute(Selector, "value");
                if (string.IsNullOrEmpty(text)) return null;
                else return text;
            }
        }

        public bool IsInvalid
        {
            get
            {
                return Class != null && Class.Split(' ').Contains("input-validation-error");
            }
        }
    }

    [TagDefinition("table")]
    public class Table : Tag
    {

    }

    [TagDefinition("tr")]
    public class TableRow : Tag
    {

    }

    [TagDefinition("ul")]
    public class UnorderedList : Tag
    {
        [Tag("li")]
        public TagCollection<ListItem> CollectionItems;
    }

    #endregion
}

