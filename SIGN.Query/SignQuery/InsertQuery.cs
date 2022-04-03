using SIGN.Query.Constants;
using SIGN.Query.DataAnnotations;
using SIGN.Query.Domains;
using SIGN.Query.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class InsertQuery<T> : SignQuery<T> where T : SignQueryBase
    {
      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ExecuteQuery<T> Add()
        {
            return CreateDbQuery<ExecuteQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="origin"></param>
        protected override void SetDefaultFields(T domain, bool isScalar)
        {
            base.SetDefaultFields(domain, isScalar);
            _query = String.Format(SQLKeys.INSERT,
                                     string.IsNullOrEmpty(this._dataBase) ? GetTableName(typeof(T)) : GetFullName(typeof(T)),
                                     string.Join(", ", GetProperties()),
                                     GetPrimaryKeyName(typeof(T)),
                                     string.Join(", ", GetValues()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<string> GetValues()
        {
            var values = new List<string>();
            _domain.GetType().GetProperties().ToList().ForEach(prop =>
            {
                if ((prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0))
                {
                    if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                    {
                        var val = TreatValue((dynamic)prop.GetValue(_domain), true);
                        values.Add(val?.ToString());
                    }
                }
            });
            return values;
        }
    }
}
