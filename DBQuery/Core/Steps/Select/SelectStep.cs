using Application.Domains.Entities;
using DBQuery.Core.Base;
using DBQuery.Core.Steps.SelectSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps
{
    public class SelectStep<TEntity> : SelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SelectAfterDistinctStep<TEntity> Distinct()
        {
            return InstanceNextLevel<SelectAfterDistinctStep<TEntity>>(_levelFactory.PrepareDistinctStep());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SelectAfterTopStep<TEntity> Top(int top)
        {
            return InstanceNextLevel<SelectAfterTopStep<TEntity>>(_levelFactory.PrepareTopStep(top));
        }
    }
}
