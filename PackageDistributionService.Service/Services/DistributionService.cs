using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using PackageDistributionService.Core.Domain;
using PackageDistributionService.Core.Dto;
using PackageDistributionService.Core.DataInterfaces;
using Spring.Transaction;
using log4net;
using Spring.Transaction.Interceptor;
using PackageDistributionService.Aspects.PerformanceMonitoring;

namespace PackageDistributionService.Service.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class DistributionService : IDistributionService
    {
        private static readonly MD5 Md5 = MD5.Create();
        private static readonly ILog Log = LogManager.GetLogger(typeof(DistributionService));
        private IPosCallLogDao _posCallLogDao;
        private IPosAssemblyLogDao _posAssemblyLogDao;
        private IPackageVersionDao _packageVersionDao;
        private IPackageGroupDao _packageGroupDao;
        private IGroupStoreDao _groupStoreDao;

        /// <summary>
        /// PosCallLogDao - Dependency injected
        /// </summary>
        public IPosCallLogDao PosCallLogDao
        {
            set { _posCallLogDao = value; }
        }

        /// <summary>
        /// PosAssemblyLogDao - Dependency injected
        /// </summary>
        public IPosAssemblyLogDao PosAssemblyLogDao
        {
            set { _posAssemblyLogDao = value; }
        }
        
        /// <summary>
        /// PackageVersionDao - Dependency injected
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
        /// This method returns the package depending on its availability.
        /// </summary>
        /// <param name="request">Request from POS</param>
        /// <returns>
        /// 
        /// </returns>
        [PerformanceMonitoring]
        [Transaction(TransactionPropagation.Required, ReadOnly = false)]
        public PosPackage GetPackage(PosRequest request)
        {
            var posPackage = new PosPackage();

            try
            {
                // Step : Log the information in file
                Log.Info(String.Format("Request recieved for {0}: {1}", request.CoopStoreId, request));

                // Step : Insert data into poscalllog and posassemblylog tables in database
                InsertPosCallLog(request);

                // Step : Lookup if there is a new package version in the database (packageversions) - packageversion
                var packageVersion = LookupLatestPackageVersion(request.PackageVersionId);
                Log.Info(String.Format("Current package of store {0} is {1} and the latest package is {2}: ", request.CoopStoreId, request.PackageVersionId, packageVersion));
                if (packageVersion == null)
                {
                    posPackage.Status = 0; // Nothing new.
                    posPackage.Message = "No new package available.";
                    // Nothing else simply return!
                    Log.Info(String.Format("Response sent for {0}: {1}", request.CoopStoreId, posPackage.Message));
                    return posPackage;
                }
                // Step : Lookup to which groups this package can be deployed (packagegroups) - groups list
                var packageGroupsList = LookupPackageGroupsByPackageVersionId(packageVersion.Id);
                Log.Info(String.Format("PackageGroupsList.Count: {0} for PackagVersion.Id: {1}", packageGroupsList.Count, packageVersion.Id));

                if (packageGroupsList.Count > 0)
                {
                    // Step : Lookup if coopstoreid belongs to any of the groups                    
                    var groupStoresList = LookupGroupStoresByStoreId(request.CoopStoreId);
                    Log.Info(String.Format("GroupStoresList.Count: {0} for  CoopStoreId: {1}", groupStoresList.Count, request.CoopStoreId));

                    if (groupStoresList.Count > 0)
                    {
                        foreach (var groupStore in groupStoresList)
                        {
                            var gs = groupStore;
                            var packageGroup =
                                from g in packageGroupsList
                                where g.GroupId == gs.GroupId
                                select g;

                            var packageGroups = packageGroup as IPackageGroup[] ?? packageGroup.ToArray();
                            if (packageGroups.FirstOrDefault() == null) continue;
                                
                            posPackage.Status = 1; // Something new.
                            posPackage.Message = "New package attached.";
                            posPackage.ActivationTime = packageGroups.First().ActivationTimestamp; // Activation timestamp
                            posPackage.PackageVersionId = packageVersion.Id; // Package verison id
                            posPackage.Filename = packageVersion.PackageName;
                            posPackage.PackageContents = File.ReadAllBytes(packageVersion.PackagePath);
                            posPackage.Md5CheckSum = CalculateChecksum(packageVersion.PackagePath);
                            Log.Info(String.Format("Response sent for {0}: {1}", request.CoopStoreId, posPackage.ToStringMini()));
                            return posPackage;
                        }
                        posPackage.Status = 0; // Nothing new.
                        posPackage.Message = "New package available but it hasn't been assigned to any group containing this store.";
                        // Nothing else simply return!
                        Log.Info(String.Format("Response sent for {0}: {1}", request.CoopStoreId, posPackage.Message));
                        return posPackage;                                                    
                    }
                    posPackage.Status = 0; // Nothing new.
                    posPackage.Message = "New package available but store hasn't been assigned to any group.";
                    // Nothing else simply return!
                    Log.Info(String.Format("Response sent for {0}: {1}", request.CoopStoreId, posPackage.Message));
                    return posPackage;
                }
                posPackage.Status = 0; // Nothing new.
                posPackage.Message = "New package available but package hasn't been assigned to any group.";
                // Nothing else simply return!
                Log.Info(String.Format("Response sent for {0}: {1}", request.CoopStoreId, posPackage.Message));
                return posPackage;
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                Log.Error(exception.StackTrace);
                posPackage.Status = 2; // Exception! Anything went wrong. Contact system admin.
                posPackage.Message = "Exception occured. Please contact admininstrator!";
                return posPackage;                                                   
            }
        }

        /// <summary>
        /// Inserts point of sales call data into database
        /// </summary>
        /// <param name="request"></param>
        private void InsertPosCallLog(PosRequest request)
        {
            IPosCallLog posCallLog = new PosCallLog();
            posCallLog.CallTimestamp = DateTime.Now;
            posCallLog.CoopStoreId = request.CoopStoreId;
            posCallLog.HostName = request.HostName;
            posCallLog.IpAddress = request.IpAddress;
            posCallLog.PackageVersionId = request.PackageVersionId;
            posCallLog.PosManufacturerName = request.PosManufacturerName;
            posCallLog.PosNumber = request.PosNumber;
            posCallLog.PosVersion = request.PosVersion;
            posCallLog.TerminalSerialNumber = request.TerminalSerialNumber;
            var posCallLogId = _posCallLogDao.Save(posCallLog);

            if (posCallLog.PosAssemblyLogs != null)
            {
                foreach (var assembly in request.AssemblyInfos)
                {
                    _posAssemblyLogDao.Save(new PosAssemblyLog { AssemblyName = assembly.AssemblyName, AssemblyVersion = assembly.AssemblyVersion, PosCallLogId = posCallLogId});
                }                
            }
        }

        /// <summary>
        /// Checks in database if new packageversion is available
        /// </summary>
        /// <param name="currentPackageVerionId">current package version id</param>
        /// <returns>package if available; otherwise null</returns>
        private IPackageVersion LookupLatestPackageVersion(int currentPackageVerionId)
        {
            Log.Debug(String.Format(" CurrentPackageVerionId: {0}", currentPackageVerionId));
            var latestPackageVersion = _packageVersionDao.GetLatestPackageVersion();
            return latestPackageVersion.Id > currentPackageVerionId ? latestPackageVersion : null;
        }

        /// <summary>
        /// Fetches list of groups to which this package can be deployed
        /// </summary>
        /// <param name="packageVersionId">Package version id</param>
        /// <returns>List of groups</returns>
        private IList<IPackageGroup> LookupPackageGroupsByPackageVersionId(int packageVersionId)
        {
            return _packageGroupDao.GetAllByPackageVersionId(packageVersionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coopStoreId"></param>
        /// <returns></returns>
        private IList<IGroupStore> LookupGroupStoresByStoreId(int coopStoreId)
        {
            Log.Debug(String.Format(" CoopStoreId: {0}", coopStoreId));
            return _groupStoreDao.GetAllByCoopStoreId(coopStoreId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string CalculateChecksum(string file)
        {
            using (var stream = File.OpenRead(file))
            {
                var checksum = Md5.ComputeHash(stream);
                return (BitConverter.ToString(checksum).Replace("-", string.Empty));
            } // End of using fileStream
        } // End of CalculateChecksum 
    }
}
