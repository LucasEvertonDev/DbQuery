using SIGN.Query.Domains;
using SIGN.Query.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class JoinQuery<T> : SignQuery<T> where T : SignQueryBase
    {
        protected const string INNER_JOIN = " INNER JOIN {0} ON {1}";
        protected const string LEFT_JOIN = " LEFT JOIN {0} ON {1}";

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public JoinQuery<T> Join<J, P>(Expression<Func<J, P, bool>> expression = null)
        {
            return IncludeJoinOnQuery<J, P>(expression, INNER_JOIN);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public JoinQuery<T> LeftJoin<J, P>(Expression<Func<J, P, bool>> expression = null)
        {
            return IncludeJoinOnQuery<J, P>(expression, LEFT_JOIN);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> Where<A>(Expression<Func<A, bool>> expression = null)
        {
            return IncludeWhereConditions(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> Where<A, B>(Expression<Func<A, B, bool>> expression = null)
        {
            return IncludeWhereConditions(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> Where<A, B, C>(Expression<Func<A, B, C, bool>> expression = null)
        {
            return IncludeWhereConditions(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> Where<A, B, C, D>(Expression<Func<A, B, C, D, bool>> expression = null)
        {
            return IncludeWhereConditions(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> Where<A, B, C, D, E>(Expression<Func<A, B, C, D, E, bool>> expression = null)
        {
            return IncludeWhereConditions(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="origin"></param>
        protected override void SetDefaultFields(T domain, Type origin)
        {
            base.SetDefaultFields(domain, origin);
        }
    }
}
