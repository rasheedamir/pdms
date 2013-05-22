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
        private static readonly ILog Logger = LogManager.GetLogger(typeof(DistributionService));
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
        public LspPackage GetPackage(LspRequest request)
        {
            var lspPackage = new LspPackage();

            try
            {
                // Step : Log the information in file
                Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Request recieved :{2}", request.PosNumber, request.CoopStoreId, request));

                // Step : Insert data into poscalllog and posassemblylog tables in database
                InsertPosCallLog(request);

                // Step : Lookup if there is a new package version in the database (packageversions) - packageversion
                var packageVersion = LookupLatestPackageVersion(request.PackageVersionId);
                Logger.Debug(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Installed package :{2} and the Latest available package is :{3} ", request.PosNumber, request.CoopStoreId, request.PackageVersionId, packageVersion));
                if (packageVersion == null)
                {
                    lspPackage.Status = 0; // Nothing new.
                    lspPackage.Message = "No new package available.";
                    // Nothing else simply return!
                    Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, lspPackage.Message));
                    return lspPackage;
                }
                // Step : Lookup to which groups this package can be deployed (packagegroups) - groups list
                var packageGroupsList = LookupPackageGroupsByPackageVersionId(packageVersion.Id);
                Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || PackageGroupsList.Count :{2} for PackagVersion.Id :{3}", request.PosNumber, request.CoopStoreId, packageGroupsList.Count, packageVersion.Id));

                if (packageGroupsList.Count > 0)
                {
                    // Step : Lookup if coopstoreid belongs to any of the groups                    
                    var groupStoresList = LookupGroupStoresByStoreId(request.CoopStoreId);
                    Logger.Debug(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || GroupStoresList.Count :{2}", request.PosNumber, request.CoopStoreId, groupStoresList.Count));

                    if (groupStoresList.Count > 0)
                    {
                        foreach (var groupStore in groupStoresList)
                        {
                            var gs = groupStore;
                            var packageGroup =
                                from g in packageGroupsList
                                where g.GroupId == gs.GroupId
                                select g;

                            var packageGroups = packageGroup as PackageGroup[] ?? packageGroup.ToArray();
                            if (packageGroups.FirstOrDefault() == null) continue;
                                
                            lspPackage.Status = 1; // Something new.
                            lspPackage.Message = "New package attached.";
                            lspPackage.ActivationTime = packageGroups.First().ActivationTimestamp; // Activation timestamp
                            lspPackage.PackageVersionId = packageVersion.Id; // Package verison id
                            lspPackage.Filename = packageVersion.PackageName;
                            lspPackage.PackageContents = File.ReadAllBytes(packageVersion.PackagePath);
                            lspPackage.Md5CheckSum = CalculateChecksum(packageVersion.PackagePath);
                            Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, lspPackage.ToStringMini()));
                            return lspPackage;
                        }
                        lspPackage.Status = 0; // Nothing new.
                        lspPackage.Message = "New package available but it hasn't been assigned to any group containing this store.";
                        // Nothing else simply return!
                        Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, lspPackage.Message));
                        return lspPackage;                                                    
                    }
                    lspPackage.Status = 0; // Nothing new.
                    lspPackage.Message = "New package available but store hasn't been assigned to any group.";
                    // Nothing else simply return!
                    Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, lspPackage.Message));
                    return lspPackage;
                }
                lspPackage.Status = 0; // Nothing new.
                lspPackage.Message = "New package available but package hasn't been assigned to any group.";
                // Nothing else simply return!
                Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, lspPackage.Message));
                return lspPackage;
            }
            catch (Exception exception)
            {
                Logger.Error(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Exception :{2}", request.PosNumber, request.CoopStoreId, exception.Message));
                Logger.Error(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Exception :{2}", request.PosNumber, request.CoopStoreId, exception.StackTrace));
                lspPackage.Status = 2; // Exception! Anything went wrong. Contact system admin.
                lspPackage.Message = "Exception occured. Please contact admininstrator!";
                Logger.Info(String.Format("Processing LspPackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, lspPackage.Message));
                return lspPackage;                                                   
            }
        }

        /// <summary>
        /// Inserts point of sales call data into database
        /// </summary>
        /// <param name="request"></param>
        private void InsertPosCallLog(LspRequest request)
        {
            var posCallLog = new PosCallLog
                {
                    DateCreated = DateTime.Now,
                    CoopStoreId = request.CoopStoreId,
                    HostName = request.HostName,
                    IpAddress = request.IpAddress,
                    PackageVersionId = request.PackageVersionId,
                    PosManufacturerName = request.PosManufacturerName,
                    PosNumber = request.PosNumber,
                    PosVersion = request.PosVersion,
                    TerminalSerialNumber = request.TerminalSerialNumber
                };
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
        private PackageVersion LookupLatestPackageVersion(int currentPackageVerionId)
        {
            Logger.Debug(String.Format(" CurrentPackageVerionId: {0}", currentPackageVerionId));
            var latestPackageVersion = _packageVersionDao.GetLatestPackageVersion();
            return latestPackageVersion.Id > currentPackageVerionId ? latestPackageVersion : null;
        }

        /// <summary>
        /// Fetches list of groups to which this package can be deployed
        /// </summary>
        /// <param name="packageVersionId">Package version id</param>
        /// <returns>List of groups</returns>
        private IList<PackageGroup> LookupPackageGroupsByPackageVersionId(int packageVersionId)
        {
            return _packageGroupDao.GetAllByPackageVersionId(packageVersionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coopStoreId"></param>
        /// <returns></returns>
        private IList<GroupStore> LookupGroupStoresByStoreId(int coopStoreId)
        {
            Logger.Debug(String.Format(" CoopStoreId: {0}", coopStoreId));
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FilePackage GetFile(FileRequest request)
        {
            var filePackage = new FilePackage();
            //ToDo: Read from a property file...
            const string filePath = "C:\\LSP\\Files";
            try
            {
                Logger.Info(String.Format("Processing FilePackage request for PosNumber:{0}: StoreId:{1} || Request recieved :{2}", request.PosNumber, request.CoopStoreId, request));
                var completeFilePathAndName = filePath + "\\" + request.Filename;
                filePackage.FileContents = File.ReadAllBytes(completeFilePathAndName);
                filePackage.Md5CheckSum = CalculateChecksum(completeFilePathAndName);
                filePackage.Status = 1; // All went well! File is attached!
                filePackage.Message = "All went well! File is attached!";
                Logger.Info(String.Format("Processing FilePackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, filePackage.Message));
                return filePackage;
            }
            catch (Exception exception)
            {
                Logger.Error(String.Format("Processing FilePackage request for PosNumber:{0}: StoreId:{1} || Exception :{2}", request.PosNumber, request.CoopStoreId, exception.Message));
                Logger.Error(String.Format("Processing FilePackage request for PosNumber:{0}: StoreId:{1} || Exception :{2}", request.PosNumber, request.CoopStoreId, exception.StackTrace));
                filePackage.Status = 0; // Exception occured. Please contact admininstrator!
                filePackage.Message = "Exception occured. Please contact admininstrator!";
                Logger.Info(String.Format("Processing FilePackage request for PosNumber:{0}: StoreId:{1} || Response sent :{2}", request.PosNumber, request.CoopStoreId, filePackage.Message));
                return filePackage;
            }
        }
    }
}
