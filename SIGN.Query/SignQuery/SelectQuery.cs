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
            if (_top.HasValue)
            {
                _query = _query.Replace(SQLKeys.SELECT_DISTINCT, string.Format(SQLKeys.SELECT_TOP, _top.Value));
            }
        }

        /// <summary>
        /// Determina que a função irá usar a nomenclatura do parametro da expression como alias 
        /// Só será funcional se for utilisado para a mesma tabela o mesmo codenome 
        /// indenpente da ação realizada
        /// O parametro alias é usado para setar o alias a classe do rpository(select)
        /// </summary>
        public virtual SelectQuery<T> UseAlias(string alias)
        {
            IncludeTop();
            _query = _query.Replace(GetFullName(typeof(T)), GetFullName(typeof(T)) + " AS " + alias);
            this._useAlias = true;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> Where(Expression<Func<T, bool>> expression = null)
        {
            IncludeTop();
            return IncludeWhereConditions(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual JoinQuery<T> Join<J, P>(Expression<Func<J, P, bool>> expression)
        {
            IncludeTop();
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
            IncludeTop();
            return IncludeJoinOnQuery<J, P>(expression, SQLKeys.LEFT_JOIN);
        }

        #region Order BY
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderBy(Expression<Func<T, dynamic[]>> expression)
        {
            IncludeTop();
            return AddOrderBy(SQLKeys.ASC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderByDesc(Expression<Func<T, dynamic[]>> expression)
        {
            IncludeTop();
            return AddOrderBy(SQLKeys.DESC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderBy(Expression<Func<T, dynamic>> expression)
        {
            IncludeTop();
            return AddOrderBy(SQLKeys.ASC, expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual OrderByQuery<T> OrderByDesc(Expression<Func<T, dynamic>> expression)
        {
            IncludeTop();
            return AddOrderBy(SQLKeys.DESC, expression);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ResultQuery<T> Execute()
        {
            IncludeTop();
            return base.Execute();
        }
    }
}
