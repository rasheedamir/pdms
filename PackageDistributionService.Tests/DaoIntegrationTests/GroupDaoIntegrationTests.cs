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
    public class GroupDaoIntegrationTests : CrudTest<Group, int>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GroupDaoIntegrationTests));
        private IGroupDao _groupDao;

        /// <summary>
        /// GroupDao - Dependency injected
        /// </summary>
        public IGroupDao GroupDao
        {
            set { _groupDao = value; }
        }

        /// <summary>
        /// Sanity check to ensure everything is properly setup!
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            Assert.IsNotNull(_groupDao, "GroupDao is null");
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void GetAll_ReturnsAll()
        {
            var entity = BuildEntity();
            _groupDao.Save(entity);
            Assert.AreEqual(1, _groupDao.GetAll().Count);
        }

        /// <summary>
        /// Test criteria queries work
        /// </summary>
        [Test]
        public void CriteriaQueryWorks()
        {
            var insertedEntity = EntityBuilder.AddGroup(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("Id", insertedEntity.Id),
                    Restrictions.Eq("Name", insertedEntity.Name),
                    Restrictions.Eq("DateCreated", insertedEntity.DateCreated),
                };
            var list = _groupDao.GetByCriteria(criterion.ToArray());
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
            var insertedEntity = EntityBuilder.AddGroup(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var retrievedEntity = _groupDao.GetById(insertedEntity.Id);
            Log.Debug("Retrieved Entity : " + retrievedEntity);
            AssertAreEqual(insertedEntity, retrievedEntity);
        }

        /// <summary>
        /// Test that get all works
        /// </summary>
        [Test]
        public void GetAllWorks()
        {
            EntityBuilder.AddGroup(Session);
            EntityBuilder.AddGroup(Session);
            var list = _groupDao.GetAll();
            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Group BuildEntity()
        {
            return EntityBuilder.BuildGroup();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void ModifyEntity(Group entity)
        {
            entity.Name = "Updated New Group";
            entity.DateCreated = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedEntity"></param>
        /// <param name="actualEntity"></param>
        protected override void AssertAreEqual(Group expectedEntity, Group actualEntity)
        {
            Assert.AreEqual(expectedEntity.Id, actualEntity.Id);
            Assert.AreEqual(expectedEntity.Name, actualEntity.Name);
            //Assert.AreEqual(expectedEntity.CreationTimestamp, actualEntity.CreationTimestamp);
            var result = expectedEntity.Equals(actualEntity);
            Assert.AreEqual(result, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void AssertValidId(Group entity)
        {
            Assert.AreEqual(entity.Id>0, true);
        }
    }
}
