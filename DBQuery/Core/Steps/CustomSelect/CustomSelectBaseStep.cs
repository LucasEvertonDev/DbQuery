using Application.Domains.Entities;
using DBQuery.Core.Base;
using DBQuery.Core.Steps.CustomSelects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.SelectSteps
{
    public class CustomSelectBaseStep<TEntity> : CustomSelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <TEntityypeparam name="Entity1"></typeparam>
        /// <TEntityypeparam name="Entity2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterJoinStep<TEntity> Join<Entity1, Entity2>(Expression<Func<Entity1, Entity2, bool>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterJoinStep<TEntity>>(_levelFactory.PrepareJoinStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <TEntityypeparam name="Entity1"></typeparam>
        /// <TEntityypeparam name="Entity2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterJoinStep<TEntity> LeftJoin<Entity1, Entity2>(Expression<Func<Entity1, Entity2, bool>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterJoinStep<TEntity>>(_levelFactory.PrepareLeftJoinStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public SelectAfterWhereStep<TEntity> Where(Expression<Func<TEntity ,bool>> expression = null)
        {
            return InstanceNextLevel<SelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy(Expression<Func<TEntity, bool>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy(Expression<Func<TEntity, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }
    }
}
