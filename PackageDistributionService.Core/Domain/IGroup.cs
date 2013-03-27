using System;
using System.Collections.Generic;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Interface for Group entity data.
    /// </summary>
    public interface IGroup : IEntityWithTypedId<int>
    {
        /// <summary>
        /// group_id : primary key
        /// </summary>
        //int GroupId { get; set; }

        /// <summary>
        /// group name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// time when this group was created
        /// </summary>
        DateTime? CreationTimestamp { get; set; }

        /// <summary>
        /// list of stores that belongs to this group
        /// </summary>
        IList<IGroupStore> GroupStores { get; set; }

        /// <summary>
        /// list of packages that have been deployed to this group
        /// </summary>
        IList<IPackageGroup> PackageGroups { get; set; } 
    }
}
