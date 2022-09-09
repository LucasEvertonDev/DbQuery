using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;
using System.Data;

namespace DB.Query.Core.Steps.Update
{
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
    }
}
