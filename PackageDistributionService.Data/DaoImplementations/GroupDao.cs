using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;
using Spring.Stereotype;

namespace PackageDistributionService.Data.DaoImplementations
{
    /// <summary>
    /// Concrete DAO for accessing instances of <see cref="Group" /> from DB.
    /// </summary>
    [Repository]
    public class GroupDao : AbstractNHibernateDao<Group, int>, IGroupDao
    {
    }
}