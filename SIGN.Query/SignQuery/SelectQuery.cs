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

        protected const string SELECT = "SELECT DISTINCT {0} FROM {1} {2}";
        protected const string INNER_JOIN = "INNER JOIN {0} ON {1}";
        protected const string LEFT_JOIN = "LEFT JOIN {0} ON {1}";
        public int? _top { get; set; }

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
                                  string.IsNullOrEmpty(this._dataBase) ? GetTableName(typeof(T)) : GetFullName(typeof(T)),
                                  "");
        }

        /// <summary>
        /// 
        /// </summary>
        public void IncludeTop()
        {
            if (_top.HasValue)
            {
                _query = _query.Replace("SELECT DISTINCT", $"SELECT TOP({_top.Value})");
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
            return IncludeJoinOnQuery<J, P>(expression, INNER_JOIN);
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
            return IncludeJoinOnQuery<J, P>(expression, LEFT_JOIN);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> OrderBy(Expression<Func<T, dynamic[]>> expression)
        {
            IncludeTop();
            return AddOrderBy("ASC", expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> OrderByDesc(Expression<Func<T, dynamic[]>> expression)
        {
            IncludeTop();
            return AddOrderBy("DESC", expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> OrderBy(Expression<Func<T, dynamic>> expression)
        {
            IncludeTop();
            return AddOrderBy("ASC", expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> OrderByDesc(Expression<Func<T, dynamic>> expression)
        {
            IncludeTop();
            return AddOrderBy("DESC", expression);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> OrderBy<P>(Expression<Func<P, dynamic>> expression)
        {
            IncludeTop();
            return AddOrderBy("ASC", expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual SelectExecuteQuery<T> OrderByDesc<P>(Expression<Func<P, dynamic>> expression)
        {
            IncludeTop();
            return AddOrderBy("DESC", expression);
        }

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
