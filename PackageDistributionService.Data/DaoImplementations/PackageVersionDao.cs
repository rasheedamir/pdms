using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;
using Spring.Stereotype;

namespace PackageDistributionService.Data.DaoImplementations
{
    /// <summary>
    /// Concrete DAO for accessing instances of <see cref="PackageVersion" /> from DB.
    /// </summary>
    [Repository]
    public class PackageVersionDao : AbstractNHibernateDao<PackageVersion, int>, IPackageVersionDao //HibernateDao
    {
        /// <summary>
        /// Gets the latest version package
        /// http://stackoverflow.com/questions/2150135/nhibernate-select-most-recent-record-that-meets-criteria
        /// </summary>
        /// <returns></returns>
        public PackageVersion GetLatestPackageVersion()
        {
            var cr = CurrentSession.CreateCriteria<PackageVersion>();
            cr.AddOrder(Order.Desc("Id")).SetMaxResults(1);
            PackageVersion packageVersion = (PackageVersion)cr.UniqueResult();
            return packageVersion;
        }
    }
}
