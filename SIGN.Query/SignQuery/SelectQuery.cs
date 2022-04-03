using SIGN.Query.Constants;
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
    public class SelectQuery<T> : SignQuery<T> where T : SignQueryBase
    {
        public int? _top { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="origin"></param>
        protected override void SetDefaultFields(T domain, bool isScalar)
        {
            base.SetDefaultFields(domain, isScalar);
            _query = string.Format(SQLKeys.SELECT,
                                  SQLKeys.ALL_COLUMNS,
                                  string.IsNullOrEmpty(this._dataBase) ? GetTableName(typeof(T)) : GetFullName(typeof(T)),
                                  string.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        public void IncludeTop()
        {
            if (_top.HasValue && !_query.Contains(DbQueryConstants.TOP_FUNCTION))
            {
                _query = _query.Replace(SQLKeys.SELECT_KEY, string.Format(SQLKeys.SELECT_TOP, _top.Value));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SelectQuery<T> Distinct()
        {
            PreRoutine();
            if (_top.HasValue)
            {
                _query = _query.Replace(string.Format(SQLKeys.SELECT_TOP, _top.Value), String.Format(SQLKeys.SELECT_DISTINCT_TOP, _top.Value));
            }
            else
            {
                _query = _query.Replace(SQLKeys.SELECT_KEY, SQLKeys.SELECT_DISTINCT);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void PreRoutine()
        {
            if (!this._query.Contains(SQLKeys.AS) && !string.IsNullOrEmpty(_alias))
            {
                _useAlias = true;
                _query = _query.Replace(GetFullName(typeof(T)), GetFullName(typeof(T)) + SQLKeys.AS_WITH_SPACE + _alias);
            }
            IncludeTop();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> Where(Expression<Func<T, bool>> expression = null)
        {
            PreRoutine();
            return IncludeWhereConditions(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ResultQuery<T> Execute()
        {
            PreRoutine();
            return base.Execute();
        }

        #region JOINS
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual JoinQuery<T> Join<J, P>(Expression<Func<J, P, bool>> expression)
        {
            PreRoutine();
            return IncludeJoinOnQuery<J, P>(expression, SQLKeys.INNER_JOIN);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual JoinQuery<T> LeftJoin<J, P>(Expression<Func<J, P, bool>> expression)
        {
            PreRoutine();
            return IncludeJoinOnQuery<J, P>(expression, SQLKeys.LEFT_JOIN);
        }
        #endregion

        #region Order BY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderBy(Expression<Func<T, dynamic[]>> expression)
        {
            PreRoutine();
            return AddOrderBy(SQLKeys.ASC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderByDesc(Expression<Func<T, dynamic[]>> expression)
        {
            PreRoutine();
            return AddOrderBy(SQLKeys.DESC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderBy(Expression<Func<T, dynamic>> expression)
        {
            PreRoutine();
            return AddOrderBy(SQLKeys.ASC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderByDesc(Expression<Func<T, dynamic>> expression)
        {
            PreRoutine();
            return AddOrderBy(SQLKeys.DESC, expression);
        }
        #endregion
    }
}
