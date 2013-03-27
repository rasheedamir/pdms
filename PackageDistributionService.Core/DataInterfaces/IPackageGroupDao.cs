using System.Collections.Generic;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PackageGroup related DAO operations interface
    /// </summary>
    public interface IPackageGroupDao : IDao<IPackageGroup, int>, ISupportsSaveDao<IPackageGroup, int>, ISupportsCriteriaDao<IPackageGroup>, ISupportsDeleteDao<IPackageGroup>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageVersionId"></param>
        /// <returns></returns>
        IList<IPackageGroup> GetAllByPackageVersionId(int packageVersionId);
    }
}
