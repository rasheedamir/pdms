using System;
using NHibernate;
using PackageDistributionService.Core.Domain;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    public sealed class EntityBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PackageVersion BuildPackageVersion()
        {
            return new PackageVersion
            {
                Comment = "Brand New Package",
                CreationTimestamp = DateTime.Now,
                PackagePath = "C:\\Package\\ABC.zip",
                VersionNumber = "1.0.0.1"
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PackageVersion AddPackageVersion(ISession session)
        {
            var entity = BuildPackageVersion();
            session.Save(entity);
            session.Flush();
            session.Evict(entity);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Group BuildGroup()
        {
            var group = new Group();
            group.Name = "New Group";
            group.CreationTimestamp = DateTime.Now;
            return group;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static Group AddGroup(ISession session)
        {
            var entity = BuildGroup();
            session.Save(entity);
            session.Flush();
            session.Evict(entity);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PosCallLog BuildPosCallLog(ISession session)
        {
            var posCallLog = new PosCallLog
            {
                CallTimestamp = DateTime.Now,
                CoopStoreId = 22,
                HostName = "I am the first host!",
                IpAddress = "123.3.4.5",
                PosManufacturerName = "R2M",
                PosNumber = "123456789",
                PosVersion = "5.0.0.1",
                TerminalSerialNumber = "556667778899"
            };

            posCallLog.PosAssemblyLogs.Add(new PosAssemblyLog { AssemblyName = "I am assembly 1", AssemblyVersion = "7.5.6" });
            posCallLog.PosAssemblyLogs.Add(new PosAssemblyLog { AssemblyName = "I am assembly 2", AssemblyVersion = "9.1.3" });

            var packageVersion = AddPackageVersion(session);
            posCallLog.PackageVersionId = packageVersion.Id;

            return posCallLog;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PosCallLog AddPosCallLog(ISession session)
        {
            var entity = BuildPosCallLog(session);
            session.Save(entity);
            session.Flush();
            session.Evict(entity);
            return entity;            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PackageGroup BuildPackageGroup(ISession session)
        {
            var packageGroup = new PackageGroup { ActivationTimestamp = DateTime.Now };

            var group = AddGroup(session);

            packageGroup.Group = group;
            packageGroup.GroupId = group.Id;

            var packageVersion = AddPackageVersion(session);

            packageGroup.PackageVersion = packageVersion;
            packageGroup.PackageVersionId = packageVersion.Id;

            return packageGroup;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PackageGroup AddPackageGroup(ISession session)
        {
            var entity = BuildPackageGroup(session);
            session.Save(entity);
            session.Flush();
            session.Evict(entity);
            return entity;                        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static GroupStore BuildGroupStore(ISession session)
        {
            var groupStore = new GroupStore { CoopStoreId = 10 };
            var group = AddGroup(session);
            groupStore.GroupId = group.Id;
            return groupStore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static GroupStore AddGroupStore(ISession session)
        {
            var entity = BuildGroupStore(session);
            session.Save(entity);
            session.Flush();
            session.Evict(entity);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PosAssemblyLog BuildPosAssemblyLog(ISession session)
        {
            var posAssemblyLog = new PosAssemblyLog
            {
                AssemblyName = "I am brand new Assembly",
                AssemblyVersion = "3.4.5.6",
            };
            var posCallLog = AddPosCallLog(session);
            posAssemblyLog.PosCallLogId = posCallLog.Id;
            posAssemblyLog.PosCallLog = posCallLog;
            return posAssemblyLog;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static PosAssemblyLog AddPosAssemblyLog(ISession session)
        {
            var entity = BuildPosAssemblyLog(session);
            session.Save(entity);
            session.Flush();
            session.Evict(entity);
            return entity;
        }


    }
}
