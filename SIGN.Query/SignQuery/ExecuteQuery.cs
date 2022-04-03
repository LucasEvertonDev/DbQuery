using SIGN.Query.Domains;
using SIGN.Query.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class ExecuteQuery<T> : SignQuery<T> where T : SignQueryBase
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ResultQuery<T> Execute()
        {
            return new ResultQuery<T>(ExecuteSql());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected dynamic ExecuteSql()
        {
            if (this._signTransaction == null)
            {
                throw new Exception("Transação nula. Setar a transação do repository BindTransaction()");
            }

            SqlCommand Sql_Comando = new SqlCommand(_query, _signTransaction.GetConnection(), _signTransaction.GetTransaction()) { CommandType = CommandType.Text };
            if (!this._isScalar)
            {
                return Sql_Comando.ExecuteSql();
            }
            else
            {
                return Sql_Comando.ExecuteScalar();
            }
        }
    }
}
