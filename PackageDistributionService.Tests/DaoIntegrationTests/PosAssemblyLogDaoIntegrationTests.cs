using System.Collections.Generic;
using Common.Logging;
using NHibernate.Criterion;
using NUnit.Framework;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    public class PosAssemblyLogDaoIntegrationTests : CrudTest<PosAssemblyLog, int>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PosAssemblyLogDaoIntegrationTests));

        private IPosAssemblyLogDao _posAssemblyLogDao;

        /// <summary>
        /// PosCallLogDao - Dependency injected
        /// </summary>
        public IPosAssemblyLogDao PosAssemblyLogDao
        {
            set { _posAssemblyLogDao = value; }
        }

        /// <summary>
        /// Sanity check to ensure everything is properly setup!
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            Assert.IsNotNull(_posAssemblyLogDao, "PosAssemblyLogDao is null");
        }

        /// <summary>
        /// Test criteria queries work
        /// </summary>
        [Test]
        public void CriteriaQueryWorks()
        {
            var insertedEntity = EntityBuilder.AddPosAssemblyLog(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("Id", insertedEntity.Id),
                    Restrictions.Eq("AssemblyName", insertedEntity.AssemblyName),
                    Restrictions.Eq("AssemblyVersion", insertedEntity.AssemblyVersion),
                    Restrictions.Eq("PosCallLogId", insertedEntity.PosCallLogId)
                };
            var list = _posAssemblyLogDao.GetByCriteria(criterion.ToArray());
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
            var insertedEntity = EntityBuilder.AddPosAssemblyLog(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var retrievedEntity = _posAssemblyLogDao.GetById(insertedEntity.Id);
            Log.Debug("Retrieved Entity : " + retrievedEntity);
            AssertAreEqual(insertedEntity, retrievedEntity);
        }

        /// <summary>
        /// Test that get all works
        /// </summary>
        [Test]
        public void GetAllWorks()
        {
            EntityBuilder.AddPosAssemblyLog(Session);
            EntityBuilder.AddPosAssemblyLog(Session);
            var list = _posAssemblyLogDao.GetAll();
            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override PosAssemblyLog BuildEntity()
        {
            return EntityBuilder.BuildPosAssemblyLog(Session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void ModifyEntity(PosAssemblyLog entity)
        {
            entity.AssemblyName = "I am updated new assembly";
            entity.AssemblyVersion = "6.5.4.3";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedEntity"></param>
        /// <param name="actualEntity"></param>
        protected override void AssertAreEqual(PosAssemblyLog expectedEntity, PosAssemblyLog actualEntity)
        {
            Assert.AreEqual(expectedEntity.Id, actualEntity.Id);
            Assert.AreEqual(expectedEntity.AssemblyName, expectedEntity.AssemblyName);
            Assert.AreEqual(expectedEntity.AssemblyVersion, expectedEntity.AssemblyVersion);
            Assert.AreEqual(expectedEntity.PosCallLogId, actualEntity.PosCallLogId);
            var result = expectedEntity.Equals(actualEntity);
            Assert.AreEqual(result, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void AssertValidId(PosAssemblyLog entity)
        {
            Assert.AreEqual(entity.Id > 0, true);
        }
    }
}
