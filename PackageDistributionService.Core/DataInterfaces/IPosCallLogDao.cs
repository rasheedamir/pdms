using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PosCallLog related DAO operations interface
    /// </summary>
    public interface IPosCallLogDao : IDao<IPosCallLog, int>, ISupportsSaveDao<IPosCallLog, int>, ISupportsCriteriaDao<IPosCallLog>, ISupportsDeleteDao<IPosCallLog>
    {
    }
}
