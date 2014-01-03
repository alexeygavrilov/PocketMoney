using System;
using System.Globalization;
using System.Resources;

namespace PocketMoney.Resources
{
    public interface ICurrentCulture
    {
        void ChangeUICulture(CultureInfo culture);
        CultureInfo GetCurrentUICulture();
        string GetString(string resource, string name);
        string GetString(ResourceManager resource, string name);
        string GetString(ResourceManager resource, string name, params string[] args);
    }
}
