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
                var ret = ExecuteSql();
                if (this._origin == typeof(SelectQuery<T>) || this._origin == typeof(JoinQuery<T>) 
                    || this._origin == typeof(SelectCountQuery<T>) || this._origin == typeof(SelectCustomQuery<T>))
                {
                    if (ret.GetType() == typeof(DataTable))
                        result.Retorno = ret;
                    else
                    {
                        result.Retorno = new DataTable();
                        result.Retorno.AddColluns("ret");
                        result.Retorno.Rows.Add(ret);
                    }
                }

                if (this._origin == typeof(InsertQuery<T>) || this._origin == typeof(UpdateQuery<T>))
                {
                    result.Output = ret;
                }

                if (_origin == typeof(SelectCountQuery<T>) || (_origin == typeof(SelectCustomQuery<T>) && ret.GetType() != typeof(DataTable)))
                {
                    result.Output = ret;
                }

                if (this._origin == typeof(SelectQuery<T>))
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
            if (this._origin == typeof(JoinQuery<T>) || this._origin == typeof(SelectQuery<T>) || this._origin == typeof(SelectCountQuery<T>))
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
