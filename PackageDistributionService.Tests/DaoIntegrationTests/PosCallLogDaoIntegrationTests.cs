using System;
using System.Collections.Generic;
using Common.Logging;
using NHibernate.Criterion;
using NUnit.Framework;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    public class PosCallLogDaoIntegrationTests : CrudTest<IPosCallLog, int>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PosCallLogDaoIntegrationTests));

        private IPosCallLogDao _posCallLogDao;

        /// <summary>
        /// PosCallLogDao - Dependency injected
        /// </summary>
        public IPosCallLogDao PosCallLogDao
        {
            set { _posCallLogDao = value; }
        }

        /// <summary>
        /// Sanity check to ensure everything is properly setup!
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            Assert.IsNotNull(_posCallLogDao, "PosCallLogDao is null");
        }

        /// <summary>
        /// Test criteria queries work
        /// </summary>
        [Test]
        public void CriteriaQueryWorks()
        {
            var insertedEntity = EntityBuilder.AddPosCallLog(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("Id", insertedEntity.Id),
                    Restrictions.Eq("CallTimestamp", insertedEntity.CallTimestamp),
                    Restrictions.Eq("CoopStoreId", insertedEntity.CoopStoreId),
                    Restrictions.Eq("HostName", insertedEntity.HostName),
                    Restrictions.Eq("IpAddress", insertedEntity.IpAddress),
                    Restrictions.Eq("PackageVersionId", insertedEntity.PackageVersionId),
                    Restrictions.Eq("PosManufacturerName", insertedEntity.PosManufacturerName),
                    Restrictions.Eq("PosNumber", insertedEntity.PosNumber),
                    Restrictions.Eq("PosVersion", insertedEntity.PosVersion),
                    Restrictions.Eq("TerminalSerialNumber", insertedEntity.TerminalSerialNumber)
                };
            var list = _posCallLogDao.GetByCriteria(criterion.ToArray());
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
            var insertedEntity = EntityBuilder.AddPosCallLog(Session);
            Log.Debug("Inserted Entity : " + insertedEntity);
            var retrievedEntity = _posCallLogDao.GetById(insertedEntity.Id);
            Log.Debug("Retrieved Entity : " + retrievedEntity);
            AssertAreEqual(insertedEntity, retrievedEntity);
        }

        /// <summary>
        /// Test that get all works
        /// </summary>
        [Test]
        public void GetAllWorks()
        {
            EntityBuilder.AddPosCallLog(Session);
            EntityBuilder.AddPosCallLog(Session);
            var list = _posCallLogDao.GetAll();
            Assert.AreEqual(2, list.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IPosCallLog BuildEntity()
        {
            return EntityBuilder.BuildPosCallLog(Session);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void ModifyEntity(IPosCallLog entity)
        {
            entity.CallTimestamp = DateTime.Now;
            entity.CoopStoreId = 23;
            entity.HostName = "My name was changed to second host";
            entity.IpAddress = "145.3.4.7";
            entity.PosManufacturerName = "R2M 3";
            entity.PosNumber = "987654321";
            entity.PosVersion = "8.0.0.1";
            entity.TerminalSerialNumber = "8888777766655";
            entity.HostName = "I am the second host!";
            entity.PosAssemblyLogs.Add(new PosAssemblyLog { AssemblyName = "I am assembly 3", AssemblyVersion = "4.5.3" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expectedEntity"></param>
        /// <param name="actualEntity"></param>
        protected override void AssertAreEqual(IPosCallLog expectedEntity, IPosCallLog actualEntity)
        {
            Assert.AreEqual(expectedEntity.Id, actualEntity.Id);
            //Assert.AreEqual(expectedEntity.CallTimestamp, actualEntity.CallTimestamp);
            Assert.AreEqual(expectedEntity.CoopStoreId, actualEntity.CoopStoreId);
            Assert.AreEqual(expectedEntity.HostName, actualEntity.HostName);
            Assert.AreEqual(expectedEntity.IpAddress, actualEntity.IpAddress);
            Assert.AreEqual(expectedEntity.PackageVersionId, actualEntity.PackageVersionId);
            Assert.AreEqual(expectedEntity.PosManufacturerName, actualEntity.PosManufacturerName);
            Assert.AreEqual(expectedEntity.PosNumber, actualEntity.PosNumber);
            Assert.AreEqual(expectedEntity.PosVersion, actualEntity.PosVersion);
            Assert.AreEqual(expectedEntity.TerminalSerialNumber, actualEntity.TerminalSerialNumber);
            var result = expectedEntity.Equals(actualEntity);
            Assert.AreEqual(result, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        protected override void AssertValidId(IPosCallLog entity)
        {
            Assert.AreEqual(entity.Id > 0, true);
        }
    }
}
