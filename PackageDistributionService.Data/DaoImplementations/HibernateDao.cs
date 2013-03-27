using System.Collections.Generic;
using NHibernate;

namespace PackageDistributionService.Data.DaoImplementations
{
    /// <summary>
    /// Base class for data access operations.
    /// </summary>
    public abstract class HibernateDao
    {
        private ISessionFactory _sessionFactory;

        /// <summary>
        /// Session factory for sub-classes.
        /// </summary>
        public ISessionFactory SessionFactory
        {
            protected get { return _sessionFactory; }
            set { _sessionFactory = value; }
        }

        /// <summary>
        /// Get's the current active session.
        /// </summary>
        protected ISession CurrentSession
        {
            get { return _sessionFactory.GetCurrentSession(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected IList<T> GetAll<T>() where T : class
        {
            var criteria = CurrentSession.CreateCriteria<T>();
            return criteria.List<T>();
        }
    }
}
