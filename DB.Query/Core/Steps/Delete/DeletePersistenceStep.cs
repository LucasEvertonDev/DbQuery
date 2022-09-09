using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;
using System.Data;

namespace DB.Query.Core.Steps.Delete
{
    public class DeletePersistenceStep<TEntity> : PersistenceStep<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        ///     Realiza a execução de toda a querie montada
        /// </summary>
        /// <returns>
        ///   Retorna o numero de registros afetados
        /// </returns>
        public int Execute()
        {
            var res = ExecuteSql();
            ClearOldConfigurations();
            return new DeleteResultStep<TEntity>(res).GetNumeroRegistrosAfetados();
        }
    }
}
