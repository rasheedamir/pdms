using ProjectBase.Data.Domain;
using System;
using System.Collections.Generic;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// Group entity
    /// </summary>
    public class Group : Entity, IGroup
    {
        #region Constructors

        public Group()
        {
            _packageGroups = new List<IPackageGroup>();
            _groupStores = new List<IGroupStore>();
        }

        #endregion

        #region Members

        private string _name;
        private DateTime? _creationTimestamp;
        private IList<IGroupStore> _groupStores;
        private IList<IPackageGroup> _packageGroups;

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
        /// time when this group was created
        /// </summary>
        public DateTime? CreationTimestamp
        {
            get { return _creationTimestamp; }
            set { _creationTimestamp = value; }
        }

        /// <summary>
        /// list of stores that belongs to this group
        /// </summary>
        public IList<IGroupStore> GroupStores
        {
            get { return _groupStores; }
            set { _groupStores = value; }
        }

        /// <summary>
        /// list of packages that have been deployed to this group
        /// </summary>
        public IList<IPackageGroup> PackageGroups
        {
            get { return _packageGroups; }
            set { _packageGroups = value; }

        }
        #endregion

        #region Methods
        #endregion
    }
}
