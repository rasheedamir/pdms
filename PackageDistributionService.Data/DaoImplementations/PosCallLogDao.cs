using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;
using Spring.Stereotype;
using PackageDistributionService.Core.DataInterfaces;

namespace PackageDistributionService.Data.DaoImplementations
{
    /// <summary>
    /// Concrete DAO for accessing instances of <see cref="PosCallLog" /> from DB.
    /// </summary>
    [Repository]
    public class PosCallLogDao : AbstractNHibernateDao<PosCallLog, int>, IPosCallLogDao
    {        
    }
}
