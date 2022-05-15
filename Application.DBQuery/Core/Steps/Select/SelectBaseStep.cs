using DBQuery.Core.Base;
using DBQuery.Core.Steps.CustomSelects;
using DBQuery.Core.Steps.SimpleSelectSteps;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.SelectSteps
{
    public class SelectBaseStep<TEntity> : SelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectAfterWhereStep<TEntity> Where(Expression<Func<TEntity, bool>> expression = null)
        {
            return InstanceNextLevel<SelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterJoinStep<TEntity> Join<Entity1, Entity2>(Expression<Func<Entity1, Entity2, bool>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterJoinStep<TEntity>>(_levelFactory.PrepareJoinStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterJoinStep<TEntity> LeftJoin<Entity1, Entity2>(Expression<Func<Entity1, Entity2, bool>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterJoinStep<TEntity>>(_levelFactory.PrepareLeftJoinStep(expression));
        }
    }
}
