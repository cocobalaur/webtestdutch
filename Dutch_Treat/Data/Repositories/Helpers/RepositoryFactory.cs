using Dutch_Treat.Data.Interfaces;
using DutchTreat.Data.Entities;

namespace Dutch_Treat.Data.Repositories.Helpers
{
    /**
     * RepositoryFactories: Decides Which Repository to Create
     * Maps entity types to specific repositories:

       Product → DutchProductRepository

       Order → DutchOrderRepository

       If no specific repository exists, it falls back to GenericRepository.
     */
    public class RepositoryFactories
    {
        private ILoggerFactory _loggerFactory;
        private readonly IDictionary<Type, Func<ApplicationDbContext, object>> _repositoryFactories;

        private IDictionary<Type, Func<ApplicationDbContext, object>> GetDucthFactories()
        {
            return new Dictionary<Type, Func<ApplicationDbContext, object>>
            {
                {typeof(IDutchRepository<Product>), 
                    dbContext => new DutchProductRepository(dbContext, 
                                        new Logger<DutchProductRepository>(_loggerFactory)) },
                {typeof(IDutchRepository<Order>), 
                    dbContext => new DutchOrderRepository(dbContext, 
                                        new Logger<DutchOrderRepository>(_loggerFactory)) },
                {typeof(IDutchRepository<OrderItem>), 
                    dbContext => new DutchOrderItemRepository(dbContext, 
                                        new Logger<DutchOrderItemRepository>(_loggerFactory)) }

            };
        }
        public RepositoryFactories(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _repositoryFactories = GetDucthFactories();
        }

        public Func<ApplicationDbContext, object> GetRepositoryFactory<T>()
        {
            Func<ApplicationDbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        public Func<ApplicationDbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<ApplicationDbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new DutchGenericRepository<T>(dbContext, 
                                        new Logger<DutchGenericRepository<T>>(_loggerFactory));
        }
    }
}
