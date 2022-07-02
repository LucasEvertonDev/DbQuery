using DBQuery.Core.Base;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DBQuery.Core.Services;

namespace DBQuery.Core.Steps
{
    public class UpdateStep<TEntity> : PersistenceStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Responsável pela etapa de condições da querie
        ///     <para>
        ///         A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade. A mesma deve possuir dois passos, mesmo em casos redundantes 
        ///         que são os de propriedades booleanas. Ou seja utilize Entidade.Propriedade == true
        ///     </para>
        ///     <para><see cref="InterpretService{TEntity}.AddWhere(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as condições da query.</param>
        /// <returns>
        ///     Retorno do tipo PersistenceStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public PersistenceStep<TEntity> Where(Expression<Func<TEntity, bool>> expression = null)
        {
            return InstanceNextLevel<PersistenceStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }
    }
}
