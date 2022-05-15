using DBQuery.Core.Base;
using DBQuery.Core.Steps.SimpleSelectSteps;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Steps.CustomSelects
{
    public class CustomSelectOrderByStep<TEntity> : SelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }


         /// <summary>
         /// 
         /// </summary>
         /// <typeparam name="Entity1"></typeparam>
         /// <typeparam name="Entity2"></typeparam>
         /// <param name="expression"></param>
         /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }
    }
}
