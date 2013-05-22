using System.Collections.Generic;
using Common.Logging;
using NHibernate.Criterion;
using NUnit.Framework;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    /// <summary>
    /// Unit tests for PackageVersionDao for accessing instances of <see cref="PackageVersion" /> from DB.
    /// </summary>
    public class PackageVersionDaoIntegrationTests : CrudTest<PackageVersion, int>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PackageVersionDaoIntegrationTests));
        private IPackageVersionDao _packageVersionDao;

        /// <summary>
        /// GroupStoreDao - Dependency injected
        /// </summary>
        public IPackageVersionDao PackageVersionDao
        {
            set { _packageVersionDao = value; }
        }

        /// <summary>
        /// Sanity check to ensure everything is properly setup!
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            Assert.IsNotNull(_packageVersionDao, "PackageVersionDao is null");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void GetLatestPackageVersionWorks()
        {
            var insertedEntity1 = EntityBuilder.AddPackageVersion(Session);
            Log.Debug("Inserted Entity 1: " + insertedEntity1);
            var insertedEntity2 = EntityBuilder.AddPackageVersion(Session);
            Log.Debug("Inserted Entity 2: " + insertedEntity2);
            var latestPackageVersion = _packageVersionDao.GetLatestPackageVersion();
            Log.Debug("Latest Package Version: " + latestPackageVersion);
            AssertAreEqual(insertedEntity2, latestPackageVersion);
        }

        /// <summary>
        /// Test criteria queries work
        /// </summary>
        [Test]
        public void CriteriaQueryWorks()
        {
            var insertedEntity = EntityBuilder.AddPackageVersion(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("Id", insertedEntity.Id),
                    Restrictions.Eq("VersionNumber", insertedEntity.VersionNumber),
                    Restrictions.Eq("Comment", insertedEntity.Comment),
                    Restrictions.Eq("DateCreated", insertedEntity.DateCreated),
                    Restrictions.Eq("PackagePath", insertedEntity.PackagePath)
                };
            var list = _packageVersionDao.GetByCriteria(criterion.ToArray());
            Log.Debug("Retrieved Entity : " + list[0]);
            Assert.AreEqual(1,list.Count);
            AssertAreEqual(insertedEntity, list[0]);
        }

        /// <summary>
        /// Test that get by id works
        /// </summary>
        [Test]
        public void GetIdWorks()
        {
            var insertedEntity = EntityBuilder.AddPackageVersion(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var retrievedEntity = _packageVersionDao.GetById(insertedEntity.Id);
            Log.Debug("Retrieved Entity : " + retrievedEntity);
            AssertAreEqual(insertedEntity, retrievedEntity);
        }

        /// <summary>
        /// Test that get all works
        /// </summary>
        [Test]
        public void GetAllWorks()
        {
            EntityBuilder.AddPackageVersion(Session);
            EntityBuilder.AddPackageVersion(Session);
            var list = _packageVersionDao.GetAll();
            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// Builds an entity
        /// </summary>
        /// <returns></returns>
        protected override PackageVersion BuildEntity()
        {
            return EntityBuilder.BuildPackageVersion();
        }

        /// <summary>
        /// Modifies attributes of an entity
        /// </summary>
        /// <param name="entity"></param>
        protected override void ModifyEntity(PackageVersion entity)
        {
            entity.Comment = "Brand New Package 2";
        }

        /// <summary>
        /// Asserts if the Id (primary key) of entity is valid
        /// </summary>
        /// <param name="entity"></param>
        protected override void AssertValidId(PackageVersion entity)
        {
            Assert.AreEqual(entity.Id > 0, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedEntity"></param>
        /// <param name="actualEntity"></param>
        protected override void AssertAreEqual(PackageVersion expectedEntity, PackageVersion actualEntity)
        {
            Assert.AreEqual(expectedEntity.Id, actualEntity.Id);
            Assert.AreEqual(expectedEntity.Comment, actualEntity.Comment);
            //Assert.AreEqual(expectedEntity.CreationTimestamp, actualEntity.CreationTimestamp);
            Assert.AreEqual(expectedEntity.VersionNumber, actualEntity.VersionNumber);
            Assert.AreEqual(expectedEntity.PackagePath, expectedEntity.PackagePath);
            var result = expectedEntity.Equals(actualEntity);
            Assert.AreEqual(result, true);
        }
    }
}
