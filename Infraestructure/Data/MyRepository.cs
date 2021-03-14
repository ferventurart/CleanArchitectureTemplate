using ApplicationCore.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace Infraestructure.Data
{
    public class MyRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;

        public MyRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
