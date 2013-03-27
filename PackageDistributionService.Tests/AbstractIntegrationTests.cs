using NUnit.Framework;
using Spring.Testing.NUnit;

namespace PackageDistributionService.Tests
{
    /// <summary>
    /// http://www.springframework.net/docs/1.3.0/reference/html/testing.html#testing-ctx-management
    /// Context Management & Caching
    /// http://www.springframework.net/docs/1.3.0/reference/html/testing.html#testing-tx
    /// </summary>
    [TestFixture]
    public abstract class AbstractIntegrationTests : AbstractTransactionalDbProviderSpringContextTests
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
                        "assembly://PackageDistributionService.Service/PackageDistributionService.Service.Services/Services.xml",
                        "assembly://PackageDistributionService.Data/PackageDistributionService.Data.DaoImplementations/Dao.xml"
                    };
            }
        }
    }
}
