
namespace PackageDistributionService.Core.Dto
{
    /// <summary>
    /// Pos Assembly Information
    /// </summary>
    public interface IAssemblyInfo
    {
        /// <summary>
        /// Assembly Name
        /// </summary>
        string AssemblyName { get; set; }

        /// <summary>
        /// Assembly Version
        /// </summary>
        string AssemblyVersion { get; set; }
    }
}
