using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;
using System.Data;
using DB.Query.Core.Services;
using System;

namespace DB.Query.Core.Steps.Update
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class UpdatePersistenceStep<TEntity> : PersistenceStep<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        ///     Realiza a execução de toda a querie montada
        /// </summary>
        /// <returns>
        ///    Retorna o númeto de registros afetados
        /// </returns>
        public int Execute()
        {
            var res = ExecuteSql();
            ClearOldConfigurations();
            return new UpdateResultStep<TEntity>(res).GetNumeroRegistrosAfetados();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string StartTranslateQuery()
        {
            return Activator.CreateInstance<InterpretUpdateService<TEntity>>().StartToInterpret(this._steps);
        }
    }
}
