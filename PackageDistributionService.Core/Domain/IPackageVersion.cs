using System;
using System.Collections.Generic;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Interface for PackageVersion entity
    /// </summary>
    public interface IPackageVersion : IEntityWithTypedId<int>
    {
        /// <summary>
        /// version_number of the package
        /// </summary>
        string VersionNumber { get; set; }

        /// <summary>
        /// name of the the package
        /// </summary>
        string PackageName { get; set; }

        /// <summary>
        /// package_path, where the file is stored. both path and name
        /// </summary>
        string PackagePath { get; set; }

        /// <summary>
        /// comment related to the new package
        /// </summary>
        string Comment { get; set; }

        /// <summary>
        /// time when package was loaded into the system
        /// </summary>
        DateTime? CreationTimestamp { get; set; }

        /// <summary>
        /// list of groups to which this package is going to be deployed
        /// </summary>
        IList<IPackageGroup> PackageGroups { get; set; }

        /// <summary>
        /// list of pos call logs on which this package has been deployed
        /// </summary>
        IList<IPosCallLog> PosCallLogs { get; set; }
    }
}
