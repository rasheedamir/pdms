using System.Runtime.Serialization;

namespace PackageDistributionService.Core.Dto
{
    /// <summary>
    /// Pos assembly information
    /// </summary>
    [DataContract]
    public class AssemblyInfo : BaseDto
    {
        #region Constructors
        #endregion

        #region Members

        private string _assemblyName;
        private string _assemblyVersion;
        
        #endregion

        #region Properties

        [DataMember(IsRequired = true)]
        public string AssemblyName
        {
            get { return _assemblyName; }
            set { _assemblyName = value; }
        }

        [DataMember(IsRequired = true)]
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
