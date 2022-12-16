using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;

namespace DB.Query.Core.Steps.Insert
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class InsertStep<TEntity> : InsertPersistenceStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {

    }
}
