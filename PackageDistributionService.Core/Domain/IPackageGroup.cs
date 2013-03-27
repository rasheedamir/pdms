using System;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Interface for PackageGroup entity.
    /// "Package" to be deployed to which "Groups"
    /// </summary>
    public interface IPackageGroup : IEntityWithTypedId<int>
    {
        /// <summary>
        /// package_group_id - primary key
        /// </summary>
        //int PackageGroupId { get; set; }

        /// <summary>
        /// package_version entity
        /// </summary>
        IPackageVersion PackageVersion { get; set; }

        /// <summary>
        /// group entity
        /// </summary>
        IGroup Group { get; set; }

        /// <summary>
        /// package_version_id from package_version entity
        /// </summary>
        int PackageVersionId { get; set; }

        /// <summary>
        /// group_id from group entity
        /// </summary>
        int GroupId { get; set; }

        /// <summary>
        /// timestamp when the package will be enabled on the pos
        /// </summary>
        DateTime? ActivationTimestamp { get; set; }
    }
}
