using DB.Query.Models.Entities;

namespace DB.Query.Core.Repositorys
{
    public class RepositoryAfterAlias<TEntity> : RepositoryBase<TEntity>, IRepository where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected override void StartLevel()
        {
            base.StartLevel();
        }
    }
}
