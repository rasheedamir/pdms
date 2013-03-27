using System.Collections.Generic;
using NHibernate.Criterion;
using PackageDistributionService.Core.DataInterfaces;
using PackageDistributionService.Core.Domain;
using ProjectBase.Data.Dao;
using Spring.Stereotype;

namespace PackageDistributionService.Data.DaoImplementations
{
    /// <summary>
    /// Concrete DAO for accessing instances of <see cref="PackageGroup" /> from DB.
    /// </summary>
    [Repository]
    public class PackageGroupDao : AbstractNHibernateDao<IPackageGroup, int>, IPackageGroupDao
    {
        /// <summary>
        /// Get all by package version id
        /// </summary>
        /// <param name="packageVersionId"></param>
        /// <returns></returns>
        public IList<IPackageGroup> GetAllByPackageVersionId(int packageVersionId)
        {
            var criterion = new List<ICriterion>
                {
                    Restrictions.Eq("PackageVersionId", packageVersionId),
                };
            return GetByCriteria(criterion.ToArray());
        }
    }
}
