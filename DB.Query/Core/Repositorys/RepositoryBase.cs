using System;
using System.Linq.Expressions;
using DB.Query.Core.Examples;
using DB.Query.Services;
using DB.Query.Models.Entities;
using DB.Query.Core.Services;
using DB.Query.Core.Steps.Base;
using DB.Query.Core.Steps.CustomSelect;
using DB.Query.Core.Steps.Select;
using DB.Query.Core.Steps.Delete;
using DB.Query.Core.Steps.Insert;
using DB.Query.Core.Steps.Update;

namespace DB.Query.Core.Repositorys
{
    public class RepositoryBase<TEntity> : DBQuery<TEntity>, IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        ///     Indica que a ação a ser realizada será um INSERT simples, sem verificações.
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Insert">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateInsertScript()">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="domain">Parametro do tipo TEntity, passado como tipo na instância da classe Repository.</param>
        /// <returns>
        ///     Retorno do tipo InsertStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação
        /// </returns>
        public InsertStep<TEntity> Insert(TEntity domain)
        {
            return InstanceNextLevel<InsertStep<TEntity>>(_levelFactory.PrepareInsertStep(domain));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um DELETE simples.
        ///     <para>ATENÇÃO!! O controle de quais entidades serão apagadas deve ser realizado na condição WHERE!</para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Delete">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateDeleteScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo DeleteStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public DeleteStep<TEntity> Delete()
        {
            return InstanceNextLevel<DeleteStep<TEntity>>(_levelFactory.PrepareDeleteStep());
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT simples!
        ///     <para>Um SELECT SIMPLES, nada mais é que um espelho da tabela representada pela entidade (SELECT * FROM TEntity)</para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <returns>
        ///     Retorno do tipo SelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public SelectStep<TEntity> Select()
        {
            return InstanceNextLevel<SelectStep<TEntity>>(_levelFactory.PrepareSimpleSelectStep());
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um UPDATE simples, sem verificações.
        ///     <para>ATENÇÃO!! O controle de quais entidades serão atualizadas deve ser realizado na condição WHERE!</para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Update">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateUpdateScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="domain">Parametro do tipo TEntity, passado como tipo na instância da classe Repository.</param>
        /// <returns>
        ///     Retorno do tipo UpdateStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public UpdateStep<TEntity> Update(TEntity domain)
        {
            return InstanceNextLevel<UpdateStep<TEntity>>(_levelFactory.PrepareUpdateStep(domain));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um INSERT, com uma validação de existência para efetuação do mesmo. 
        ///     <para>ATENÇÃO!! A verificação da existência é feita atravez de todas as propriedades mapeadas, QUE NÃO ESTÂO MAPEADAS COMO IDENTITY(AUTO-INCREMENT)</para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.InsertIfNotExists">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateInsertIfNotExistsScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="domain">Parametro do tipo TEntity, passado como tipo na instância da classe Repository.</param>
        /// <returns>
        ///     Retorno do tipo InsertStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public InsertStep<TEntity> InsertIfNotExists(TEntity domain)
        {
            return InstanceNextLevel<InsertStep<TEntity>>(_levelFactory.PrepareInsertIfNotExistsStep(domain));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um INSERT, com uma validação de existência para efetuação do mesmo.
        ///     Caso já exista é realizado o UPDATE.
        ///     <para>ATENÇÃO!! A etapa WHERE é de suma importância para essa ação.  Pois a mesma irá verificar a existência e também controlar a atualização se necessário.</para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.InsertOrUpdate">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateInsertOrUpdateScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="domain">Parametro do tipo TEntity, passado como tipo na instância da classe Repository.</param>
        /// <returns>
        ///     Retorno do tipo UpdateStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public UpdateStep<TEntity> InsertOrUpdate(TEntity domain)
        {
            return InstanceNextLevel<UpdateStep<TEntity>>(_levelFactory.PrepareInsertOrUpdateStep(domain));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um INSERT, após uma etapa de DELETE
        ///     <para>ATENÇÃO!! O controle de quais entidades serão apagadas deve ser realizado na condição WHERE!</para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.DeleteAndInsert">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateDeleteAndInsertScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="domain">Parametro do tipo TEntity, passado como tipo na instância da classe Repository.</param>
        /// <returns>
        ///     Retorno do tipo DeleteStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public DeleteStep<TEntity> DeleteAndInsert(TEntity domain)
        {
            return InstanceNextLevel<DeleteStep<TEntity>>(_levelFactory.PrepareDeleteAndInsertStep(domain));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples.SelectOneColumn">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select(Expression<Func<TEntity, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select(Expression<Func<TEntity, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples.SelectOneColumn">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1>(Expression<Func<Entity1, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1>(Expression<Func<Entity1, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2>(Expression<Func<Entity1, Entity2, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }


        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3>(Expression<Func<Entity1, Entity2, Entity3, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4>(Expression<Func<Entity1, Entity2, Entity3, Entity4, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <typeparam name="Entity5"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic[]>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        ///     Indica que a ação a ser realizada será um SELECT tipado. Onde os tipos passados no metodo serão usados para a listagem das colunas! 
        ///     <para>Para retornar valores únicos, basta instanciar a expression e selecionar a propriedade, ou função: <see cref="DBQueryExamples">Exemplo.</see></para>
        ///     <para>Para mapeamento de colunas é recomendado o uso do metodo <see cref="DBQueryPersistenceExample.Columns(dynamic[])">DbQueryPersistenceExample.Columns.</see></para>
        ///     <para>Para obter todas as propriedades de uma entidade: <see cref="EntityBase.AllColumns">SignQueryBase.AllColumns.</see></para>
        ///     <para>Funções pré definidas: <see cref="DBQueryExamples.SelectWithFunctions">Funções tratadas.</see></para>
        ///     <para>Dúvidas de como implementar? <see cref="DBQueryExamples.Select(Expression{Func{EntityBase, dynamic[]}})">Clique aqui.</see></para>
        ///     <para><see cref="InterpretService{TEntity}.GenerateSelectScript">Navegue para o método de geração script.</see></para>
        /// </summary>
        /// <typeparam name="Entity1"></typeparam>
        /// <typeparam name="Entity2"></typeparam>
        /// <typeparam name="Entity3"></typeparam>
        /// <typeparam name="Entity4"></typeparam>
        /// <typeparam name="Entity5"></typeparam>
        /// <param name="expression">Parametro usado para indicar as colunas de retorno.</param>
        /// <returns>
        ///     Retorno do tipo CustomSelectStep, responsável por garantir o controle da próxima etapa. Impedindo que esse método seja novamente chamado na mesma operação.
        /// </returns>
        public CustomSelectStep<TEntity> Select<Entity1, Entity2, Entity3, Entity4, Entity5>(Expression<Func<Entity1, Entity2, Entity3, Entity4, Entity5, dynamic>> expression)
        {
            return InstanceNextLevel<CustomSelectStep<TEntity>>(_levelFactory.PrepareSelectStep(expression));
        }

        /// <summary>
        /// Responsavável por colocar o repository em questão na transação corrente.
        /// </summary>
        /// <param name="dataBaseService"></param>
        /// <returns></returns>
        public void BindTransaction(SignTransaction dataBaseService)
        {
            _transaction = dataBaseService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseService"></param>
        public SignTransaction GetTransaction()
        {
            return _transaction;
        }
    }
}
