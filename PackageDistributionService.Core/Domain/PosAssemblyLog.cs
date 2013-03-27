using ProjectBase.Data.Domain;

namespace PackageDistributionService.Core.Domain
{
    /// <summary>
    /// PosAssemblyLog entity
    /// </summary>
    public class PosAssemblyLog : Entity, IPosAssemblyLog
    {
        #region Constructors
        #endregion

        #region Members

        private IPosCallLog _posCallLog;
        private int _posCallLogId;
        private string _assemblyName;
        private string _assemblyVersion;

        #endregion

        #region Properties

        /// <summary>
        /// pos_assembly_log_id : primary key
        /// </summary>
        /*[DomainSignature]
        public int PosAssemblyLogId
        {
            get { return Id; }
            set { Id= value; }
        }*/

        /// <summary>
        /// PosCallLog entity to which this PosAssemblyLog belongs
        /// 1(PosCallLog)-to-M(PosAssemblyLog)
        /// </summary>
        public IPosCallLog PosCallLog
        {
            get { return _posCallLog; }
            set { _posCallLog = value; }
        }

        /// <summary>
        /// pos_call_log_id of PosCallLog entity
        /// </summary>
        public int PosCallLogId
        {
            get { return _posCallLogId; }
            set { _posCallLogId = value; }
        }

        /// <summary>
        /// Assembly Name
        /// </summary>
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        /// <summary>
        /// Assembly Version
        /// </summary>
        public string AssemblyVersion
        {
            get { return _assemblyVersion; }
            set { _assemblyVersion = value; }
        }
        #endregion

        #region Methods
        #endregion
    }
}
