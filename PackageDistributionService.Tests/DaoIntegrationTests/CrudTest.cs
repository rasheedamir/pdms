using Common.Logging;
using NUnit.Framework;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    /// <summary>
    /// Base CRUD test cases class
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class CrudTest<TEntity, TId> : NHibernateTest where TEntity : IEntityWithTypedId<TId>
    {
// ReSharper disable StaticFieldInGenericType
        private static readonly ILog Log = LogManager.GetLogger(typeof(CrudTest<TEntity, TId>));
// ReSharper restore StaticFieldInGenericType

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public virtual void SelectQueryWorks()
        {
            Session.CreateCriteria(typeof(TEntity)).SetMaxResults(5).List();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public virtual void AddEntity_EntityWasAdded()
        {
            var entity = BuildEntity();
            InsertEntity(entity);
            Session.Evict(entity);
            Log.Debug("Entity : " + entity);
            var reloadedEntity = Session.Get<TEntity>(entity.Id);
            Log.Debug("Reloaded Entity : " + entity); 
            Assert.IsNotNull(reloadedEntity);
            AssertAreEqual(entity, reloadedEntity);
            AssertValidId(reloadedEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public virtual void UpdateEntity_EntityWasUpdated()
        {
            var entity = BuildEntity();
            InsertEntity(entity);
            Log.Debug("Original Entity : " + entity);
            ModifyEntity(entity);
            UpdateEntity(entity);
            Session.Evict(entity);
            Log.Debug("Modified Entity : " + entity);
            var reloadedEntity = Session.Get<TEntity>(entity.Id);
            Log.Debug("Reloaded Entity : " + entity);
            Assert.IsNotNull(reloadedEntity);
            AssertAreEqual(entity, reloadedEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public virtual void DeleteEntity_EntityWasDeleted()
        {
            var entity = BuildEntity();
            InsertEntity(entity);
            DeleteEntity(entity);
            Assert.IsNull(Session.Get<TEntity>(entity.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void InsertEntity(TEntity entity)
        {
            Session.Save(entity);
            Session.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void UpdateEntity(TEntity entity)
        {
            Session.Update(entity);
            Session.Flush();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void DeleteEntity(TEntity entity)
        {
            Session.Delete(entity);
            Session.Flush();
        }

        protected abstract TEntity BuildEntity();
        protected abstract void ModifyEntity(TEntity entity);

        protected abstract void AssertAreEqual(TEntity expectedEntity, TEntity actualEntity);

        protected abstract void AssertValidId(TEntity entity);
    }
}
