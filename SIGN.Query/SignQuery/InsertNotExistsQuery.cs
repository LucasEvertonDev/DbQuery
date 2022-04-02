using SIGN.Query.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class InsertNotExistsQuery<T> : InsertQuery<T> where T : SignQueryBase
    {
        protected const string INSERT_NOT_EXISTS = "IF NOT EXISTS(SELECT * FROM {0} {1}) BEGIN {2} END ";


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

            _query = string.Format(INSERT_NOT_EXISTS, GetFullName(typeof(T)), "WHERE " + string.Join(" AND ", GetObjectClausules()), _query);
        }
    }
}
