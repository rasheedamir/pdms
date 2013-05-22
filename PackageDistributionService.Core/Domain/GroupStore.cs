using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// GroupStore entity
    /// </summary>
    public class GroupStore : Entity
    {
        #region Constructors
        #endregion

        #region Members

        private int _groupId;
        private Group _group;
        private int _coopStoreId;

        #endregion

        #region Properties

        /// <summary>
        /// group_store_id : primary key
        /// </summary>
        /*[DomainSignature]
        public int GroupStoreId
        {
            get { return Id; }
            set { Id = value; }
        }*/

        /// <summary>
        /// group entity
        /// </summary>
        public Group Group
        {
            get { return _group; }
            set { _group = value; }
        }

        /// <summary>
        /// group_id of group entity
        /// </summary>
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        /// <summary>
        /// coop_store_id : unique id of coop store
        /// </summary>
        public int CoopStoreId
        {
            get { return _coopStoreId; }
            set { _coopStoreId = value; }
        }

        #endregion

        #region Methods
        #endregion
    }
}
