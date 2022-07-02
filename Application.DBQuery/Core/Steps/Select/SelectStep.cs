using DBQuery.Core.Base;
using DBQuery.Core.Steps.SelectSteps;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGN.Query.Core.Examples;
using DBQuery.Core.Services;

namespace DBQuery.Core.Steps
{
    public class SelectStep<TEntity> : SelectBaseStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Responsável por atribuir a chave distinct a consulta montada
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.SelectDistinct">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo SelectAfterDistinctStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public SelectAfterDistinctStep<TEntity> Distinct()
        {
            return InstanceNextLevel<SelectAfterDistinctStep<TEntity>>(_levelFactory.PrepareDistinctStep());
        }

        /// <summary>
        ///     Responsável por atribuir a chave top a consulta montada
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.SelectTop" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo SelectAfterTopStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public SelectAfterTopStep<TEntity> Top(int top)
        {
            return InstanceNextLevel<SelectAfterTopStep<TEntity>>(_levelFactory.PrepareTopStep(top));
        }
    }
}
