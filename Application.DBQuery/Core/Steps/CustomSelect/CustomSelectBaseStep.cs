using DBQuery.Core.Base;
using DBQuery.Core.Steps.CustomSelects;
using Application.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SIGN.Query.Core.Examples;
using DBQuery.Core.Services;
using DBQuery.Core.Examples;

namespace DBQuery.Core.Steps.SelectSteps
{
    public class CustomSelectBaseStep<TEntity> : CustomSelectOrderByStep<TEntity>, IPersistenceStep where TEntity : EntityBase
    {
        /// <summary>
        ///     Responsável pela etapa de condições da query
        ///     <para>
        ///         A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade. Que a mesma deve possuir dois passos, mesmo em casos redundantes 
        ///         que são os de propriedades booleanas. Ou seja utilize Entidade.Propriedade == true
        ///     </para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddWhere(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as condições da query.</param>
        /// <returns>
        ///     Retorno do tipo SelectAfterWhereStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public SelectAfterWhereStep<TEntity> Where(Expression<Func<TEntity, bool>> expression = null)
        {
            return InstanceNextLevel<SelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa de condições da query
        ///     <para>
        ///         A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade. A mesma deve possuir dois passos, mesmo em casos redundantes 
        ///         que são os de propriedades booleanas. Ou seja utilize Entidade.Propriedade == true
        ///     </para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.SelectManyTables(Expression{Func{EntityBase, EntityBase, dynamic[]}})" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddWhere(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <param name="expression">Parametro usado para indicar as condições da query.</param>
        /// <returns>
        ///     Retorno do tipo SelectAfterWhereStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterWhereStep<TEntity> Where<TEntity1>(Expression<Func<TEntity1, bool>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterWhereStep<TEntity>>(_levelFactory.PrepareWhereStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa de join da query
        ///     <para>
        ///         A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade. A mesma deve possuir dois passos, mesmo em casos redundantes 
        ///         que são os de propriedades booleanas. Ou seja utilize Entidade.Propriedade == true
        ///     </para>
        ///     <para>ATENÇÃO!! O primeiro tipo Entity1, é a propiedade que já está associada. O segundo tipo Entity2 é a entidade com que será feito o join.</para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.SelectManyTables(Expression{Func{EntityBase, EntityBase, dynamic[]}})" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddJoin(Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression">Parametro usado para indicar as condições da contrato do join "ON".</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterJoinStep.
        /// </returns>
        public CustomSelectAfterJoinStep<TEntity> Join<Entity1, Entity2>(Expression<Func<Entity1, Entity2, bool>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterJoinStep<TEntity>>(_levelFactory.PrepareJoinStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa de join da query
        ///     <para>
        ///         A expressão deve ter um resultado booleano, porém é de suma importância na comparação de propriedade. A mesma deve possuir dois passos, mesmo em casos redundantes 
        ///         que são os de propriedades booleanas. Ou seja utilize Entidade.Propriedade == true
        ///     </para>
        ///     <para>ATENÇÃO!! O primeiro tipo Entity1, é a propiedade que já está associada. O segundo tipo Entity2 é a entidade com que será feito o join.</para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.SelectManyTables(Expression{Func{EntityBase, EntityBase, dynamic[]}})" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddJoin(Expression, string)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression">Parametro usado para indicar as condições da contrato do join "ON".</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterJoinStep.
        /// </returns>
        public CustomSelectAfterJoinStep<TEntity> LeftJoin<Entity1, Entity2>(Expression<Func<Entity1, Entity2, bool>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterJoinStep<TEntity>>(_levelFactory.PrepareLeftJoinStep(expression));
        }

        /// <summary>
        ///      Responsável pela etapa de group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as condições da query.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectAfterGroupByStep<TEntity> GroupBy(Expression<Func<TEntity, dynamic>> expression = null)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa de group by da query
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref = "DBQueryExamples.Select" > Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.AddGroupBy(Expression, string)(Expression)">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicaras propriedades que serão agrupadas.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectAfterGroupByStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public virtual CustomSelectAfterGroupByStep<TEntity> GroupBy<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectAfterGroupByStep<TEntity>>(_levelFactory.PrepareGroupByStep(expression));
        }

        /// <summary>
        ///     Responsável pela etapa de group by da query
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
    }
}
