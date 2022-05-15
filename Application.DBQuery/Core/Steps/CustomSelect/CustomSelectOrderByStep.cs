using DBQuery.Core.Base;
using DBQuery.Core.Steps.SimpleSelectSteps;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DBQuery.Core.Services;
using SIGN.Query.Core.Examples;
using DBQuery.Core.Examples;

namespace DBQuery.Core.Steps.CustomSelects
{
    public class CustomSelectOrderByStep<TEntity> : SelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderBy<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }
        
        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterOrderByStep.
        /// </returns>
        public CustomSelectAfterOrderByStep<TEntity> OrderByDesc<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }
    }
}
