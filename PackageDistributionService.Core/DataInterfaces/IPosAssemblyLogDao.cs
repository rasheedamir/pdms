using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PosAssemblyLogDao related DAO operations interface
    /// </summary>
    public interface IPosAssemblyLogDao : IDao<IPosAssemblyLog, int>, ISupportsSaveDao<IPosAssemblyLog, int>, ISupportsCriteriaDao<IPosAssemblyLog>, ISupportsDeleteDao<IPosAssemblyLog>
    {
    }
}
