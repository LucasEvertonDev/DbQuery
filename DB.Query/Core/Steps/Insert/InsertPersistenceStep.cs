using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;
using System.Data;
using DB.Query.Core.Services;
using System;
using DB.Query.Core.Enuns;
using System.Linq;

namespace DB.Query.Core.Steps.Insert
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InsertPersistenceStep<TEntity> : PersistenceStep<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        ///     Realiza a execução de toda a querie montada
        /// </summary>
        /// <returns>
        /// Retorna o Identity id inserido
        /// </returns>
        public dynamic Execute()
        {
            var res = ExecuteSql();
            ClearOldConfigurations();
            return new InsertResultStep<TEntity>(res).GetIdentityId();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string StartTranslateQuery()
        {
            return Activator.CreateInstance<InterpretInsertService<TEntity>>().StartToInterpret(this._steps);
        }
    }
}
