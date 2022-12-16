using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;

namespace DB.Query.Core.Steps.Select
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SelectAfterTopStep<TEntity> : SelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
    }
}
