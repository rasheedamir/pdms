using System;
using System.Collections.Generic;
using Common.Logging;
using NHibernate.Criterion;
using NUnit.Framework;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    /// <summary>
    /// This class contains tests for GroupDao
    /// </summary>
    [TestFixture]
    public class PackageGroupDaoIntegrationTests : CrudTest<IPackageGroup, int>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PackageGroupDaoIntegrationTests));
        private IPackageGroupDao _packageGroupDao;

        /// <summary>
        /// PackageGroupDao - Dependency injected
        /// </summary>
        public IPackageGroupDao PackageGroupDao
        {
            set { _packageGroupDao = value; }
        }

        /// <summary>
        /// Sanity check to ensure everything is properly setup!
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            Assert.IsNotNull(_packageGroupDao, "PackageGroupDao is null");
        }

        /// <summary>
        /// Test criteria queries work
        /// </summary>
        [Test]
        public void CriteriaQueryWorks()
        {
            var insertedEntity = EntityBuilder.AddPackageGroup(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("Id", insertedEntity.Id),
                    Restrictions.Eq("ActivationTimestamp", insertedEntity.ActivationTimestamp),
                    Restrictions.Eq("GroupId", insertedEntity.GroupId),
                    Restrictions.Eq("PackageVersionId", insertedEntity.PackageVersionId),
                };
            var list = _packageGroupDao.GetByCriteria(criterion.ToArray());
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
            var insertedEntity = EntityBuilder.AddPackageGroup(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var retrievedEntity = _packageGroupDao.GetById(insertedEntity.Id);
            Log.Debug("Retrieved Entity : " + retrievedEntity);
            AssertAreEqual(insertedEntity, retrievedEntity);
        }

        /// <summary>
        /// Test that get all works
        /// </summary>
        [Test]
        public void GetAllWorks()
        {
            EntityBuilder.AddPackageGroup(Session);
            EntityBuilder.AddPackageGroup(Session);
            var list = _packageGroupDao.GetAll();
            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IPackageGroup BuildEntity()
        {
            return EntityBuilder.BuildPackageGroup(Session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void ModifyEntity(IPackageGroup entity)
        {
            entity.ActivationTimestamp = DateTime.Now.AddDays(-1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedEntity"></param>
        /// <param name="actualEntity"></param>
        protected override void AssertAreEqual(IPackageGroup expectedEntity, IPackageGroup actualEntity)
        {
            Assert.AreEqual(expectedEntity.Id, actualEntity.Id);
            //Assert.AreEqual(expectedEntity.ActivationTimestamp, actualEntity.ActivationTimestamp);
            Assert.AreEqual(expectedEntity.GroupId, actualEntity.GroupId);
            Assert.AreEqual(expectedEntity.PackageVersionId, expectedEntity.PackageVersionId);
            var result = expectedEntity.Equals(actualEntity);
            Assert.AreEqual(result, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void AssertValidId(IPackageGroup entity)
        {
            Assert.AreEqual(entity.Id > 0, true);
        }
    }
}
