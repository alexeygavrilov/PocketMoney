using System;
using System.Configuration;
using System.Reflection;
using Castle.Core.Configuration;
using Castle.Facilities.NHibernateIntegration;
using Castle.Facilities.NHibernateIntegration.Builders;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using PocketMoney.Data.NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace PocketMoney.Configuration.Web
{
    public class FluentNHibernateConfigurationBuilder : IConfigurationBuilder
    {
        #region IConfigurationBuilder Members

        public NHibernate.Cfg.Configuration GetConfiguration(IConfiguration facilityConfiguration)
        {
            var builder = new DefaultConfigurationBuilder();
            NHibernate.Cfg.Configuration configuration = builder.GetConfiguration(facilityConfiguration);
            ConnectionStringSettings connectionStringSetting = ConfigurationManager.ConnectionStrings["DefaultConnection"];
            configuration = Fluently.Configure(configuration)
                .Database(() =>
                {
                    if (connectionStringSetting.ProviderName.StartsWith("System.Data.SqlServerCe",
                                                                        StringComparison.OrdinalIgnoreCase))
                    {
                        return
                            MsSqlCeConfiguration.Standard.ConnectionString(
                                connectionStringSetting.ConnectionString);
                    }

                    return
                        MsSqlConfiguration.MsSql2008.ConnectionString(
                            connectionStringSetting.ConnectionString);
                })
                .Mappings(m =>
                {
                    foreach (Assembly assembly in new BuildManager().ApplicationAssemblies)
                    {
                        m.FluentMappings.AddFromAssembly(assembly);
                    }
                    m.FluentMappings.Conventions.AddFromAssemblyOf<DatabaseSchemaBuilder>();
                })
                .ExposeConfiguration(SchemaMetadataUpdater.QuoteTableAndColumns)
                .ExposeConfiguration(x => x.SetInterceptor(new EntityInterceptor()))
                .BuildConfiguration();
            configuration.AddAssembly(GetType().Assembly);
            return configuration;
        }

        #endregion
    }
}