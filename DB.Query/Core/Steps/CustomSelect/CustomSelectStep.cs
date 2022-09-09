using DB.Query.Models.Entities;
using DB.Query.Core.Examples;
using DB.Query.Core.Services;
using DB.Query.Core.Steps.Base;

namespace DB.Query.Core.Steps.CustomSelect
{
    public class CustomSelectStep<TEntity> : CustomSelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Responsável por atribuir a chave distinct na query
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.SelectDistinct">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterDistinctStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterDistinctStep<TEntity> Distinct()
        {
            return InstanceNextLevel<CustomSelectAfterDistinctStep<TEntity>>(_levelFactory.PrepareDistinctStep());
        }

        /// <summary>
        ///     Responsável por atribuir a chave top na query
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.SelectTop" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="top">Indica o número de registros a ser retornado.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterTopStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterTopStep<TEntity> Top(int top)
        {
            return InstanceNextLevel<CustomSelectAfterTopStep<TEntity>>(_levelFactory.PrepareTopStep(top));
        }
    }
}
