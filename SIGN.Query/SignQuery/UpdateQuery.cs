using SIGN.Query.Constants;
using SIGN.Query.DataAnnotations;
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
    public class UpdateQuery<T> : SignQuery<T> where T : SignQueryBase
    {
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
        protected override void SetDefaultFields(T domain, bool isScalar)
        {
            base.SetDefaultFields(domain, isScalar);
            var list = GetObjectClausules();
            _query = String.Format(SQLKeys.UPDATE,
                                 string.IsNullOrEmpty(this._dataBase) ? GetTableName(typeof(T)) : this._dataBase + ".." + GetTableName(typeof(T)),
                                 string.Join(", ", list),
                                 string.Empty);
        }
    }
}
