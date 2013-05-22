using System;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    public class PackageGroup : Entity
    {
        #region Constructors
        #endregion

        #region Members

        private PackageVersion _packageVersion;
        private Group _group;
        private int _packageVersionId;
        private int _groupId;
        private DateTime? _activationTimestamp;

        #endregion

        #region Properties

        /// <summary>
        /// package_group_id - primary key
        /// </summary>
        /*[DomainSignature]
        public int PackageGroupId
        {
            get { return Id; }
            set { Id = value; }
        }*/

        /// <summary>
        /// package_version entity
        /// </summary>
        public PackageVersion PackageVersion
        {
            get { return _packageVersion; }
            set { _packageVersion = value; }
        }

        /// <summary>
        /// group entity
        /// </summary>
        public Group Group
        {
            get { return _group; }
            set { _group = value; }
        }

        /// <summary>
        /// package_version_id from package_version entity
        /// </summary>
        public int PackageVersionId
        {
            get { return _packageVersionId; }
            set { _packageVersionId = value; }
        }

        /// <summary>
        /// group_id from group entity
        /// </summary>
        public int GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        /// <summary>
        /// timestamp when the package will be enabled on the pos
        /// </summary>
        public DateTime? ActivationTimestamp
        {
            get { return _activationTimestamp; }
            set { _activationTimestamp = value; }
        }
        #endregion

        #region Methods
        #endregion
    }
}
