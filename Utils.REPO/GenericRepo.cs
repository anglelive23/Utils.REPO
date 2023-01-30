namespace Utils.REPO
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        #region Context
        DbContext _context;
        #endregion

        public GenericRepo(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }

        #region CRUD Operations
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }

        public T FindById(int id)
        {
            return _context.Find<T>(id);
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public T Find(Expression<Func<T, bool>> cretria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            return query.SingleOrDefault(cretria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> cretria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            return await query.SingleOrDefaultAsync(cretria);
        }

        public T FindFirst(Expression<Func<T, bool>> cretria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            return query.FirstOrDefault(cretria);
        }

        public async Task<T> FindFirstAsync(Expression<Func<T, bool>> cretria, string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            return await query.FirstOrDefaultAsync(cretria);
        }

        public IEnumerable<T> FindRange(Expression<Func<T, bool>> cretria, string[]? includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            query = query.Where(cretria);
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            return query.ToList();
        }

        public async Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> cretria, string[]? includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending)
        {
            //IQueryable<T> query = _context.Set<T>().Where(cretria);
            IQueryable<T> query = _context.Set<T>();
            // Ordering
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            // Filtering
            query = query.Where(cretria);
            // Including
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            // Returning data
            return await query.ToListAsync();
        }

        public T Post(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }
        public async Task<T> PostAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
        public void Attach(T entity)
        {
            _context.Set<T>().Attach(entity);
        }

        public int Count()
        {
            return _context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        #endregion
    }
}
