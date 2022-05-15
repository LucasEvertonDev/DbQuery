using DBQuery.Core.Services;
using DBQuery.Core.Steps;
using DBQuery.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBQuery.Core.Enuns;
using Application.Domains.Entities;

namespace DBQuery.Core.Base
{
    public class PersistenceStep<TEntity> : DBQuery<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultStep<TEntity> Execute()
        {
            var res = ExecuteSql();
            ClearOldConfigurations();
            return new ResultStep<TEntity>(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        { 
            var query = Activator.CreateInstance<InterpretService<TEntity>>().StartToInterpret(this._steps);
            while(query.Contains("  "))
            {
                query = query.Replace("  ", " ");
            }
            ClearOldConfigurations();
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected dynamic ExecuteSql()
        {
            var query = Activator.CreateInstance<InterpretService<TEntity>>().StartToInterpret(this._steps);

            if (this._transaction == null)
            {
                throw new Exception("Transação nula. Setar a transação do repository BindTransaction()");
            }

            VerifyChangeDataBase();

            SqlCommand Sql_Comando = new SqlCommand(query, _transaction.GetConnection(), _transaction.GetTransaction()) { CommandType = CommandType.Text };
            if (_steps.Exists(a => a.StepType == StepType.SELECT || a.StepType == StepType.CUSTOM_SELECT))
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
