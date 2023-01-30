using Microsoft.EntityFrameworkCore;

namespace Utils.REPO
{
    public interface IGenericRepo<T> where T : class
    {
        #region CRUD Operations
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        T FindById(int id);
        Task<T> FindByIdAsync(int id);
        T Find(Expression<Func<T, bool>> cretria, string[]? includes = null);
        Task<T> FindAsync(Expression<Func<T, bool>> cretria, string[]? includes = null);
        T FindFirst(Expression<Func<T, bool>> cretria, string[]? includes = null);
        Task<T> FindFirstAsync(Expression<Func<T, bool>> cretria, string[]? includes = null);
        IEnumerable<T> FindRange(Expression<Func<T, bool>> cretria, string[]? includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        Task<IEnumerable<T>> FindRangeAsync(Expression<Func<T, bool>> cretria, string[]? includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);
        T Post(T entity);
        Task<T> PostAsync(T entity);
        void Delete(T entity);
        void Attach(T entity);
        int Count();
        Task<int> CountAsync();
        #endregion
    }
}