using System.Data.SQLite;
using System.Reflection;
using NHibernate.Dialect;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DataBase
{
    public struct ConnectionParams
    {
        public static ConnectionParams FromConfig => new ConnectionParams();
        
        public readonly string Source;

        public ConnectionParams(string source)
        {
            Source = source;
        }
    }
    
    public static class Connection
    {
        public static ISession OpenSession()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration
                    .Standard.UsingFile("Chess.db"))
                .Mappings(x => x
                    .FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildConfiguration()
                .BuildSessionFactory().OpenSession();
        }
    }
}