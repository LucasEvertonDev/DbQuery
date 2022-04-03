using SIGN.Query.Constants;
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


            _query = string.Format(SQLKeys.INSERT_NOT_EXISTS, GetFullName(typeof(T)), SQLKeys.WHERE_WITH_SPACE + string.Join(SQLKeys.AND_WITH_SPACE, GetObjectClausules()), _query);
        }
    }
}
