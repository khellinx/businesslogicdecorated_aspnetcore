using Digipolis.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.BusinessLogicDecorated.SampleApi.DataAccess
{
    public class UnitOfWorkScope : IUnitOfWorkScope
    {
        private DbContext _context;
        private IUnitOfWork _unitOfWork;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWorkScope(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new NullReferenceException(nameof(serviceProvider));

            _serviceProvider = serviceProvider;
        }

        public IUnitOfWork GetUnitOfWork(bool trackChanges = true)
        {
            if (_unitOfWork == null)
            {
                _context = (DbContext)_serviceProvider.GetService(typeof(EntityContext));

                if (!trackChanges)
                    _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                _unitOfWork = new Digipolis.DataAccess.Uow.UnitOfWork(_context, _serviceProvider);
            }
            else
            {
                if (trackChanges)
                    _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            }

            return _unitOfWork;
        }

        #region IDisposable Implementation

        protected bool _isDisposed;

        protected void CheckDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException("The UnitOfWorkScope is already disposed and cannot be used anymore.");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_unitOfWork != null)
                    {
                        _unitOfWork.Dispose();
                        _unitOfWork = null;
                    }
                }
            }
            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWorkScope()
        {
            Dispose(false);
        }

        #endregion
    }
}
