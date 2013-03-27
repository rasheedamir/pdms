using System;
using System.Collections.Generic;
using NUnit.Framework;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;
using PackageDistributionService.Service.Services;
using PackageDistributionService.Core.Dto;

namespace PackageDistributionService.Tests.ServiceIntegrationTests
{
    /// <summary>
    /// This class contains tests for DistributionService
    /// </summary>
    [TestFixture]
    public class DistributionServiceIntegrationTests : AbstractIntegrationTests
    {
        //ToDo: Mock Repository(s) or Dao Layer(s)
        //ToDo: Add test data - Setup
        //ToDo: Delete test data - Teardown
        //ToDo: Ndbunit ?

        //ToDo: Define test data as static

        private static readonly IGroup[] Groups = new IGroup[] 
                                {new Group{CreationTimestamp = DateTime.Now, Name = "Group 1"},
                               new Group{CreationTimestamp = DateTime.Now.AddMinutes(-10), Name = "Group 2"}};

        private static readonly IPackageVersion[] PackageVersions = new IPackageVersion[] 
                                {new PackageVersion {Comment = "This is package 1", VersionNumber = "1.0", CreationTimestamp = DateTime.Now, PackagePath = "C:\\PDS\\packages\\packae_1.0.7z"},
                                new PackageVersion {Comment = "This is package 2", VersionNumber = "2.0", CreationTimestamp = DateTime.Now, PackagePath = "C:\\PDS\\packages\\packae_1.0.7z"}};

        private static readonly IPackageGroup[] PackageGroups = new IPackageGroup[] 
                                {new PackageGroup{ActivationTimestamp = DateTime.Now.AddMinutes(-10)}, new PackageGroup{ActivationTimestamp = DateTime.Now}};

        private static readonly IGroupStore[] GroupStores = new IGroupStore[] 
                                {new GroupStore{CoopStoreId = 1111}, new GroupStore{CoopStoreId = 2222} };

        //private static readonly ILog Log = LogManager.GetLogger(typeof(DistributionServiceIntegrationTests));
        private IDistributionService _distributionService;
        private IGroupDao _groupDao;
        private IGroupStoreDao _groupStoreDao;
        private IPackageGroupDao _packageGroupDao;
        private IPackageVersionDao _packageVersionDao;
        private IPosCallLogDao _posCallLogDao;

        /// <summary>
        /// PosCallLogDao - Dependency injected
        /// </summary>
        public IPosCallLogDao PosCallLogDao
        {
            set { _posCallLogDao = value; }
        }
        /// <summary>
        /// GroupStoreDao - Dependency injected
        /// </summary>
        public IPackageVersionDao PackageVersionDao
        {
            set { _packageVersionDao = value; }
        }

        /// <summary>
        /// PackageGroupDao - Dependency injected
        /// </summary>
        public IPackageGroupDao PackageGroupDao
        {
            set { _packageGroupDao = value; }
        }

        /// <summary>
        /// GroupStoreDao - Dependency injected
        /// </summary>
        public IGroupStoreDao GroupStoreDao
        {
            set { _groupStoreDao = value; }
        }        
        
        /// <summary>
        /// GroupDao - Dependency injected
        /// </summary>
        public IGroupDao GroupDao
        {
            set { _groupDao = value; }
        }        

        /// <summary>
        /// DistributionService - Dependency injected
        /// </summary>
        public IDistributionService DistributionService
        {
            set { _distributionService = value; }
        }

        /// <summary>
        /// Sanity check to ensure everything is properly setup!
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            Assert.IsNotNull(_distributionService, "DistributionService is null");
            Assert.IsNotNull(_groupDao, "GroupDao is null");
            Assert.IsNotNull(_groupStoreDao, "GroupStoreDao is null");
            Assert.IsNotNull(_packageGroupDao, "PackageGroupDao is null");
            Assert.IsNotNull(_packageVersionDao, "PackageVersionDao is null");
            Assert.IsNotNull(_posCallLogDao, "PosCallLogDao is null");
        }

        /// <summary>
        /// Checks if no new package available then correct status is returned
        /// </summary>
        [Test]
        public void GetPackage_NoNewPackageWorks()
        {
            var packageId = _packageVersionDao.Save(PackageVersions[0]);
            var package = _distributionService.GetPackage(CreatePosRequest(1111, packageId));
            Assert.AreEqual(0, package.Status);
            Assert.AreEqual("No new package available.", package.Message);
        }

        /// <summary>
        /// Checks if new package is available but no group has been assigned
        /// to it
        /// </summary>
        [Test]
        public void GetPackage_NewPackageAvailableButNotAssignedToAnyGroupWorks()
        {
            var packageId1 = _packageVersionDao.Save(PackageVersions[0]);
            _packageVersionDao.Save(PackageVersions[1]);
            var package = _distributionService.GetPackage(CreatePosRequest(1111, packageId1));
            Assert.AreEqual(0, package.Status);
            Assert.AreEqual("New package available but package hasn't been assigned to any group.", package.Message);
        }

