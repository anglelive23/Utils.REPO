namespace Utils.REPO
{
    public interface IGenericRepo<T> where T : class
    {
        #region Get
        /// <summary>
        /// Retrieves all elements from a table without including navigation properties, paging, or ordering.
        /// </summary>
        /// <returns>An IQueryable representing all elements in the table.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Retrieves a queryable collection of elements from a table with optional filtering, inclusion of navigation properties, and ordering.
        /// </summary>
        /// <param name="predicate">Optional predicate for filtering the results.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional enum indicating ordering direction.</param>
        /// <returns>An IQueryable representing the collection of elements with optional filtering, navigation properties, and ordering applied.</returns>
        IQueryable<T> GetAll(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending);


        /// <summary>
        /// Retrieves a queryable collection of elements from a table with optional navigation property inclusion and ordering.
        /// </summary>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional string indicating ordering direction ('Ascending' or 'Descending').</param>
        /// <returns>An IQueryable representing the collection of elements with optional navigation properties and ordering applied.</returns>
        IQueryable<T> GetAll(
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending);


        /// <summary>
        /// Retrieves a queryable collection of elements from a table with optional navigation property inclusion, paging, and ordering.
        /// </summary>
        /// <param name="pageNumber">Page number for paging.</param>
        /// <param name="pageSize">Number of elements per page for paging.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional string indicating ordering direction ('Ascending' or 'Descending').</param>
        /// <returns>An IQueryable representing the filtered, paged, and ordered collection of elements.</returns>
        IQueryable<T> GetAll(
            int pageNumber = 0,
            int pageSize = 0,
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending);
        #endregion

        #region Search
        /// <summary>
        /// Retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity to retrieve.</param>
        /// <returns>The entity with the specified primary key, or null if not found.</returns>
        T? FindById(int id);

        /// <summary>
        /// Asynchronously retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity to retrieve.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the entity with the specified primary key,
        /// or null if not found.
        /// </returns>
        Task<T?> FindByIdAsync(int id);

        /// <summary>
        /// Retrieves a single entity from a table based on the specified predicate, with optional inclusion of navigation properties.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entity.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <returns>The entity matching the predicate, or null if not found.</returns>
        T? Find(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null);

        /// <summary>
        /// Asynchronously retrieves a single entity from a table based on the specified predicate, with optional inclusion of navigation properties.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the entity.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity matching the predicate, or null if not found.</returns>
        Task<T?> FindAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null);
        #endregion

        #region Filtering
        /// <summary>
        /// Filters and customizes a queryable collection of elements from a table based on the specified predicate,
        /// with optional inclusion of navigation properties and ordering.
        /// </summary>
        /// <param name="predicate">The predicate used to filter the collection.</param>
        /// <param name="includeNavigationProperties">Optional delegate to include navigation properties in the query.</param>
        /// <param name="orderBy">Optional expression to specify ordering.</param>
        /// <param name="orderByDirection">Optional enum indicating ordering direction.</param>
        /// <returns>An IQueryable representing the filtered and customized collection of elements.</returns>
        IQueryable<T> Filter(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<T>>? includeNavigationProperties = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy? orderByDirection = OrderBy.Ascending);

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
        Task<IEnumerable<T>> FilterAsync(Expression<Func<T, bool>> predicate, string[]? includes = null, Expression<Func<T, object>>? orderBy = null, OrderBy? orderByDirection = OrderBy.Ascending);
        #endregion

        #region Post
        Task<T> PostAsync(T entity);
        #endregion

        #region Delete
        void Delete(T entity);
        #endregion

        #region Attach
        void Attach(T entity);
        #endregion

        #region Count
        int Count();
        Task<int> CountAsync();
        #endregion
    }
}