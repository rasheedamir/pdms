using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PackageDistributionService.Core.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class PosRequest : BaseDto //, IPosRequest
    {
        #region Constructors
        #endregion

        #region Members

        private int _coopStoreId;
        private string _posNumber;
        private int _packageVersionId;
        private string _terminalSerialNumber;
        private string _ipAddress;
        private string _hostName;
        private string _posManufacturerName;
        private string _posVersion;
        private List<AssemblyInfo> _assemblyInfos;
        
        #endregion

        #region Properties

        [DataMember(IsRequired = true)]
        public int CoopStoreId
        {
            get { return _coopStoreId; }
            set { _coopStoreId = value; }
        }

        [DataMember(IsRequired = true)]
        public string PosNumber
        {
            get { return _posNumber; }
            set { _posNumber = value; }
        }

        [DataMember(IsRequired = true)]
        public int PackageVersionId
        {
            get { return _packageVersionId; }
            set { _packageVersionId = value; }
        }

        [DataMember(IsRequired = true)]
        public string TerminalSerialNumber
        {
            get { return _terminalSerialNumber; }
            set { _terminalSerialNumber = value; }
        }

        [DataMember(IsRequired = true)]
        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        [DataMember(IsRequired = true)]
        public string HostName
        {
            get { return _hostName; }
            set { _hostName = value; }
        }

        [DataMember(IsRequired = true)]
        public string PosManufacturerName
        {
            get { return _posManufacturerName; }
            set { _posManufacturerName = value; }
        }

        [DataMember(IsRequired = true)]
        public string PosVersion
        {
            get { return _posVersion; }
            set { _posVersion = value; }
        }

        [DataMember(IsRequired = true)]
        public List<AssemblyInfo> AssemblyInfos
        {
            get { return _assemblyInfos; }
            set { _assemblyInfos = value; }
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Mini version of ToString to use for logging
        /// </summary>
        /// <returns></returns>
        public string ToStringMini()
        {
            var sb = new StringBuilder();

            sb.Append(" CoopStoreId: ");
            sb.Append(_coopStoreId);
            sb.Append(" | ");
            sb.Append(" PosNumber: ");
            sb.Append(_posNumber);

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(base.ToString());
            foreach (var assembly in AssemblyInfos)
            {
                sb.Append(assembly);
            }
            return sb.ToString();
        }

        #endregion
    }
}