        /// <summary>
        /// Checks if new package available but not assigned to store in question
        /// </summary>
        [Test]
        public void GetPackage_NewPackageAvailableButStoreHasnotBeenAssignedToAnyGroup()
        {
            var packageId1 = _packageVersionDao.Save(PackageVersions[0]);
            var packageId2 = _packageVersionDao.Save(PackageVersions[1]);
            var groupId = _groupDao.Save(Groups[0]);
            PackageGroups[0].GroupId = groupId;
            PackageGroups[0].PackageVersionId = packageId1;
            _packageGroupDao.Save(PackageGroups[0]);
            PackageGroups[1].GroupId = groupId;
            PackageGroups[1].PackageVersionId = packageId2;
            _packageGroupDao.Save(PackageGroups[1]);
            var package = _distributionService.GetPackage(CreatePosRequest(2222, packageId1));
            Assert.AreEqual(0, package.Status);
            Assert.AreEqual("New package available but store hasn't been assigned to any group.", package.Message);
        }

        /// <summary>
        /// Checks if new package available but not assigned to any group containing
        /// the store in question
        /// </summary>
        [Test]
        public void GetPackage_NewPackageAvailableButNotAssignedToAnyGroupContainingThisStoreWorks()
        {
            var packageId1 = _packageVersionDao.Save(PackageVersions[0]);
            var packageId2 = _packageVersionDao.Save(PackageVersions[1]);
            var groupId1 = _groupDao.Save(Groups[0]);
            var groupId2 = _groupDao.Save(Groups[1]);
            PackageGroups[0].GroupId = groupId1;
            PackageGroups[0].PackageVersionId = packageId1;
            _packageGroupDao.Save(PackageGroups[0]);
            PackageGroups[1].GroupId = groupId1;
            PackageGroups[1].PackageVersionId = packageId2;
            _packageGroupDao.Save(PackageGroups[1]);
            GroupStores[0].GroupId = groupId1;
            _groupStoreDao.Save(GroupStores[0]);
            GroupStores[1].GroupId = groupId2;
            _groupStoreDao.Save(GroupStores[1]);
            var package = _distributionService.GetPackage(CreatePosRequest(2222, packageId1));
            Assert.AreEqual(0, package.Status);
            Assert.AreEqual("New package available but it hasn't been assigned to any group containing this store.", package.Message);
        }

        /// <summary>
        /// Checks a new package is available and is returned correctly
        /// </summary>
        [Test]
        public void GetPackage_NewPackageAvailableWorks()
        {
            var packageId1 = _packageVersionDao.Save(PackageVersions[0]);
            var packageId2 = _packageVersionDao.Save(PackageVersions[1]);
            var groupId1 = _groupDao.Save(Groups[0]);
            PackageGroups[0].GroupId = groupId1;
            PackageGroups[0].PackageVersionId = packageId1;
            _packageGroupDao.Save(PackageGroups[0]);
            PackageGroups[1].GroupId = groupId1;
            PackageGroups[1].PackageVersionId = packageId2;
            _packageGroupDao.Save(PackageGroups[1]);
            GroupStores[0].GroupId = groupId1;
            _groupStoreDao.Save(GroupStores[0]);
            GroupStores[1].GroupId = groupId1;
            _groupStoreDao.Save(GroupStores[1]);
            var packageFromDb = _packageVersionDao.GetById(packageId2);
            var packageFromService = _distributionService.GetPackage(CreatePosRequest(2222, packageId1));
            Assert.AreEqual(packageFromDb.Id, packageFromService.PackageVersionId);
            Assert.AreEqual(1, packageFromService.Status);
            Assert.AreEqual("New package attached.", packageFromService.Message);
        }

        /// <summary>
        /// Check in case of exception the status is set correctly
        /// </summary>
        [Test]
        public void GetPackage_CatchesExceptionWorks()
        {
            // Sending invalid packageversionid which should result an exception
            var package = _distributionService.GetPackage(CreatePosRequest(2222, -1));
            Assert.AreEqual(2, package.Status);
            Assert.AreEqual("Exception occured. Please contact admininstrator!", package.Message);
        }

        /// <summary>
        /// Creates pos request message which can be reused in different methods.
        /// </summary>
        /// <returns></returns>
        private static PosRequest CreatePosRequest(int coopStoreId, int packageVersionId)
        {
            var request = new PosRequest();
            request.CoopStoreId = coopStoreId;
            request.HostName = "Host 1";
            request.IpAddress = "192.11.12.33";
            request.PackageVersionId = packageVersionId;
            request.PosManufacturerName = "R2M";
            request.PosNumber = "12341234";
            request.PosVersion = "1.2.0";
            request.TerminalSerialNumber = "123451234512345";            
            var assemblyInfos = new List<AssemblyInfo>();
            assemblyInfos.Add(new AssemblyInfo { AssemblyName = "Hibernate Assembly 1", AssemblyVersion = "1.0.0.1" });
            assemblyInfos.Add(new AssemblyInfo { AssemblyName = "Spring Assembly 2", AssemblyVersion = "2.0.0.1" });
            request.AssemblyInfos = assemblyInfos;
            return request;
        }
    }
}
