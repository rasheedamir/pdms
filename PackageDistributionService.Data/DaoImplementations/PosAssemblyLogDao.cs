using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;
using Spring.Stereotype;

namespace PackageDistributionService.Data.DaoImplementations
{
    /// <summary>
    /// Concrete DAO for accessing instances of <see cref="PosAssemblyLog" /> from DB.
    /// </summary>
    [Repository]
    public class PosAssemblyLogDao : AbstractNHibernateDao<IPosAssemblyLog, int>, IPosAssemblyLogDao
    {
    }
}
