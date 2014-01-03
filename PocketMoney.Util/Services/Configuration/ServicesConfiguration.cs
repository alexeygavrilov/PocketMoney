using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Xml;
using PocketMoney.Util.ExtensionMethods;

namespace PocketMoney.Util.Services.Configuration
{
    public sealed class ServicesConfiguration : IServicesConfiguration
    {
        private readonly IDictionary<string, IServiceConfiguration> services =
            new Dictionary<string, IServiceConfiguration>(new CaseInsensetiveInvariantCultureComparer());

        private readonly IDictionary<string, string> uniqieServiceDisplayNames =
            new Dictionary<string, string>(new CaseInsensetiveInvariantCultureComparer());

        private ServicesConfiguration(ConfigurationSection section)
        {
            string configSectionXML = section.ToString();
            var document = new XmlDocument();
            document.LoadXml(configSectionXML);
            LoadConfiguration(document);
        }

        public static IServicesConfiguration Current
        {
            get
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
                var section = (ConfigurationSection) ConfigurationManager.GetSection("services");
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
                return new ServicesConfiguration(section);
            }
        }

        #region IServicesConfiguration Members

        public IDictionary<string, IServiceConfiguration> Services
        {
            get { return services; }
        }

        #endregion

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return typeof (ServicesConfiguration).Assembly;
        }

        private void LoadConfiguration(XmlDocument document)
        {
            XmlNodeList serviceNodes = document.GetElementsByTagName("service");
            foreach (XmlNode serviceNode in serviceNodes)
            {
                var serviceConfiguration = new ServiceConfiguration(serviceNode);
                uniqieServiceDisplayNames.Add(serviceConfiguration.DisplayName, serviceConfiguration.DisplayName);
                services.Add(serviceConfiguration.ServiceName, serviceConfiguration);
            }
        }

        #region Nested type: CaseInsensetiveInvariantCultureComparer

        private class CaseInsensetiveInvariantCultureComparer : IEqualityComparer<string>
        {
            #region IEqualityComparer<string> Members

            public bool Equals(string x, string y)
            {
                return x.ToLowerInvariant().Equals(y.ToLowerInvariant());
            }

            public int GetHashCode(string obj)
            {
                return obj.ToLowerInvariant().GetHashCode();
            }

            #endregion
        }

        #endregion

        #region Nested type: ServiceConfiguration

        private class ServiceConfiguration : IServiceConfiguration
        {
            private readonly string description, displayName, serviceName, typeName;
            private readonly IDictionary<string, string> values = new Dictionary<string, string>();


            public ServiceConfiguration(XmlNode serviceNode)
            {
                description = serviceNode.Attributes["description"].Value;
                displayName = serviceNode.Attributes["displayName"].Value;
                serviceName = serviceNode.Attributes["serviceName"].Value;
                typeName = serviceNode.Attributes["type"].Value;
                foreach (XmlNode setting in serviceNode.ChildNodes)
                {
                    switch (setting.Name)
                    {
                        case "add":
                            {
                                values.Add(setting.Attributes["name"].Value, setting.Attributes["value"].Value);
                                break;
                            }
                        case "clear":
                            {
                                values.Clear();
                                break;
                            }
                    }
                }
            }

            #region IServiceConfiguration Members

            public string Description
            {
                get { return description; }
            }

            public string DisplayName
            {
                get { return displayName; }
            }

            public string ServiceName
            {
                get { return serviceName; }
            }

            public string TypeName
            {
                get { return typeName; }
            }

            public Type GetServiceType()
            {
                Type type = Type.GetType(typeName, true, true);
                if (type == null)
                    throw new ArgumentException(
                        String.Format("The type {0} cant be loaded.", typeName)).LogError();
                if (!type.IsSubclassOf(typeof (Service)) && (!typeof (Service).Equals(type)))
                    throw new ArgumentException(
                        String.Format("The type {0} must inherit from {1}.", type, typeof (Service))).LogError();
                return type;
            }


            public IDictionary<string, string> Settings
            {
                get { return values; }
            }

            #endregion
        }

        #endregion
    }
}