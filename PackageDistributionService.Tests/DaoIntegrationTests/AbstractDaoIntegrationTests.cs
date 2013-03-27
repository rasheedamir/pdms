using NUnit.Framework;
using Spring.Testing.NUnit;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public abstract class AbstractDaoIntegrationTests : AbstractTransactionalDbProviderSpringContextTests
    {
        /// <summary>
        /// 
        /// </summary>
        protected override string[] ConfigLocations
        {
            get
            {
                return new[]
                    {
                        "assembly://PackageDistributionService.Data/PackageDistributionService.Data.DaoImplementations/Dao.xml"
                    };
            }
        }
    }
}
