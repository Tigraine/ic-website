namespace ImagineClub.Tests
{
    using System.Collections.Generic;
    using Castle.ActiveRecord;
    using Castle.ActiveRecord.Framework.Config;
    using Web.Models;

    public class ActiveRecordInMemoryTestBase
    {
        public ActiveRecordInMemoryTestBase()
        {
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            if (!ActiveRecordStarter.IsInitialized)
                Initialize();
            ActiveRecordStarter.CreateSchema();
        }

        private static void Initialize()
        {
            IDictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");
            properties.Add("connection.provider",
                           "ImagineClub.Tests.SqLiteInMemoryTestingConnectionProvider, ImagineClub.Tests");
            properties.Add("connection.connection_string", "Data Source=:memory:;Version=3;New=True;");
            properties.Add("show_sql", "true");
            properties.Add("proxyfactory.factory_class",
                           "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");

            var source = new InPlaceConfigurationSource();
            source.Add(typeof (ActiveRecordBase), properties);

            ActiveRecordStarter.Initialize(source, typeof (Member).Assembly.GetTypes());
            ActiveRecordStarter.CreateSchema();
        }
    }
}