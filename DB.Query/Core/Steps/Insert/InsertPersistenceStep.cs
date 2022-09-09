using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;
using System.Data;

namespace DB.Query.Core.Steps.Insert
{
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
    }
}
