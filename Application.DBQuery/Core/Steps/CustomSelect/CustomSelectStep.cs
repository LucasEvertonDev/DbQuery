using DBQuery.Core.Base;
using DBQuery.Core.Steps.SelectSteps;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps
{
    public class CustomSelectStep<TEntity> : CustomSelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CustomSelectAfterDistinctStep<TEntity> Distinct()
        {
            return InstanceNextLevel<CustomSelectAfterDistinctStep<TEntity>>(_levelFactory.PrepareDistinctStep());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CustomSelectAfterTopStep<TEntity> Top(int top)
        {
            return InstanceNextLevel<CustomSelectAfterTopStep<TEntity>>(_levelFactory.PrepareTopStep(top));
        }
    }
}
