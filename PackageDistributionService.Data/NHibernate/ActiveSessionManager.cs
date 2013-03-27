using NHibernate;

namespace PackageDistributionService.Data.NHibernate
{
    public class ActiveSessionManager : IActiveSessionManager
    {
        private ISessionFactory _sessionFactory;

        /// <summary>
        /// Dependency injected
        /// </summary>
        public ISessionFactory SessionFactory
        {
            set { _sessionFactory = value; }
        }

        /// <summary>
        /// Returns current active session
        /// </summary>
        /// <returns></returns>
        public ISession GetActiveSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

    }
}
