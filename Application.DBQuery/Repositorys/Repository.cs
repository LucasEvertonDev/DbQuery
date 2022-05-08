using Application.Domains.Entities;
using DBQuery.Core.Base;
using DBQuery.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Repository
{
    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RepositoryAfterAlias<TEntity> UseAlias(string alias)
        {
            return InstanceNextLevel<RepositoryAfterAlias<TEntity>>(_levelFactory.PrepareAliasStep(alias));
        }
    }
}
