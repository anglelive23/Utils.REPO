namespace Utils.REPO.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        #region Fields and Properties
        private readonly DbContext _context;
        #endregion

        #region Constructors
        public GenericRepo(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }
        #endregion

        #region GET
        /// <summary>
        /// Retrieves all elements from a table without including navigation properties, paging, or ordering.
        /// </summary>
        /// <returns>An IQueryable representing all elements in the table.</returns>
        public IQueryable<T> GetAll()
        {
            return _context
                .Set<T>()
                .AsNoTracking();
        }

        /// <summary>
        /// Retrieves a queryable collection of elements from a table with optional filtering, inclusion of navigation properties, and ordering.
        /// </summary>
        /// <param name="predicate">Optional predicate for filtering the results.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional enum indicating ordering direction.</param>
        /// <returns>An IQueryable representing the collection of elements with optional filtering, navigation properties, and ordering applied.</returns>
        public IQueryable<T> GetAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            if (includeNavigationProperties != null)
                query = includeNavigationProperties(query);

            return query.AsNoTracking();
        }

        /// <summary>
        /// Retrieves a queryable collection of elements from a table with optional navigation property inclusion and ordering.
        /// </summary>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional string indicating ordering direction ('Ascending' or 'Descending').</param>
        /// <returns>An IQueryable representing the collection of elements with optional navigation properties and ordering applied.</returns>
        public IQueryable<T> GetAll(
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            if (includeNavigationProperties != null)
                query = includeNavigationProperties(query);

            return query.AsNoTracking();
        }

        /// <summary>
        /// Retrieves a queryable collection of elements from a table with optional navigation property inclusion, paging, and ordering.
        /// </summary>
        /// <param name="pageNumber">Page number for paging.</param>
        /// <param name="pageSize">Number of elements per page for paging.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional string indicating ordering direction ('Ascending' or 'Descending').</param>
        /// <returns>An IQueryable representing the filtered, paged, and ordered collection of elements.</returns>
        public IQueryable<T> GetAll(
            int pageNumber = 0,
            int pageSize = 0,
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            if (includeNavigationProperties != null)
                query = includeNavigationProperties(query);

            if (pageNumber > 0 && pageSize > 0)
            {
                query = query.Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize);
            }

            return query.AsNoTracking();
        }
        #endregion

        #region Search
        /// <summary>
        /// Retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity to retrieve.</param>
        /// <returns>The entity with the specified primary key, or null if not found.</returns>
        public T? FindById(int id)
        {
            return _context.Find<T>(id);
        }

        /// <summary>
        /// Asynchronously retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the entity with the specified primary key,
        /// or null if not found.
        /// </returns>
        public async Task<T?> FindByIdAsync(int id)
        {
            return await _context
                .FindAsync<T>(id);
        }

        /// <summary>
        /// Retrieves a single entity from a table based on the specified predicate, with optional inclusion of navigation properties.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entity.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <returns>The entity matching the predicate, or null if not found.</returns>
        public T? Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includeNavigationProperties != null)
                query = includeNavigationProperties(query);

            return query.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Asynchronously retrieves a single entity from a table based on the specified predicate, with optional inclusion of navigation properties.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entity.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity matching the predicate, or null if not found.</returns>
        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null)
        {

            IQueryable<T> query = _context.Set<T>();

            if (includeNavigationProperties != null)
                query = includeNavigationProperties(query);

            return await query.FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// Checks if any entities in the database context satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">A predicate to match entities against.</param>
        /// <returns>True if any matching entity exists, otherwise false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the predicate is null.</exception>
        /// <exception cref="DataAccessErrorException">Thrown when an error occurs during database access.</exception>
        public bool AnyMatching(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate), "predicate cannot be null!");
            try
            {
                return _context.Set<T>()
                    .Any(predicate);
            }
            catch (Exception ex)
            {
                throw new DataAccessErrorException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        /// <summary>
        /// Asynchronously checks if any entities in the database context satisfy the specified predicate.
        /// </summary>
        /// <param name="predicate">A predicate to match entities against.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result is true if any matching entity exists, otherwise false.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when the predicate is null.</exception>
        /// <exception cref="DataAccessErrorException">Thrown when an error occurs during database access.</exception>
        public async Task<bool> AnyMatchingAsync(Expression<Func<T, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate), "predicate cannot be null!");
            try
            {
                return await _context.Set<T>()
                    .AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                throw new DataAccessErrorException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        #endregion

        #region Filter
        /// <summary>
        /// Filters and customizes a queryable collection of elements from a table based on the specified predicate,
        /// with optional inclusion of navigation properties and ordering.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the collection.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional enum indicating ordering direction.</param>
        /// <returns>An IQueryable representing the filtered and customized collection of elements.</returns>
        public IQueryable<T> Filter(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();

            query = query.Where(predicate);

            if (includeNavigationProperties != null)
                query = includeNavigationProperties(query);

            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.AsNoTracking();
        }

        /// <summary>
        /// [Obsolete] Asynchronously filters and customizes a collection of elements from a table based on the specified predicate,
        /// with optional inclusion of related entities and ordering.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the collection.</param>
        /// <param name="includes">[Obsolete] An array of navigation property names to include in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional enum indicating ordering direction.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a collection of filtered and customized elements.
        /// </returns>
        [Obsolete("This method is obsolete. Use the newer Filter method for filtering and customization.")]
        public async Task<IEnumerable<T>> FilterAsync(
            Expression<Func<T, bool>> predicate,
            string[]? includes = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending)
        {
            IQueryable<T> query = _context.Set<T>();
            // Filtering
            query = query.Where(predicate);
            // Including
            if (includes != null)
                foreach (string include in includes)
                    query = query.Include(include);
            // Ordering
            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            // Returning data
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Post
        public async Task<T> PostAsync(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null!");

            try
            {
                await _context.Set<T>().AddAsync(entity);
                return entity;
            }
            catch (Exception ex) when (ex is DbUpdateException
                                    || ex is InvalidOperationException
                                    || ex is Exception)
            {
                throw new DataAccessErrorException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        #endregion

        #region Delete
        public void Delete(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null!");

            try
            {
                _context.Set<T>().Remove(entity);
            }
            catch (Exception ex) when (ex is DbUpdateException
                                    || ex is InvalidOperationException
                                    || ex is Exception)
            {
                throw new DataAccessErrorException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }

        }
        #endregion

        #region Attach
        public void Attach(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null!");
            try
            {
                _context.Set<T>().Attach(entity);
            }
            catch (Exception ex) when (ex is DbUpdateException
                                    || ex is InvalidOperationException
                                    || ex is Exception)
            {
                throw new DataAccessErrorException(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }
        #endregion

        #region Count
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
