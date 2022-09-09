using DB.Query.Models.Entities;
using DB.Query.Core.Examples;
using DB.Query.Core.Services;
using DB.Query.Core.Steps.Base;
using System;
using System.Linq.Expressions;

namespace DB.Query.Core.Steps.Select
{
    public class SelectOrderByStep<TEntity> : SelectPersistenceStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo SelectAfterOrderByStep.
        /// </returns>
        public SelectAfterOrderByStep<TEntity> OrderBy(Expression<Func<TEntity, dynamic>> expression)
        {
            return InstanceNextLevel<SelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo SelectAfterOrderByStep.
        /// </returns>
        public SelectAfterOrderByStep<TEntity> OrderByDesc(Expression<Func<TEntity, dynamic>> expression)
        {
            return InstanceNextLevel<SelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo SelectAfterOrderByStep.
        /// </returns>
        public SelectAfterOrderByStep<TEntity> OrderBy(Expression<Func<TEntity, dynamic[]>> expression)
        {
            return InstanceNextLevel<SelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByAscStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um Order By Desc! 
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddOrderBy(string, Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas a serem ordenadas.</param>
        /// <returns>
        ///     Retorno do tipo SelectAfterOrderByStep.
        /// </returns>
        public SelectAfterOrderByStep<TEntity> OrderByDesc(Expression<Func<TEntity, dynamic[]>> expression)
        {
            return InstanceNextLevel<SelectAfterOrderByStep<TEntity>>(_levelFactory.PrepareOrderByDescStep(expression));
        }
    }
}
