using System.Collections.Generic;
using Common.Logging;
using NHibernate.Criterion;
using NUnit.Framework;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    /// <summary>
    /// This class contains tests for GroupStoreDao
    /// </summary>
    [TestFixture]
    public class GroupStoreDaoIntegrationTests : CrudTest<IGroupStore, int>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GroupStoreDaoIntegrationTests));

        private IGroupStoreDao _groupStoreDao;

        /// <summary>
        /// GroupStoreDao - Dependency injected
        /// </summary>
        public IGroupStoreDao GroupStoreDao
        {
            set { _groupStoreDao = value; }
        }

        /// <summary>
        /// Sanity check to ensure everything is properly setup!
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            Assert.IsNotNull(_groupStoreDao, "GroupStoreDao is null");
        }

        /// <summary>
        /// Test criteria queries work
        /// </summary>
        [Test]
        public void CriteriaQueryWorks()
        {
            var insertedEntity = EntityBuilder.AddGroupStore(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("Id", insertedEntity.Id),
                    Restrictions.Eq("CoopStoreId", insertedEntity.CoopStoreId),
                    Restrictions.Eq("GroupId", insertedEntity.GroupId),
                };
            var list = _groupStoreDao.GetByCriteria(criterion.ToArray());
            Log.Debug("Retrieved Entity : " + list[0]);
            Assert.AreEqual(1, list.Count);
            AssertAreEqual(insertedEntity, list[0]);
        }

        /// <summary>
        /// Test that get by id works
        /// </summary>
        [Test]
        public void GetIdWorks()
        {
            var insertedEntity = EntityBuilder.AddGroupStore(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var retrievedEntity = _groupStoreDao.GetById(insertedEntity.Id);
            Log.Debug("Retrieved Entity : " + retrievedEntity);
            AssertAreEqual(insertedEntity, retrievedEntity);
        }

        /// <summary>
        /// Test that get all works
        /// </summary>
        [Test]
        public void GetAllWorks()
        {
            EntityBuilder.AddGroupStore(Session);
            EntityBuilder.AddGroupStore(Session);
            var list = _groupStoreDao.GetAll();
            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IGroupStore BuildEntity()
        {
            return EntityBuilder.BuildGroupStore(Session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void ModifyEntity(IGroupStore entity)
        {
            entity.CoopStoreId = 11;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedEntity"></param>
        /// <param name="actualEntity"></param>
        protected override void AssertAreEqual(IGroupStore expectedEntity, IGroupStore actualEntity)
        {
            Assert.AreEqual(expectedEntity.Id, actualEntity.Id);
            Assert.AreEqual(expectedEntity.CoopStoreId, actualEntity.CoopStoreId);
            Assert.AreEqual(expectedEntity.GroupId, actualEntity.GroupId);
            var result = expectedEntity.Equals(actualEntity);
            Assert.AreEqual(result, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void AssertValidId(IGroupStore entity)
        {
            Assert.AreEqual(entity.Id > 0, true);
        }
    }
}
