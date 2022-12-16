﻿using DB.Query.Models.Entities;
using DB.Query.Core.Examples;
using DB.Query.Core.Services;
using DB.Query.Core.Steps.Base;

namespace DB.Query.Core.Steps.Select
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class SelectAfterDistinctStep<TEntity> : SelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT! 
        ///     <para><see href="https://github.com/LucasEvertonDev/DbQuery#readme">Consulte a documentação.</see></para> 
        ///     <para><see cref="InterpretSelectService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="top">Indica o número de linhas a ser selecionadas.</param>
        /// <returns>
        ///     Retorno do tipo SelectAfterTopStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public SelectAfterTopStep<TEntity> Top(int top)
        {
            return InstanceNextLevel<SelectAfterTopStep<TEntity>>(_levelFactory.PrepareTopStep(top));
        }
    }
}
