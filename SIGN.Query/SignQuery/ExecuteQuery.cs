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
            var result = new ResultQuery<T>() { };
            try
            {
                if (Origin == typeof(SelectCustomQuery<T>))
                {
                    _query = _query.Replace(", SELECT_CONCAT", "");
                }

                var ret = ExecuteSql();
                if (this.Origin == typeof(SelectQuery<T>) || this.Origin == typeof(JoinQuery<T>) || this.Origin == typeof(SelectCountQuery<T>))
                {
                    result.Retorno = ret;
                }

                if (this.Origin == typeof(InsertQuery<T>) || this.Origin == typeof(UpdateQuery<T>))
                {
                    result.Output = ret;
                }

                if (Origin == typeof(SelectCountQuery<T>) && result.Retorno != null || Origin == typeof(SelectCustomQuery<T>))
                {
                    result.Output = result.Retorno?.First()?[0];
                }

                if (this.Origin == typeof(SelectQuery<T>))
                {
                    if (ret == null || ret.Rows.Count == 0)
                    {
                        result.Itens = new List<T>();
                    }
                    else
                    {
                        result.Itens = ((DataTable)ret).ConvertToList<T>();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
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
            if (this.Origin == typeof(JoinQuery<T>) || this.Origin == typeof(SelectQuery<T>) || this.Origin == typeof(SelectCountQuery<T>))
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
