using Application.Domains.Entities;
using DBQuery.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.CustomSelects
{
    public class CustomSelectAfterJoinStep<TEntity> : CustomSelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
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
        /// <typeparam name="TEntity1"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterWhereStep<TEntity> Where<TEntity1>(Expression<Func<TEntity1, bool>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterWhereStep<TEntity> Where<Entity1, Entity2>(Expression<Func<Entity1, Entity2, bool>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterWhereStep<TEntity> Where<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, bool>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterWhereStep<TEntity> Where<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, bool>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterWhereStep<TEntity> Where<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, bool>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }
    }
}
