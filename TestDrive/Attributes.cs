using System;

namespace TestDrive
{
    #region Tags

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TagDefinitionAttribute : Attribute
    {
        public string Selector { get; private set; }

        public TagDefinitionAttribute(string selector)
        {
            Selector = selector;
        }
    }

    #endregion

    #region Selectors

    public enum MatchType
    {
        Equals,
        StartsWith,
        Contains,
        EndsWith
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SelectorAttribute : Attribute
    {
        public string Selector { get; set; }

        public SelectorAttribute()
        {

        }

        public SelectorAttribute(string value)
        {
            Selector = value.Trim();
        }

        #region Helpers

        protected string GetMatchTypeExpression(MatchType type)
        {
            switch (type)
            {
                case MatchType.Equals:
                    return "=";
                case MatchType.StartsWith:
                    return "^=";
                case MatchType.Contains:
                    return "*=";
                case MatchType.EndsWith:
                    return "$=";
                default:
                    return "=";
            }
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class NameAttribute : SelectorAttribute
    {
        public string Name { get; private set; }

        public MatchType MatchType { get; private set; }

        public NameAttribute(string value) : this(value, MatchType.Equals)
        {
        }

        public NameAttribute(string value, MatchType type) : base(value)
        {
            Name = value;
            MatchType = type;
            Selector = string.Format("[name{0}'{1}']", GetMatchTypeExpression(type), value);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class IdAttribute : SelectorAttribute
    {
        public string Id { get; private set; }

        public MatchType MatchType { get; private set; }

        public IdAttribute(string value) : this(value, MatchType.Equals)
        {
 
        }

        public IdAttribute(string value, MatchType type) : base(value)
        {
            Id = value;
            MatchType = type;
            if (type == MatchType.Equals) Selector = "#" + value;
            else Selector = string.Format("[id{0}'{1}']", GetMatchTypeExpression(type), value);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TextAttribute : SelectorAttribute
    {
        public string Text { get; private set; }

        public TextAttribute(string value) : base(value)
        {
            Text = string.Format(":contains('{0}')", value);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class TagAttribute : SelectorAttribute
    {
        public string Tag { get; protected set; }

        public TagAttribute(string value) : base(value)
        {
            Tag = value;
            Selector = value;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ClassAttribute : SelectorAttribute
    {
        public string Class { get; protected set; }

        public ClassAttribute(string value)
            : base(value)
        {
            Class = value;
            Selector = "." + value;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class SubmitAttribute : SelectorAttribute
    {
        public SubmitAttribute()
        {
            Selector = ":submit";
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ButtonAttribute : SelectorAttribute
    {
        public ButtonAttribute()
        {
            Selector = ":button";
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class FormAttribute : SelectorAttribute
    {
        public FormAttribute()
        {
            Selector = "form";
        }
    }

    #endregion

    #region Model Types

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class InputAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EmailAttribute : SelectorAttribute
    {
        public string Subject { get; private set; }

        public EmailAttribute(string subject)
        {
            Subject = subject;
            Selector = ":email(" + subject + ")";
        }
    }

    #endregion
}
