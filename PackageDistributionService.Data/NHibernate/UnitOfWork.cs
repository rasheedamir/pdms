using System;
using System.Data;
using NHibernate;

namespace PackageDistributionService.Data.NHibernate
{
    /// <summary>
    /// A simple Unit of Work wrapper. It is IDisposable. Call <see cref="Commit"/> or call <see cref="Rollback"/> to abort the transaction.
    /// </summary>
    public sealed class UnitOfWork : IDisposable
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionManager"></param>
        public UnitOfWork(IActiveSessionManager sessionManager)
        {
            _session = sessionManager.GetActiveSession(); //this may be an already open session...
            _session.FlushMode = FlushMode.Auto; //default
        }

        /// <summary>
        /// /
        /// </summary>
        public ISession Current
        {
            get { return _session; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void CreateTransaction()
        {
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);            
        }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        public void Commit()
        {
            //becuase flushMode is auto, this will automatically commit when disposed
            if (!_transaction.IsActive)
            { 
                throw new InvalidOperationException("No active transaction");
            }
            _transaction.Commit();
            //start a new transaction
            _transaction = _session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// Rolls back this instance. You should probably close session.
        /// </summary>
        public void Rollback()
        {
            if (_transaction.IsActive) _transaction.Rollback();
        }

        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_session != null) _session.Close();
        }

        #endregion
    }
}
