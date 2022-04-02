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
        protected const string INSERT = "INSERT INTO {0} ({1}) {2} VALUES ({3})";

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
        protected override void SetDefaultFields(T domain, Type origin)
        {
            base.SetDefaultFields(domain, origin);
            _query = String.Format(INSERT,
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
            bool insert = typeof(InsertQuery<T>) == _origin;

            var values = new List<string>();
            _domain.GetType().GetProperties().ToList().ForEach(prop =>
            {
                if ((insert && prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0) || !insert)
                {
                    if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                    {
                        var val = TratarValor((dynamic)prop.GetValue(_domain), true);
                        values.Add(val?.ToString());
                    }
                }
            });
            return values;
        }
    }
}
