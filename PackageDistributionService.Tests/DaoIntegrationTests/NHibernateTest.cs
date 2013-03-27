using NHibernate;
using NUnit.Framework;
using PackageDistributionService.Data.NHibernate;

namespace PackageDistributionService.Tests.DaoIntegrationTests
{
    public abstract class NHibernateTest : AbstractDaoIntegrationTests
    {
        private IActiveSessionManager _activeSessionManager;
        private UnitOfWork _unitOfWork;
        protected ISession Session;

        /// <summary>
        /// Dependency Injected
        /// </summary>
        public IActiveSessionManager ActiveSessionManager
        {
            set { _activeSessionManager = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected virtual IActiveSessionManager GetActiveSessionManager()
        {
            return _activeSessionManager;
        }

        [SetUp]
        public new void SetUp()
        {
            BeforeSetup();
            var sessionManager = GetActiveSessionManager();
            _unitOfWork = new UnitOfWork(sessionManager);
            Session = sessionManager.GetActiveSession();
            _unitOfWork.CreateTransaction();
            AfterSetup();
        }

        [TearDown]
        public new void TearDown()
        {
            BeforeTearDown();
            if (_unitOfWork != null)
            {
                _unitOfWork.Dispose();
            }
            _unitOfWork = null;
            AfterTearDown();
        }

        protected virtual void BeforeSetup() { }
        protected virtual void AfterSetup() { }
        protected virtual void BeforeTearDown() { }
        protected virtual void AfterTearDown() { }
    }
}
