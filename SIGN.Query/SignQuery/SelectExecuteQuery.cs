using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class SelectExecuteQuery<T> : ExecuteQuery<T> where T : SignQueryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy(Expression<Func<T, dynamic[]>> expression)
        {
            return AddOrderBy("ASC", expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy<A>(Expression<Func<A, dynamic[]>> expression)
        {
            AddOrderBy("ASC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc<A>(Expression<Func<A, dynamic[]>> expression)
        {
            AddOrderBy("DESC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc(Expression<Func<T, dynamic[]>> expression)
        {
            return AddOrderBy("DESC", expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy<A, B>(Expression<Func<A, B, dynamic[]>> expression)
        {
            AddOrderBy("ASC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc<A, B>(Expression<Func<A, B, dynamic[]>> expression)
        {
            AddOrderBy("DESC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy<A, B, C>(Expression<Func<A, B, C, dynamic[]>> expression)
        {
            AddOrderBy("ASC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc<A, B, C>(Expression<Func<A, B, C, dynamic[]>> expression)
        {
            AddOrderBy("DESC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy<A, B, C, D>(Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            AddOrderBy("ASC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc<A, B, C, D>(Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            AddOrderBy("DESC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy<A, B, C, D, E>(Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            AddOrderBy("ASC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc<A, B, C, D, E>(Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            AddOrderBy("DESC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }
    }
}
