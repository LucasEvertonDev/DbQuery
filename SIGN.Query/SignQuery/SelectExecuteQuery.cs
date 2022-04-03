using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class SelectExecuteQuery<T> : OrderByQuery<T> where T : SignQueryBase
    {
        #region Group BY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> GroupBy(Expression<Func<T, dynamic>> expression)
        {
            return AddGroupBy(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> GroupBy<P>(Expression<Func<P, dynamic>> expression)
        {
            return AddGroupBy(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> GroupBy(Expression<Func<T, dynamic[]>> expression)
        {
            return AddGroupBy(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> GroupBy<A>(Expression<Func<A, dynamic[]>> expression)
        {
            return AddGroupBy(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> GroupBy<A, B>(Expression<Func<A, B, dynamic[]>> expression)
        {
            return AddGroupBy(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> GroupBy<A, B, C>(Expression<Func<A, B, C, dynamic[]>> expression)
        {
            return AddGroupBy(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> GroupBy<A, B, C, D>(Expression<Func<A, B, C, D, dynamic[]>> expression)
        {
            return AddGroupBy(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public OrderByQuery<T> GroupBy<A, B, C, D, E>(Expression<Func<A, B, C, D, E, dynamic[]>> expression)
        {
            return AddGroupBy(expression);
        }
        #endregion
    }
}
