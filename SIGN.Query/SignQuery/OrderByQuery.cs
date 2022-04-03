using SIGN.Query.Constants;
using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class OrderByQuery<T> : ExecuteQuery<T> where T: SignQueryBase
    {
        #region Order BY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderBy(Expression<Func<T, dynamic>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderByDesc(Expression<Func<T, dynamic>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderBy<P>(Expression<Func<P, dynamic>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderByDesc<P>(Expression<Func<P, dynamic>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderBy(Expression<Func<T, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderBy<A>(Expression<Func<A, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderByDesc<A>(Expression<Func<A, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderByDesc(Expression<Func<T, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderBy<A, B>(Expression<Func<A, B, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderByDesc<A, B>(Expression<Func<A, B, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderBy<A, B, C>(Expression<Func<A, B, C, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderByDesc<A, B, C>(Expression<Func<A, B, C, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderBy<A, B, C, D>(Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderByDesc<A, B, C, D>(Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderBy<A, B, C, D, E>(Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.ASC, expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> OrderByDesc<A, B, C, D, E>(Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            return AddOrderBy(SQLKeys.DESC, expression);
        }
        #endregion
    }
}
