using DB.Query.Models.Entities;
using DB.Query.Models.Entities.DBCi;
using DB.Query.Core.Extensions;
using DB.Query.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DB.Query.Core.Examples
{
    /// <summary>
    /// A classe que realiza a ação deve herdar de Database_Persistence
    /// </summary>
    public class DBQueryExamples : DBQueryPersistenceExample
    {
        protected string Conexao { get; set; }
        protected Repository<CiDominio> _dominioRepository { get; set; }
        protected Repository<CiItemDominio> _itemDominio { get; set; }

        public DBQueryExamples()
        {
            _dominioRepository = new Repository<CiDominio>();
            _itemDominio = new Repository<CiItemDominio>();
        }

        /// <summary>
        /// Realiza a persistência da entidade em questão.
        /// </summary>
        public void Insert()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                var yourInstance = new CiDominio();
                _dominioRepository.Insert(yourInstance).Execute();
            });
        }

        /// <summary>
        /// Realiza a exclusão das entidades que se adequarem a clausula where
        /// </summary>
        public void Delete()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository.Delete().Where(CiDominio => CiDominio.Codigo == 1).Execute();
            });
        }

        /// <summary>
        /// Realiza a execução de uma querie sem joins e customizações 
        /// </summary>
        public void Select()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                /// Seleciona todas as entidades que se adequarem a condição bem como todas as colunas
                _dominioRepository.Select().Where(CiDominio => CiDominio.Codigo == 1).Execute();

                /// Realiza o select top das entidades que se adequarem a condição
                _dominioRepository.Select().Top(10).Where(CiDominio => CiDominio.Codigo == 1).Execute();

                /// Realiza o distinct das entidades que se adequarem a condição
                _dominioRepository.Select().Distinct().Where(CiDominio => CiDominio.Codigo == 1).Execute();

                /// Realiza o select ordenando de forma asc
                _dominioRepository.Select().Where(CiDominio => CiDominio.Codigo == 1).OrderBy(CiDominio => CiDominio.Codigo).Execute();

                /// Realiza o select ordenando de forma asc e desc com mais de uma coluna como criterio
                _dominioRepository.Select()
                    .Where(CiDominio => CiDominio.Codigo == 1)
                    .OrderBy(CiDominio => Columns(
                        CiDominio.Codigo,
                        CiDominio.Nome)
                    )
                    .OrderByDesc(CiDominio => CiDominio.Descricao)
                    .Execute();

                /// Realiza o select ordenando de forma asc e desc com mais de uma coluna como criterio
                _dominioRepository.Select()
                    .Where(CiDominio => CiDominio.Codigo == 1)
                    .OrderBy(CiDominio => Columns(
                        CiDominio.Codigo,
                        CiDominio.Nome)
                    )
                    .OrderByDesc(CiDominio => CiDominio.Descricao)
                    .Execute();

                _dominioRepository.Select()
                     .Where(CiDominio => CiDominio.Codigo == 1)
                     .OrderBy(CiDominio => Columns(
                         CiDominio.Codigo,
                         CiDominio.Nome)
                     )
                     .Pagination(pageNumber: 1, pageSize: 10)
                    .Execute();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void SelectDistinct()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository.Select().Distinct().Where(CiDominio => CiDominio.Codigo == 1).Execute();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void SelectTop()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository.Select().Top(10).Where(CiDominio => CiDominio.Codigo == 1).Execute();
            });
        }

        /// <summary>
        /// Seleciona uma coluna
        /// </summary>
        public void SelectOneColumn()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                /// Column Example
                int codigo = _dominioRepository.Select(CiDominio => CiDominio.Codigo).Execute().GetField();
                /// Function Example
                int count = _dominioRepository.Select(CiDominio => Count()).Execute().GetField();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void SelectWithFunctions()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                // Function Count 
                _dominioRepository
                    .Select<CiDominio, CiItemDominio>(
                        (CiDominio, CiItemDominio) => Columns(
                            Count(CiItemDominio.Codigo),
                            CiItemDominio.Codigo_Dominio,
                            CiDominio.Descricao
                        )
                    )
                    .Join<CiDominio, CiItemDominio>(
                         (CiDominio, CiItemDominio) => CiDominio.Codigo == CiItemDominio.Codigo
                    )
                    .Where<CiDominio, CiItemDominio>(
                        (CiDominio, CiItemDominio) => CiDominio.Codigo == 1
                    )
                    .GroupBy<CiDominio, CiItemDominio>(
                        (CiDominio, CiItemDominio) => Columns(
                            CiItemDominio.Codigo_Dominio,
                            CiDominio.Descricao
                        )
                    )
                    .Execute();

                // Function Max
                _dominioRepository.Select(CiDominio => Max(CiDominio.Codigo)).Execute();

                // Function Min
                _dominioRepository.Select(CiDominio => Min(CiDominio.Codigo)).Execute();

                // Function Concat
                _dominioRepository.Select(CiDominio => Concat(CiDominio.Codigo, '-', CiDominio.Descricao)).Execute();

                // Function Sum 
                _dominioRepository.Select(CiDominio => Sum(CiDominio.Codigo)).Execute();

                // Function Alias 
                _dominioRepository.Select(CiDominio => Alias(CiDominio.Codigo, "Cod")).Execute();

            });
        }

        /// <summary>
        /// Realiza o update de todas as entidades que se adequarem a clausula
        /// </summary>
        public void Update()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                var toUpdate = _dominioRepository.Select()
                    .Where(CiDominio => CiDominio.Codigo == 1)
                    .OrderBy(CiDominio => Columns(
                         CiDominio.Codigo,
                         CiDominio.Nome)
                     )
                    .Pagination(pageNumber: 1, pageSize: 10)
                    .Execute()
                    .FirstOrDefault();

                toUpdate.Nome = "Novo Nome";

                _dominioRepository.Update(toUpdate).Where(CiDominio => CiDominio.Codigo == 1).Execute();
            });
        }

        /// <summary>
        /// Recebe a entidade TEntity e realiza a persistência da mesma. Caso não encontre nenhuma com os dados condizentes. Exceto propriedades [Identity] são validadas
        /// </summary>
        public void InsertIfNotExists()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository.InsertIfNotExists(new CiDominio()).Execute();
            });
        }

        /// <summary>
        /// Recebe a entidade TEntity e realiza a atualização ou persistência da mesma. Tudo controlado em cima da condição informada
        /// </summary>
        public void InsertOrUpdate()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository.InsertOrUpdate(new CiDominio()).Where(CiDominio => CiDominio.Codigo == 1).Execute();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public void DeleteAndInsert()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository.DeleteAndInsert(new CiDominio()).Where(CiDominio => CiDominio.Codigo == 1).Execute();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public void Select(Expression<Func<EntityBase, dynamic[]>> expression)
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository
                    .Select(
                        CiDominio => Columns(
                            CiDominio.Codigo,
                            CiDominio.Nome,
                            Alias(Concat(CiDominio.Codigo, "_", CiDominio.Nome), "ConcatEmails")
                        )
                    )
                    .Distinct()
                    .Where(
                        CiDominio =>
                            CiDominio.Codigo == 1
                    )
                    .OrderBy(
                        CiDominio => CiDominio.Codigo
                    )
                    .Execute()
                    .ToList();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public void Select(Expression<Func<EntityBase, dynamic>> expression)
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository
                            .Select(ci => Alias(Count(), "Count"))
                            .Distinct()
                            .Where(
                                (Dominio) => Dominio.Codigo > 1
                                && Dominio.Descricao.LIKE("TESTE_LIKE") && Dominio.Nome != null
                            )
                            .Execute();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public void SelectManyTables(Expression<Func<EntityBase, EntityBase, dynamic[]>> expression)
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                _dominioRepository
                    .Select<CiDominio, CiItemDominio>(
                        (CiDominio, CiItemDominio) => Columns(
                            CiDominio.AllColumns(),
                            CiItemDominio.Nome
                        )
                    )
                    .Join<CiDominio, CiItemDominio>(
                        (CiDominio, CiItemDominio) => CiItemDominio.Codigo_Dominio == CiDominio.Codigo
                    )
                    .Where(
                        (Dominio) => Dominio.Codigo > 1
                        && Dominio.Descricao.LIKE("TESTE_LIKE") && Dominio.Nome != null
                    )
                    .Execute()
                    .ToDataTable();
            });
        }

        /// <summary>
        /// Funções tratadas para serem usadas nas condições
        /// </summary>
        public void ConditionFunctions()
        {
            OnTransaction(this, Conexao, (transaction) =>
            {
                var inconditions = new List<int> { 1, 2 }.GenerateScriptIN();
                var notinconditions = new List<int> { 4, 5 }.GenerateScriptIN();
                _dominioRepository
                    .Select<CiDominio, CiItemDominio>(
                        (CiDominio, CiItemDominio) => Columns(
                            CiDominio.AllColumns(),
                            CiItemDominio.Nome
                        )
                    )
                    .Join<CiDominio, CiItemDominio>(
                        (CiDominio, CiItemDominio) => CiItemDominio.Codigo_Dominio == CiDominio.Codigo
                    )
                    .Where(
                        (Dominio) => Dominio.Descricao.LIKE("TESTE_LIKE")
                            && Dominio.Codigo.IN(inconditions)
                            && Dominio.Codigo.NOT_IN(notinconditions)
                    )
                    .Execute()
                    .ToDataTable();
            });
        }
    }
}
