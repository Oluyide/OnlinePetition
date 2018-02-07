using System.Collections.Generic;
using System.Data;
using System.Linq;
using BusinessLogic.Entities;
using NHibernate;

namespace BusinessLogic.Interfaces
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T, TId>
        where T : BaseEntity<TId>
        where TId : struct
    {
        T GetById(object id);
        void SaveOrUpdate(T entity);
        void StartTransaction();
        void RollBackTransaction();
        void CommitTransaction();
        void Flush();
        void Evict(T entity);
        ISession CurrentSession { get; }
        void Delete(T entity);
        void Refresh(T entity);
        IQueryable<T> Table { get; }

        IList<T> ExecuteStoredProcedureSelect(string StoreProcedureName, params object[] parameters);

        void ExecuteStoredProcedureUpdate(string StoredName, params object[] parameters);

        DataTable ExecuteStoredProcedureSelectDataTable(string StoreProcedureName, params object[] parameters);
    }
}
