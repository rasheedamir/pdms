using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Interface for GroupStore entity.
    /// "Group" contains which "Stores"
    /// </summary>
    public interface IGroupStore : IEntityWithTypedId<int>
    {
        /// <summary>
        /// group_store_id : primary key
        /// </summary>
        //int GroupStoreId { get; set; }

        /// <summary>
        /// group entity
        /// </summary>
        IGroup Group { get; set; }

        /// <summary>
        /// group_id of group entity
        /// </summary>
        int GroupId { get; set; }

        /// <summary>
        /// coop_store_id : unique id of coop store
        /// </summary>
        int CoopStoreId { get; set; }
    }
}
