﻿using DB.Query.Models.Entities;
using DB.Query.Core.Repositorys;

namespace DB.Query.Repositorys
{
    public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository, IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Indica se deve ser considerado os apelidos dos parâmetros das expressions montadas como chave 'AS' para acesso as tabelas.
        /// </summary>
        /// <param name="alias">O parâmetro deve conter o nome o qual será associado a classe instanciada na query</param>
        /// <example>_repositorty.UseAlias("ci").Select().Execute(). Tal expressão dará origem a seguinte query: SELECT * FROM table as ci</example>
        /// <returns>Retorno do tipo RepositoryAfterAlias, responsável por garantir o controle da próxiam etapa.
        /// Impedindo que esse método seja novamente chamado na mesma operação</returns>
        public RepositoryAfterAlias<TEntity> UseAlias(string alias)
        {
            return InstanceNextLevel<RepositoryAfterAlias<TEntity>>(_levelFactory.PrepareAliasStep(alias));
        }
    }
}
