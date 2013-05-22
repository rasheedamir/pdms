using System.Collections.Generic;
using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// PosCallLog
    /// </summary>
    public class PosCallLog : Entity
    {
        #region Constructors

        public PosCallLog()
        {
            _posAssemblyLogs = new List<PosAssemblyLog>();
        }

        #endregion

        #region Members

        private int _coopStoreId;
        private string _posNumber;
        private PackageVersion _packageVersion;
        private int _packageVersionId;
        private string _terminalSerialNumber;
        private string _ipAddress;
        private string _hostName;
        private string _posManufactureName;
        private string _posVersion;
        private IList<PosAssemblyLog> _posAssemblyLogs;

        #endregion

        #region Properties

        /// <summary>
        /// pos_call_log_id : primary key
        /// </summary>
        /*[DomainSignature]
        public int PosCallLogId
        {
            get { return Id; }
            set { Id = value; }
        }*/

        /// <summary>
        /// coop_store_id - unique id of the the coop store
        /// </summary>
        public int CoopStoreId
        {
            get { return _coopStoreId; }
            set { _coopStoreId = value; }
        }

        /// <summary>
        /// pos_number - point of sales number
        /// </summary>
        public string PosNumber
        {
            get { return _posNumber; }
            set { _posNumber = value; }
        }

        /// <summary>
        /// PackageVersion entity
        /// </summary>
        public PackageVersion PackageVersion
        {
            get { return _packageVersion; }
            set { _packageVersion = value; }
        }

        /// <summary>
        /// package_version_id of PackageVersion entity
        /// </summary>
        public int PackageVersionId
        {
            get { return _packageVersionId; }
            set { _packageVersionId = value; }
        }

        /// <summary>
        /// terminal_serial_number - a store can have multiple pos
        /// </summary>
        public string TerminalSerialNumber
        {
            get { return _terminalSerialNumber; }
            set { _terminalSerialNumber = value; }
        }

        /// <summary>
        /// ip_address - of the pos
        /// </summary>
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        /// <summary>
        /// host_nanem - of the pos
        /// </summary>
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        /// <summary>
        /// pos_manufacturer_name
        /// </summary>
        public string PosManufacturerName
        {
            get { return _posManufactureName; }
            set { _posManufactureName = value; }
        }

        /// <summary>
        /// pos_verion
        /// </summary>
        public string PosVersion
        {
            get { return _posVersion; }
            set { _posVersion = value; }
        }

        /// <summary>
        /// list of assemblies
        /// </summary>
        public IList<PosAssemblyLog> PosAssemblyLogs
        {
            get { return _posAssemblyLogs; }
            set { _posAssemblyLogs = value; }
        }
        #endregion

        #region Methods
        #endregion

    }
}
