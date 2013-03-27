using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PackageVersion related DAO operations interface
    /// </summary>
    public interface IPackageVersionDao : IDao<IPackageVersion, int>, ISupportsSaveDao<IPackageVersion, int>, ISupportsCriteriaDao<IPackageVersion>, ISupportsDeleteDao<IPackageVersion>
    {
        /// <summary>
        /// Returns the latest package version
        /// </summary>
        /// <returns></returns>
        IPackageVersion GetLatestPackageVersion();
    }
}
