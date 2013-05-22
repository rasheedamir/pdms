using System.Collections.Generic;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// PackageVersion entity
    /// </summary>
    public class PackageVersion : Entity
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PackageVersion()
        {
            _packageGroups = new List<PackageGroup>();
            _posCallLogs = new List<PosCallLog>();
        }

        #endregion

        #region Members

        private string _versionNumber;
        private string _packagePath;
        private string _comment;
        private IList<PackageGroup> _packageGroups;
        private IList<PosCallLog> _posCallLogs;
        private string _packageName;

        #endregion

        #region Properties

        /// <summary>
        /// package_version_id - primary key
        /// </summary>
        //[DomainSignature]
        //public int PackageVersionId
        //{
        //    get { return Id; }
        //    set { Id = value; }
        //}

        /// <summary>
        /// version_number of the package
        /// </summary>
        public string VersionNumber
        {
            get { return _versionNumber; }
            set { _versionNumber = value; }
        }

        public string PackageName
        {
            get { return _packageName; }
            set { _packageName = value; }
        }

        /// <summary>
        /// package_path, where the file is stored.
        /// </summary>
        public string PackagePath
        {
            get { return _packagePath; }
            set { _packagePath = value; }
        }

        /// <summary>
        /// comment related to the new package
        /// </summary>
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        /// <summary>
        /// list of groups to which this package is going to be deployed
        /// </summary>
        public IList<PackageGroup> PackageGroups
        {
            get { return _packageGroups; }
            set { _packageGroups = value; }
        }

        /// <summary>
        /// list of pos call logs on which this package has been deployed
        /// </summary>
        public IList<PosCallLog> PosCallLogs
        {
            get { return _posCallLogs; }
            set { _posCallLogs = value; }
        }
        #endregion

        #region Methods
        #endregion
    }
}
