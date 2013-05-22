using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;

namespace PackageDistributionService.Core.DataInterfaces
{
    /// <summary>
    /// PosCallLog related DAO operations interface
    /// </summary>
    public interface IPosCallLogDao : IDao<PosCallLog, int>, ISupportsSaveDao<PosCallLog, int>, ISupportsCriteriaDao<PosCallLog>, ISupportsDeleteDao<PosCallLog>
    {
    }
}
