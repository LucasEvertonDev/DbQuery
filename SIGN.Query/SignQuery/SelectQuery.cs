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
    public class SelectQuery<T> : SelectQueryBase<T> where T : SignQueryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectQuery<T> GetCollumns<P>(params Expression<Func<P, dynamic>>[] expression)
        {
            var aux = GetPropertiesExpression<P>(expression);
            aux.Add("SELECT_CONCAT");
            if (_query.Contains("*"))
            {
                _query = _query.Replace("*", String.Join(", ", aux));
            }
            else if (_query.Contains("SELECT_CONCAT"))
            {
                _query = _query.Replace("SELECT_CONCAT", String.Join(", ", aux));
            }
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectQuery<T> GetCollumns(params Expression<Func<T, dynamic>>[] expression)
        {
            var aux = GetPropertiesExpression<T>(expression);
            aux.Add("SELECT_CONCAT");
            if (_query.Contains("*"))
            {
                _query = _query.Replace("*", String.Join(", ", aux));
            }
            else if (_query.Contains("SELECT_CONCAT"))
            {
                _query = _query.Replace("SELECT_CONCAT", String.Join(", ", aux));
            }
            return this;
        }

        /// <summary>
        /// Determina que a função irá usar a nomenclatura do parametro da expression como alias 
        /// Só será funcional se for utilisado para a mesma tabela o mesmo codenome 
        /// indenpente da ação realizada
        /// O parametro alias é usado para setar o alias a classe do rpository(select)
        /// </summary>
        public virtual SelectQuery<T> UseAlias(string alias)
        {
            _query = _query.Replace(GetFullName(typeof(T)), GetFullName(typeof(T)) + " AS " + alias);
            this._useAlias = true;
            return this;
        }
    }
}
