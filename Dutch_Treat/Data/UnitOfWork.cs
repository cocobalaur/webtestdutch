using Dutch_Treat.Data.Interfaces;
using Dutch_Treat.Data.Repositories;
using Dutch_Treat.Data.Repositories.Helpers;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Dutch_Treat.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _context;
        private readonly IRepositoryProvider _repositoryProvider;
        //private ILogger<DutchProductRepository> _loggerProduct;
        //private DutchProductRepository _productRepository;

        //UnitOfWork depends on ApplicationDbContext and IRepositoryProvider.
        // When a service or controller asks for IUnitOfWork, DI creates an instance of UnitOfWork and injects:
        //The current ApplicationDbContext(scoped per request), The RepositoryProvider
        public UnitOfWork(ApplicationDbContext context, IRepositoryProvider provider, ILoggerFactory loggerFactory) 
        {
            //_loggerOrder = new Logger<DutchOrderRepository>(loggerFactory);
            //_loggerProduct = new Logger<DutchProductRepository>(loggerFactory);
            _context = context;
            _repositoryProvider = provider;
        }

        public T GetRepository<T>() where T : class
        {
            return _repositoryProvider.GetRepository<T>();
        }
        //public DutchProductRepository ProductRepository
        //{
        //    get
        //    {
        //        if (this._productRepository == null)
        //        {
        //            this._productRepository = new DutchProductRepository(_context, _loggerProduct);
        //        }
        //        return this._productRepository;
        //    }
        //}

        //private ILogger<DutchOrderRepository> _loggerOrder;
        //private DutchOrderRepository _orderRepository;       
        //public DutchOrderRepository OrderRepository
        //{
        //    get
        //    {
        //        if (this._orderRepository == null)
        //        {
        //            this._orderRepository = new DutchOrderRepository(_context, _loggerOrder);
        //        }
        //        return this._orderRepository;
        //    }
        //}

        //private ILogger<DutchOrderItemRepository> _loggerOrderItem;
        //private DutchOrderItemRepository _orderItemRepository;
        //public DutchOrderItemRepository OrderItemRepository
        //{
        //    get
        //    {
        //        if (this._orderItemRepository == null)
        //        {
        //            this._orderItemRepository = new DutchOrderItemRepository(_context, _loggerOrderItem);
        //        }
        //        return this._orderItemRepository;
        //    }
        //}

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        
    }
}
