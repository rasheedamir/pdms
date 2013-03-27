using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using Spring.Transaction;
using Spring.Transaction.Interceptor;

namespace ProjectBase.Data.Dao
{
    /// <summary>
    /// Abstract DAO class that every DAO concrete implementation extends from.
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TIdT">Unique identifier</typeparam>
    public abstract class AbstractNHibernateDao<TEntity, TIdT> : IDao<TEntity, TIdT>, ISupportsSaveDao<TEntity, TIdT>, ISupportsCriteriaDao<TEntity>, ISupportsDeleteDao<TEntity>
    {
        private ISessionFactory _sessionFactory;
        private readonly Type _persitentType = typeof(TEntity);

        /// <summary>
        /// Session factory
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
        /// Loads an instance of type TEntity from the DB based on its ID.
        /// Why is Transaction property ReadOnly set to true?
        /// If a transaction performs only read operations against the underlying data store,
        /// the data store may be able to apply certain optimizations that take advantage of the
        /// read-only nature of the transaction. By declaring a transaction as read-only, you give
        /// the underlying data store the opportunity to apply those optimizations as it sees fit.
        /// Furthermore, if you’re using Hibernate as your persistence mechanism, declaring
        /// a transaction as read-only will result in Hibernate’s flush mode being set to
        /// FLUSH_NEVER. This tells Hibernate to avoid unnecessary synchronization of objects
        /// with the database, thus delaying all updates until the end of the transaction.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public TEntity GetById(TIdT id)
        {
            return CurrentSession.Get<TEntity>(id);
        }
        
        /// <summary>
        /// Loads every instance of the requested type with no filtering.
        /// </summary>
        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public IList<TEntity> GetAll()
        {
            return GetByCriteria();
        }

        /// <summary>
        /// For entities that have assigned ID's, you must explicitly call Save to add a new one.
        /// See http://www.hibernate.org/hib_docs/reference/en/html/mapping.html#mapping-declaration-id-assigned.
        /// TransactionPropagation.Required Indicates that the current method must run within a transaction.
        /// If an existing transaction is in progress, the method will run
        /// within that transaction. Otherwise, a new transaction will be started.
        /// </summary>
        [Transaction(TransactionPropagation.Required, ReadOnly = false)]
        public TIdT Save(TEntity entity)
        {
            return (TIdT)CurrentSession.Save(entity);
        }

        /// <summary>
        /// For entities with automatatically generated IDs, such as identity, SaveOrUpdate may 
        /// be called when saving a new entity.  SaveOrUpdate can also be called to _update_ any 
        /// entity, even if its ID is assigned.
        /// </summary>
        /// <param name="entity"></param>
        [Transaction(TransactionPropagation.Required, ReadOnly = false)]
        public void Update(TEntity entity)
        {
            CurrentSession.SaveOrUpdate(entity);
        }

        /// <summary>
        /// Loads every instance of the requested type using the supplied <see cref="ICriterion" />.
        /// If no <see cref="ICriterion" /> is supplied, this behaves like <see cref="GetAll" />.
        /// </summary>
        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public List<TEntity> GetByCriteria(params ICriterion[] criterion)
        {
            var criteria = CurrentSession.CreateCriteria(_persitentType);

            foreach (var criterium in criterion)
            {
                criteria.Add(criterium);
            }

            return criteria.List<TEntity>() as List<TEntity>;
        }

        /// <summary>
        /// Get all entity(s) by example
        /// </summary>
        /// <param name="exampleInstance"></param>
        /// <param name="propertiesToExclude"></param>
        /// <returns></returns>
        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public List<TEntity> GetByExample(TEntity exampleInstance, params string[] propertiesToExclude)
        {
            var criteria = CurrentSession.CreateCriteria(_persitentType);
            var example = Example.Create(exampleInstance);

            foreach (var propertyToExclude in propertiesToExclude)
            {
                example.ExcludeProperty(propertyToExclude);
            }

            criteria.Add(example);

            return criteria.List<TEntity>() as List<TEntity>;
        }

        /// <summary>
        /// Get unique entity by example
        /// </summary>
        /// <param name="exampleInstance"></param>
        /// <param name="propertiesToExclude"></param>
        /// <returns></returns>
        [Transaction(TransactionPropagation.Required, ReadOnly = true)]
        public TEntity GetUniqueByExample(TEntity exampleInstance, params string[] propertiesToExclude)
        {
            var foundList = GetByExample(exampleInstance, propertiesToExclude);

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }

            if (foundList.Count > 0)
            {
                return foundList[0];
            }
            return default(TEntity);
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity"></param>
        [Transaction(TransactionPropagation.Required, ReadOnly = false)]
        public void Delete(TEntity entity)
        {
            CurrentSession.Delete(entity);
        }
    }
}
