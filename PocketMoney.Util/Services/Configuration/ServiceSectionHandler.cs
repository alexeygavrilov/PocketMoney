using System.Configuration;
using System.Xml;

namespace PocketMoney.Util.Services.Configuration
{
    public class ServicesSectionHandler : IConfigurationSectionHandler
    {
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, XmlNode section)
        {
            return new MyConfigurationSection(section);
        }

        #endregion

        #region Nested type: MyConfigurationSection

        private class MyConfigurationSection : ConfigurationSection
        {
            private readonly string _section;

            public MyConfigurationSection(XmlNode section)
            {
                _section = section.OuterXml;
            }

            public override string ToString()
            {
                return _section;
            }
        }

        #endregion
    }
}