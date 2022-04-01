using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class SelectQueryBase<T> : SignQuery<T> where T : SignQueryBase
    {
        protected const string SELECT = "SELECT DISTINCT {0} FROM {1} {2}";
        protected const string INNER_JOIN = "INNER JOIN {0} ON {1}";
        protected const string LEFT_JOIN = "LEFT JOIN {0} ON {1}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> Where(Expression<Func<T, bool>> expression = null)
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
            _query = string.Format(SELECT,
                                  "*",
                                  string.IsNullOrEmpty(this.DataBase) ? GetTableName(typeof(T)) : GetFullName(typeof(T)),
                                  "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public SelectQueryBase<T> Top(int top)
        {
            _query = _query.Replace("SELECT DISTINCT", $"SELECT DISTINCT TOP({top})");
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public SelectQueryBase<T> Count()
        {
            _query = _query.Replace("*", $"COUNT(*)").Replace("DISTINCT", "");
            this.Origin = typeof(SelectCountQuery<T>);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public JoinQuery<T> Join<J, P>(Expression<Func<J, P, bool>> expression)
        {
            return IncludeJoinOnQuery<J, P>(expression, INNER_JOIN);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public JoinQuery<T> LeftJoin<J, P>(Expression<Func<J, P, bool>> expression)
        {
            return IncludeJoinOnQuery<J, P>(expression, LEFT_JOIN);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderBy(params Expression<Func<T, dynamic>>[] expression)
        {
            return AddOrderBy<T>("ASC", expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectExecuteQuery<T> OrderByDesc(params Expression<Func<T, dynamic>>[] expression)
        {
            return AddOrderBy<T>("DESC", expression);
        }
    }
}
