using DB.Query.Models.Entities;
using DB.Query.Core.Examples;
using DB.Query.Core.Services;
using DB.Query.Core.Steps.Base;
using System;
using System.Linq.Expressions;

namespace DB.Query.Core.Steps.Select
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SelectOrderByStep<TEntity> : SelectPersistenceStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {

        /// <summary>
        ///     Responsável pela etapa Order by By da query
        ///     <para>
        ///       A expressão deve listar as colunas que ordenarão a query
        ///     </para>
        ///     <para><see href="https://github.com/LucasEvertonDev/DbQuery#readme">Consulte a documentação.</see></para>    
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
        ///     Responsável pela etapa Order by By da query
        ///     <para>
        ///       A expressão deve listar as colunas que ordenarão a query
        ///     </para>
        ///     <para><see href="https://github.com/LucasEvertonDev/DbQuery#readme">Consulte a documentação.</see></para>    
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
        ///     Responsável pela etapa Order by By da query
        ///     <para>
        ///       A expressão deve listar as colunas que ordenarão a query
        ///     </para>
        ///     <para><see href="https://github.com/LucasEvertonDev/DbQuery#readme">Consulte a documentação.</see></para>    
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
        ///     Responsável pela etapa Order by By da query
        ///     <para>
        ///       A expressão deve listar as colunas que ordenarão a query
        ///     </para>
        ///     <para><see href="https://github.com/LucasEvertonDev/DbQuery#readme">Consulte a documentação.</see></para>    
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
