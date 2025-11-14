using Dutch_Treat.Data.Interfaces;

namespace Dutch_Treat.Data.Repositories.Helpers
{
    /**
     * RepositoryProvider: Factory for Repositories

        RepositoryProvider holds a cache of created repositories.

        Uses RepositoryFactories to know how to create each repository.

        Ensures single instance per type per UnitOfWork.

        Acts as a bridge between UnitOfWork and actual repositories.
     */
    public class RepositoryProvider : IRepositoryProvider
    {
        private RepositoryFactories _repositoryFactories;

        public Dictionary<Type, object> Repositories { get; private set; }

        public RepositoryProvider(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _repositoryFactories = new RepositoryFactories(loggerFactory);
            Repositories = new Dictionary<Type, object>();       
            DbContext = dbContext;
        }

        public ApplicationDbContext DbContext { get; set; }
        public IDutchRepository<T>? GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IDutchRepository<T>>(
                    _repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        public T GetRepository<T>(Func<ApplicationDbContext, object>? factory = null) where T : class
        {
            object repoObj;
            Repositories.TryGetValue(typeof(T), out repoObj);
            if (repoObj != null)
            {
                return (T)repoObj;
            }
            return MakeRepository<T>(factory, DbContext);
        }

        private T MakeRepository<T>(Func<ApplicationDbContext, object>? factory, ApplicationDbContext dbContext) where T : class
        {
            var f = factory ?? _repositoryFactories.GetRepositoryFactory<T>();
            if (f == null)
            {
                throw new Exception("No factory for repository type, " + typeof(T).FullName);
            }
            var repo = (T)f(dbContext);
            Repositories[typeof(T)] = repo;
            return repo;
        }

        public void SetRepository<T>(T repository) where T : class
        {
            Repositories[typeof(T)] = repository;
        }

    }
}
