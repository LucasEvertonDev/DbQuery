using System;
using System.Data;
using System.Data.SqlClient;
using DB.Query.Models.Entities;
using DB.Query.Core.Enuns;
using DB.Query.Core.Extensions;
using DB.Query.Core.Services;

namespace DB.Query.Core.Steps.Base
{
    public class PersistenceStep<TEntity> : DBQuery<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Retorna a querie montada
        /// </summary>
        /// <returns>
        ///     Retorno do tipo string
        /// </returns>
        public string GetQuery()
        {
            var query = StartTranslateQuery();
            while (query.Contains("  "))
            {
                query = query.Replace("  ", " ");
            }
            ClearOldConfigurations();
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string StartTranslateQuery()
        {
            return Activator.CreateInstance<InterpretService<TEntity>>().StartToInterpret(this._steps);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected dynamic ExecuteSql()
        {
            var query = StartTranslateQuery();
            try
            {
                if (this._transaction == null)
                {
                    throw new Exception("Transação nula. Setar a transação do repository BindTransaction()");
                }

                VerifyChangeDataBase();

                if(_transaction.ExecutedInDebug())
                {
                    LogService.PrintQuery(query);
                }

                SqlCommand Sql_Comando = new SqlCommand(query, _transaction.GetConnection(), _transaction.GetTransaction()) { CommandType = CommandType.Text };
                if (_steps.Exists(a => a.StepType == StepType.SELECT || a.StepType == StepType.CUSTOM_SELECT))
                {
                    return Sql_Comando.ExecuteSql();
                }
                else if (_steps.Exists(a => a.StepType == StepType.UPDATE || a.StepType == StepType.DELETE))
                {
                    return Sql_Comando.ExecuteNonQuery();
                }
                else
                {
                    return Sql_Comando.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Erro ao executar o script ({query}) -> {e.Message}");
            }
        }
    }
}
