using System;
using NUnit.Framework;

namespace TestDrive
{
    public static class TagAssert
    {
        public static void IsVisible(Tag tag)
        {
            try
            {
                Browser.WaitUntilVisible(tag.Selector);
            }
            catch (Exception)
            {
                throw new AssertionException(string.Format("Tag with selector \"{0}\" is not visible.", tag.Selector));
            }
        }

        public static void IsNotVisible(Tag tag)
        {
            try
            {
                Browser.WaitWhileVisible(tag.Selector);
            }
            catch (Exception)
            {
                throw new AssertionException(string.Format("Tag with selector \"{0}\" is visible.", tag.Selector));
            }
        }

        public static void Exists(Tag tag)
        {
            try
            {
                Browser.WaitUntilExists(tag.Selector);
            }
            catch (Exception)
            {
                throw new AssertionException(string.Format("Tag with selector \"{0}\" does not exist.", tag.Selector));
            }
        }

        public static void NotExists(Tag tag)
        {
            try
            {
                Browser.WaitWhileExists(tag.Selector);
            }
            catch (Exception)
            {
                throw new AssertionException(string.Format("Tag with selector \"{0}\" exists.", tag.Selector));
            }
        }

        public static void HasText(Tag tag, string text)
        {
            if (!tag.HasText(text))
                throw new AssertionException(
                    string.Format("Tag with selector \"{0}\" does not have text \"{1}\".", tag.Selector, text));
        }
    }
}
