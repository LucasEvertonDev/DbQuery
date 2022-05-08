using Application.Domains.Entities;
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

namespace DBQuery.Core.Base
{
    public class PersistenceStep<TEntity> : DBQuery<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        public ResultStep<TEntity> Execute()
        {
            var res = ExecuteSql();
            ClearOldConfigurations();
            return new ResultStep<TEntity>(res);
        }

        public string GetQuery()
        { 
            var query = Activator.CreateInstance<InterpretService<TEntity>>().StartToInterpret(this._steps);
            while(query.Contains("  "))
            {
                query = query.Replace("  ", " ");
            }
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

            Console.WriteLine(query);

            SqlCommand Sql_Comando = new SqlCommand(query, _transaction.GetConnection(), _transaction.GetTransaction()) { CommandType = CommandType.Text };
            if (_steps.Exists(a => a.LevelType == StepType.SELECT || a.LevelType == StepType.CUSTOM_SELECT))
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
