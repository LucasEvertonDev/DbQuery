using DBQuery.Core.Base;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps
{
    public class UpdateStep<TEntity> : PersistenceStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public PersistenceStep<TEntity> Where(Expression<Func<TEntity, bool>> expression = null)
        {
            return InstanceNextLevel<PersistenceStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }
    }
}
