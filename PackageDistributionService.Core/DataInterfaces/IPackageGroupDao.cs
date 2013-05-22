using System.Collections.Generic;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PackageGroup related DAO operations interface
    /// </summary>
    public interface IPackageGroupDao : IDao<PackageGroup, int>, ISupportsSaveDao<PackageGroup, int>, ISupportsCriteriaDao<PackageGroup>, ISupportsDeleteDao<PackageGroup>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageVersionId"></param>
        /// <returns></returns>
        IList<PackageGroup> GetAllByPackageVersionId(int packageVersionId);
    }
}
