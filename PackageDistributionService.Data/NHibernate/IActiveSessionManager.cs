using NHibernate;

namespace PackageDistributionService.Data.NHibernate
{
    public interface IActiveSessionManager
    {
        ISession GetActiveSession();
    }
}
