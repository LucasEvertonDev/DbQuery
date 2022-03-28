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
        public SelectExecuteQuery<T> OrderBy<P>(params Expression<Func<P, dynamic>>[] expression)
        {
            return AddOrderBy<P>("ASC", expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc<P>(params Expression<Func<P, dynamic>>[] expression)
        {
            return AddOrderBy<P>("DESC", expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy(params Expression<Func<T, dynamic>>[] expression)
        {
            AddOrderBy<T>("ASC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc(params Expression<Func<T, dynamic>>[] expression)
        {
            AddOrderBy<T>("DESC", expression);
            return CreateDbQuery<SelectExecuteQuery<T>>(); ;
        }
    }
}
