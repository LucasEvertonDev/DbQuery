using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class SelectCustomQuery<T> : SelectQuery<T> where T : SignQueryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectCustomQuery<T> GetCollumns<P>(params Expression<Func<P, dynamic>>[] expression)
        {
            var aux = GetPropertiesExpression<P>(expression);
            return this;
        }
    }
}
