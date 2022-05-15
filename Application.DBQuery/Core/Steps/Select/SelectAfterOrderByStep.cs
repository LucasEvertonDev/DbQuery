using DBQuery.Core.Base;
using DBQuery.Core.Services;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIGN.Query.Core.Examples;

namespace DBQuery.Core.Steps.SimpleSelectSteps
{
    public class SelectAfterOrderByStep<TEntity> : SelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Indica que a ação a ser realizada será uma paginação! 
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select()">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo PersistenceStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public PersistenceStep<TEntity> Pagination(int pageSize, int pageNumber)
        {
            return InstanceNextLevel<PersistenceStep<TEntity>>(_levelFactory.PreparePaginationStep(pageSize, pageNumber));
        }
    }
}
