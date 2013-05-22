using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PosAssemblyLogDao related DAO operations interface
    /// </summary>
    public interface IPosAssemblyLogDao : IDao<PosAssemblyLog, int>, ISupportsSaveDao<PosAssemblyLog, int>, ISupportsCriteriaDao<PosAssemblyLog>, ISupportsDeleteDao<PosAssemblyLog>
    {
    }
}
