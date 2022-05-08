using Application.Domains.Entities;
using DBQuery.Core;
using DBQuery.Core.Base;
using DBQuery.Core.Steps;
using DBQuery.Core.Steps.SelectSteps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Repository
{
    public class RepositoryBase<TEntity> : DBQuery<TEntity>, IRepository where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public InsertStep<TEntity> Insert(TEntity domain)
        {
            return InstanceNextLevel<InsertStep<TEntity>>(_levelFactory.PrepareInsertStep(domain));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DeleteStep<TEntity> Delete()
        {
            return InstanceNextLevel<DeleteStep<TEntity>>(_levelFactory.PrepareDeleteStep());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SelectStep<TEntity> Select()
        {
            return InstanceNextLevel<SelectStep<TEntity>>(_levelFactory.PrepareSimpleSelectStep());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public UpdateStep<TEntity> Update(TEntity domain)
        {
            return InstanceNextLevel<UpdateStep<TEntity>>(_levelFactory.PrepareUpdateStep(domain));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public InsertStep<TEntity> InsertIfNotExists(TEntity domain)
        {
            return InstanceNextLevel<InsertStep<TEntity>>(_levelFactory.PrepareInsertIfNotExistsStep(domain));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectStep<TEntity> Select(Expression<Func<TEntity, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectStep<TEntity> Select(Expression<Func<TEntity, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectStep<TEntity> Select<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <typeparam name="Entity5"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }
    }
}
