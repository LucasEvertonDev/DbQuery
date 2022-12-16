using DB.Query.Models.Entities;
using DB.Query.Core.Steps.Base;

namespace DB.Query.Core.Steps.CustomSelect
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class CustomSelectAfterTopStep<TEntity> : CustomSelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
    }
}
