using Dutch_Treat.Data.Interfaces;
using DutchTreat.Data;
using Microsoft.EntityFrameworkCore;

namespace Dutch_Treat.Data.Repositories
{
    public class DutchGenericRepository<T> : IDutchRepository<T> where T : class
    {
        internal readonly ILogger<DutchGenericRepository<T>> _logger;
        internal readonly ApplicationDbContext _context;
        internal readonly DbSet<T> _dbSet;

        public DutchGenericRepository(ApplicationDbContext db, ILogger<DutchGenericRepository<T>> logger)  //Constructor receives dependencies via DI 
        {
            _logger = logger;

            //The database context is defined in a class variable,
            //and the constructor expects the calling object to pass in an instance of the context
            _context = db; 
            _dbSet = _context.Set<T>(); //Gets the DbSet for entity type T
        }
        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                _logger.LogInformation("GetAll was called...");

                return _dbSet.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return null;
            }
        }

        public T GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void SaveAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
