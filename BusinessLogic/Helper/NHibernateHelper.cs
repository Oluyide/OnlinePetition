using System.Reflection;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace BusinessLogic.Helpers
{
    public sealed class NHibernateHelper
    {
        private const string CURRENT_NHIBERNATE_SESSION_KEY = "MyLocalGovt.SESSION.KEY";
        private static readonly ISessionFactory sessionFactory;

        static NHibernateHelper()
        {
            sessionFactory = ConfigureNHibernate();
        }

        private static ISessionFactory ConfigureNHibernate()
        {
            var configure = new Configuration();
            var mapping = GetMappings();
            configure.Configure();
            configure.AddDeserializedMapping(mapping, "NHSchema");

            configure.Configure()
                .SetProperty(NHibernate.Cfg.Environment.ConnectionStringName, "OnlinePetition")
                .SetProperty(NHibernate.Cfg.Environment.ShowSql, "true")
                .SetProperty(NHibernate.Cfg.Environment.BatchSize, "0");
            //.SetProperty(NHibernate.Cfg.Environment.ConnectionDriver,
            //    typeof(MiniProfilerSql2008ClientDriver).AssemblyQualifiedName);
            
            return configure.BuildSessionFactory();
        }

        private static HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMappings(Assembly.Load("BusinessLogic").GetTypes());
            var mapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            return mapping;
        }


        public static ISession GetCurrentSession()
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = context.Items[CURRENT_NHIBERNATE_SESSION_KEY] as ISession;

            if (currentSession == null)
            {
                currentSession = sessionFactory.OpenSession();
                context.Items[CURRENT_NHIBERNATE_SESSION_KEY] = currentSession;
            }
            if (currentSession.Connection.State == System.Data.ConnectionState.Closed)
            {
                currentSession = sessionFactory.OpenSession();
            }
            if (!currentSession.IsConnected)
            {
                currentSession = sessionFactory.OpenSession();
            }
            if (!currentSession.IsOpen)
            {
                currentSession = sessionFactory.OpenSession();
            }
            if (currentSession.IsDirty())
            {
                currentSession.Clear();
            }

            return currentSession;
        }

        public static void CloseSession()
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                ISession currentSession = context.Items[CURRENT_NHIBERNATE_SESSION_KEY] as ISession;

                if (currentSession == null)
                {
                    return;
                }
                currentSession.Clear();
                currentSession.Close();
                context.Items.Remove(CURRENT_NHIBERNATE_SESSION_KEY);
            }
        }

        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }
    }
}
