using Application.Domains.Entities;
using DBQuery.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.SelectSteps
{
    public class CustomSelectAfterDistinctStep<TEntity> : CustomSelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
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
