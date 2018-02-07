using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Helper;
using NHibernate;
using System.Data;
using NHibernate.Transform;
using BusinessLogic.Entities;


using BusinessLogic.Interfaces;
using BusinessLogic.Utility;
using BussinessLogic.Infrastuctures;
using BusinessLogic.Helpers;



namespace BusinessLogic.Helper
{
    public partial class NHibernateRepository<T, TId> : IRepository<T, TId>
        where T : BaseEntity<TId>
        where TId : struct
    {
        readonly ISession currentSession = NHibernateHelper.GetCurrentSession();
        public NHibernateRepository()
        {

        }
        public T GetById(object id)
        {

            return currentSession.Get<T>(id);
        }
        public IList<T> ExecuteStoredProcedureSelect(string StoreProcedureName, params object[] parameters)
        {



            List<string> str = new List<string>();
            for (int j = 0; j < parameters.Length; j++)
                str.Add("?");
            var St = String.Join(" , ", str);
            var query = currentSession.CreateSQLQuery("exec " + StoreProcedureName + " " + St);
            int i = 0;
            foreach (object obj in parameters)
            {
                query.SetParameter(i, obj);
                ++i;
            }


            var results = query.SetResultTransformer(new AliasToBeanResultTransformer(typeof(T)));

            return results.List<T>();

        }

        public void CommitTransaction()
        {
            currentSession.Transaction.Commit();
        }

        public DataTable ExecuteStoredProcedureSelectDataTable(string StoreProcedureName, params object[] parameters)
        {

            //DataTable dt = new DataTable();

            //using (IDbConnection con = currentSession.Connection)
            //{

            //    IDbCommand cmd = con.CreateCommand();
            //    cmd.CommandText = StoreProcedureName;

            //    IDataParameter param = cmd.CreateParameter();
            //    param.ParameterName = "@parameterName";
            //    param.Value = "parameter value";

            //    cmd.CommandType = CommandType.StoredProcedure;
            //    CustomDataAdapter cda = new CustomDataAdapter();
            //    cda.FillFromReader(dt, cmd.ExecuteReader());

            //}
            //return dt;


            List<string> str = new List<string>();
            for (int j = 0; j < parameters.Length; j++)
                str.Add("?");
            var St = String.Join(" , ", str);
            var query = currentSession.CreateSQLQuery("exec " + StoreProcedureName + " " + St);
            int i = 0;
            foreach (object obj in parameters)
            {
                query.SetParameter(i, obj);
                ++i;
            }



            var results = query.SetResultTransformer(new DataTableResultTransformer());
            return results.List()[0] as DataTable;


        }

        public void ExecuteStoredProcedureUpdate(string StoredName, params object[] parameters)
        {
            List<string> str = new List<string>();
            for (int j = 0; j < parameters.Length; j++)
                str.Add("?");
            var St = String.Join(" , ", str);

            var query = currentSession.CreateSQLQuery("exec " + StoredName + " " + St);
            int i = 0;
            foreach (object obj in parameters)
            {
                query.SetParameter(i, obj);
                ++i;
            }

            query.ExecuteUpdate();

        }

       
        public ISession CurrentSession
        {
            get { return currentSession; }
        }

        public void Evict(T entity)
        {
            currentSession.Evict(entity);
        }
    
        public IQueryable<T> Table
        {

            //using (ITransaction trans = currentSession.BeginTransaction())
            // {

            //try
            //{
            get
            {
                ICriteria icrit = currentSession.CreateCriteria<T>();
                return icrit.Future<T>().AsQueryable<T>();

            }

            // var result =  icrit.List<T>().AsQueryable<T>();

            //// trans.Commit();
            // return result;
            //return result.AsQueryable<T>();


            //}
            //catch (Exception exxx)
            //{
            //  //  trans.Rollback();
            //    NHibernateHelper.CloseSession();
            //    return null;
            //}


            //
            // }
        }
        public void StartTransaction()
        {
            currentSession.Transaction.Begin();
        }
        public void RollBackTransaction()
        {
            currentSession.Transaction.Rollback();
        }
        public void Refresh(T entity)
        {
            currentSession.Refresh(entity);
        }
        public void Flush()
        {
            currentSession.Clear();
            currentSession.Flush();
        }
        public void Delete(T entity)
        {
            using (ITransaction trans = currentSession.BeginTransaction())
            {
                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("Invalid Object " + entity.GetType().Name);
                    currentSession.Delete(entity);

                    currentSession.Flush();

                    trans.Commit();
                }
                catch (Exception ex)
                {

                    trans.Rollback();
                    NHibernateHelper.CloseSession();
                    throw new FormExceptions("Unable to Delete Entity of type : " + entity.GetType().Name + " REASON::: " + ex.Message);

                }
            }
        }

        public void SaveOrUpdate(T entity)
        {
            using (ITransaction trans = currentSession.BeginTransaction())
            {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("Invalid Object " + entity.GetType().Name);



                currentSession.SaveOrUpdate(entity);
                currentSession.Flush();
                currentSession.Refresh(entity);


                trans.Commit();


            }
            catch (Exception ex)
            {

                // trans.Rollback();
                NHibernateHelper.CloseSession();
                throw new FormExceptions("Unable to save Entity of type : " + entity.GetType().Name + " REASON::: " + ex.Message);


            }

            }
        }

    }
}
