using DB.Query.Models.Entities;
using DB.Query.Core.Examples;
using DB.Query.Core.Services;
using DB.Query.Core.Steps.Base;
using System;
using System.Linq.Expressions;

namespace DB.Query.Core.Steps.Delete
{
    public class DeleteStep<TEntity> : DeletePersistenceStep<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        ///     Responsável pela etapa de filtros da query
        ///     <para>
        ///         A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade a mesma possuir dois passos, mesmo em casos redundantes 
        ///         que são os de propriedades booleanas. Ou seja utilize Entidade.Propriedade == true
        ///     </para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Delete">Clique aqui.</see></para>
        ///     <para>Como usar funções comparativas(LIKE, IN, NOT IN)?<see cref = "DBQueryExamples.ConditionFunctions" > Clique aqui.</see></para>    
        ///     <para><see cref="InterpretService{TEntity}.AddWhere(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as condições da query.</param>
        /// <returns>
        ///     Retorno do tipo PersistenceStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public DeletePersistenceStep<TEntity> Where(Expression<Func<TEntity, bool>> expression = null)
        {
            return InstanceNextLevel<DeletePersistenceStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }
    }
}
