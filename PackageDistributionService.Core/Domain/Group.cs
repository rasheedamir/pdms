using ProjectBase.Data.Domain;
using System.Collections.Generic;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Group entity
    /// </summary>
    public class Group : Entity
    {
        #region Constructors

        public Group()
        {
            _packageGroups = new List<PackageGroup>();
            _groupStores = new List<GroupStore>();
        }

        #endregion

        #region Members

        private string _name;
        private IList<GroupStore> _groupStores;
        private IList<PackageGroup> _packageGroups;

        #endregion

        #region Properties

        /// <summary>
        /// id : primary key
        /// </summary>
        /*[DomainSignature]
        public int GroupId
        {
            get { return Id; }
            set { Id = value; }
        }*/

        /// <summary>
        /// group name
        /// </summary>
        public string Name
        {
            get { return  _name; }
            set { _name = value; }
        }

        /// <summary>
        /// list of stores that belongs to this group
        /// </summary>
        public IList<GroupStore> GroupStores
        {
            get { return _groupStores; }
            set { _groupStores = value; }
        }

        /// <summary>
        /// list of packages that have been deployed to this group
        /// </summary>
        public IList<PackageGroup> PackageGroups
        {
            get { return _packageGroups; }
            set { _packageGroups = value; }

        }
        #endregion

        #region Methods
        #endregion
    }
}
