using System;
using System.Globalization;
using System.Resources;
using System.Web;
using System.Web.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace PocketMoney.Resources
{
    public static class Culture
    {
        private static ICurrentCulture _culture = null;

        public static CultureInfo CurrentUICulture
        {
            get
            {
                if (_culture == null)
                    _culture = ServiceLocator.Current.GetInstance<ICurrentCulture>();
                return _culture.GetCurrentUICulture();
            }
        }
    }


    public class CurrentCultureProvider : ICurrentCulture
    {
        public static readonly string CurrentCultureKey = "_PocketMoneyCurrentCulture";
        private CultureInfo _currentCulture;

        private ResourceManager _resourceMan;
        private HttpContextBase _context;

        public CurrentCultureProvider()
        {
            try
            {
                _context = ServiceLocator.Current.GetInstance<HttpContextBase>();
            }
            catch (Exception) { }
        }

        public CultureInfo GetCurrentUICulture()
        {
            if (_context != null)
            {
                try
                {
                    var cultureCookie = _context.Request.Cookies[CurrentCultureKey];
                    if (cultureCookie != null)
                        _currentCulture = new CultureInfo(cultureCookie.Value);
                }
                catch (System.Web.HttpException) { }
            }
            if (_currentCulture == null)
            {
                GlobalizationSection section = WebConfigurationManager.GetSection("system.web/globalization") as GlobalizationSection;
                if (section != null && !string.IsNullOrEmpty(section.UICulture))
                    _currentCulture = new CultureInfo(section.UICulture);
                else
                    _currentCulture = new CultureInfo("en-US");
            }
            return _currentCulture;
        }

        public void ChangeUICulture(CultureInfo culture)
        {
            if (_context != null)
            {
                _currentCulture = culture;
                var cultureCookie = _context.Request.Cookies[CurrentCultureKey];
                if (cultureCookie == null)
                {
                    cultureCookie = new HttpCookie(CurrentCultureKey, culture.Name);
                    cultureCookie.Expires = DateTime.Now.AddMonths(1);
                    _context.Response.Cookies.Add(cultureCookie);
                }
                else
                {
                    cultureCookie.Value = culture.Name;
                    cultureCookie.Expires = DateTime.Now.AddMonths(1);
                    _context.Response.Cookies.Set(cultureCookie);
                }
            }
        }

        public string GetString(string resource, string name)
        {
            if (_resourceMan == null || _resourceMan.BaseName != "PocketMoney.Resources." + resource)
                _resourceMan = new ResourceManager("PocketMoney.Resources." + resource, typeof(PocketMoney.Resources.CurrentCultureProvider).Assembly);
            return _resourceMan.GetString(name, this.GetCurrentUICulture());
        }

        public string GetString(ResourceManager resource, string name)
        {
            return resource.GetString(name, this.GetCurrentUICulture());
        }

        public string GetString(ResourceManager resource, string name, params string[] args)
        {
            var culture = this.GetCurrentUICulture();
            return string.Format(culture, resource.GetString(name, culture), args);
        }
    }

    public class CultureProviderWrapper : ICurrentCulture
    {
        private System.Globalization.CultureInfo _culture;
        private ResourceManager _resourceMan;

        public CultureProviderWrapper(System.Globalization.CultureInfo culture)
        {
            _culture = culture;
        }
        
        public void ChangeUICulture(CultureInfo culture)
        {
            _culture = culture;
        }

        public CultureInfo GetCurrentUICulture()
        {
            return _culture;
        }

        public string GetString(string resource, string name)
        {
            if (_resourceMan == null || _resourceMan.BaseName != "PocketMoney.Resources." + resource)
                _resourceMan = new ResourceManager("PocketMoney.Resources." + resource, typeof(PocketMoney.Resources.CurrentCultureProvider).Assembly);
            return _resourceMan.GetString(name, this.GetCurrentUICulture());
        }

        public string GetString(ResourceManager resource, string name)
        {
            return resource.GetString(name, this.GetCurrentUICulture());
        }

        public string GetString(ResourceManager resource, string name, params string[] args)
        {
            var culture = this.GetCurrentUICulture();
            return string.Format(culture, resource.GetString(name, culture), args);
        }
    }
}
