using System;
using System.ComponentModel;
using System.Configuration;
using System.Threading;

namespace Samples
{
    public partial class TestBase
    {
        protected string CreateRandomString()
        {
            return CreateRandomString(10);
        }

        protected string CreateRandomString(int length)
        {
            Thread.Sleep(10);
            var id = "";
            var random = new Random();
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (var i = 0; i < length; i++)
            {
                id += chars[random.Next(0, 35)];
            }
            return id;
        }

        protected static void Wait(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }

        protected static string GetSetting(string setting)
        {
            if (ConfigurationManager.AppSettings[setting] != null)
            {
                var value = ConfigurationManager.AppSettings[setting];
                return value;
            }
            else
            {
                throw new Exception("Application setting '" + setting + "' does not exist.");
            }
        }

        protected static T GetSetting<T>(string setting)
        {
            if (ConfigurationManager.AppSettings[setting] != null)
            {
                var value = ConfigurationManager.AppSettings[setting];
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFrom(value);
            }
            else
            {
                throw new Exception("Application setting '" + setting + "' does not exist.");
            }
        }
    }
}
