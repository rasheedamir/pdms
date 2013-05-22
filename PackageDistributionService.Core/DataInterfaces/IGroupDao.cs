using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// Group related DAO operations interface
    /// </summary>
    public interface IGroupDao : IDao<Group, int>, ISupportsSaveDao<Group, int>, ISupportsCriteriaDao<Group>, ISupportsDeleteDao<Group>
    {
    }
}
