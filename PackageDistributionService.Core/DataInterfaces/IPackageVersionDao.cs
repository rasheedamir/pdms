using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PackageVersion related DAO operations interface
    /// </summary>
    public interface IPackageVersionDao : IDao<PackageVersion, int>, ISupportsSaveDao<PackageVersion, int>, ISupportsCriteriaDao<PackageVersion>, ISupportsDeleteDao<PackageVersion>
    {
        /// <summary>
        /// Returns the latest package version
        /// </summary>
        /// <returns></returns>
        PackageVersion GetLatestPackageVersion();
    }
}
