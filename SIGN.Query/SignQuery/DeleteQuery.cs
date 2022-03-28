using SIGN.Query.Domains;
using SIGN.Query.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class DeleteQuery<T> : SignQuery<T> where T : SignQueryBase
    {
        protected const string DELETE = "DELETE FROM {0}{1}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public ExecuteQuery<T> Where(Expression<Func<T, bool>> expression = null)
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
            _query = String.Format(DELETE, string.IsNullOrEmpty(this.DataBase) ? GetTableName(typeof(T)) : this.DataBase + ".." + GetTableName(typeof(T)), "");
        }
    }
}
