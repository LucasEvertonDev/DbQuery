using DB.Query.Models.Entities;
using DB.Query.Core.Repositorys;

namespace DB.Query.Repositorys
{
    public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        RepositoryAfterAlias<TEntity> UseAlias(string alias);
    }
}