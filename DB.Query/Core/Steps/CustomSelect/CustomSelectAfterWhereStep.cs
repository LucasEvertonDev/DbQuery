﻿using DB.Query.Models.Entities;
using DB.Query.Core.Examples;
using DB.Query.Core.Services;
using DB.Query.Core.Steps.Base;
using System;
using System.Linq.Expressions;

namespace DB.Query.Core.Steps.CustomSelect
{
    public class CustomSelectAfterWhereStep<TEntity> : CustomSelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Responsável pela etapa Group By da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy(Expression<Func<TEntity, dynamic>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public virtual CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy(Expression<Func<TEntity, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <typeparam name="Entity5"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa Group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <typeparam name="Entity5"></typeparam>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }
    }
}
