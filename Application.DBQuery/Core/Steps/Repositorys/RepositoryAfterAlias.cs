using Application.Domains.Entities;
using DBQuery.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Repository
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
