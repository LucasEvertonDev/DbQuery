using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Query.Core.Examples;
using DB.Query.Core.Extensions;
using DB.Query.Models.Entities.DBCi;
using DB.Query.Repositorys;

namespace DB.Query.Tests.Versions.v2
{
    [TestClass]
    public class DominioRepository : DBQueryPersistenceExample
    {
        private Repository<CiDominio> _dominioRepository { get; set; } = new Repository<CiDominio>();
        private Repository<CiItemDominio> _itemDominioRepository { get; set; } = new Repository<CiItemDominio>();
        private CiDominio dominio { get; set; } = new CiDominio();

        [TestInitialize]
        public void Initialize()
        {
            dominio = new CiDominio() { Descricao = "TESTE_LIKE", Nome = "Teste Nome" };
        }

        [TestMethod]
        public void Insert()
        {
            var query = _dominioRepository.Insert(dominio).GetQuery();
            Assert.AreEqual(query, "INSERT INTO DBCi..CiDominio (CiDominio.Nome, CiDominio.Descricao) OUTPUT Inserted.Codigo VALUES ('Teste Nome', 'TESTE_LIKE')");
        }

        [TestMethod]
        public void InsertIfNotExists()
        {
            var query = _dominioRepository.InsertIfNotExists(dominio).GetQuery();
            Assert.AreEqual(query, "IF NOT EXISTS(SELECT * FROM DBCi..CiDominio WHERE Nome = 'Teste Nome' AND Descricao = 'TESTE_LIKE') BEGIN INSERT INTO DBCi..CiDominio (CiDominio.Nome, CiDominio.Descricao) OUTPUT Inserted.Codigo VALUES ('Teste Nome', 'TESTE_LIKE') END ");
        }

        [TestMethod]
        public void Update()
        {
            var query = _dominioRepository
                            .Update(dominio)
                            .Where(a => a.Codigo > 1 && a.Descricao.LIKE("TESTE_LIKE"))
                            .GetQuery();
            Assert.AreEqual(query, "UPDATE DBCi..CiDominio SET Nome = 'Teste Nome', Descricao = 'TESTE_LIKE' WHERE (CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%')");
        }

        [TestMethod]
        public void Select()
        {
            var query = _dominioRepository
                .Select()
                .Top(1)
                .Where(
                    a => a.Codigo > 1 && a.Descricao.LIKE("TESTE_LIKE")
                )
                .OrderBy(
                    a => a.Descricao
                )
                .GetQuery();
            Assert.AreEqual(query, "SELECT TOP(1) * FROM DBCi..CiDominio WHERE (CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') ORDER BY CiDominio.Descricao ASC");
        }

        [TestMethod]
        public void Delete()
        {
            var query = _dominioRepository
                            .Delete()
                            .Where(a => a.Codigo > 1 && a.Descricao.LIKE("TESTE_LIKE"))
                            .GetQuery();
            Assert.AreEqual(query, "DELETE FROM DBCi..CiDominio WHERE (CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%')");
        }

        [TestMethod]
        public void Join()
        {
            var query = _dominioRepository
                            .Select()
                            .Top(1)
                            .Join<CiDominio, CiItemDominio>(
                                (Dominio, ItemDominio) => (Dominio.Codigo == ItemDominio.Codigo_Dominio)
                            )
                            .Where<CiDominio, CiItemDominio>(
                                (Dominio, ItemDominio) => Dominio.Codigo > 1
                                && Dominio.Descricao.LIKE(dominio.Descricao)
                                && (Dominio.Nome == dominio.Nome && Dominio.Descricao != null)
                            )
                            .OrderBy<CiDominio, CiItemDominio>(
                                (Dominio, ItemDominio) => Columns(
                                    Dominio.Codigo,
                                    ItemDominio.Nome
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT TOP(1) * FROM DBCi..CiDominio INNER JOIN DBCi..CiItemDominio ON CiDominio.Codigo = CiItemDominio.Codigo_Dominio WHERE ((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND (CiDominio.Nome = 'Teste Nome' AND CiDominio.Descricao IS NOT NULL)) ORDER BY CiDominio.Codigo ASC, CiItemDominio.Nome ASC");
        }

        [TestMethod]
        public void LeftJoin()
        {
            var query = _dominioRepository
                            .Select()
                            .Top(1)
                            .LeftJoin<CiDominio, CiItemDominio>(
                                (Dominio, ItemDominio) => Dominio.Codigo == ItemDominio.Codigo_Dominio
                            )
                            .Where<CiDominio, CiItemDominio>(
                                (Dominio, ItemDominio) => Dominio.Codigo > 1 && Dominio.Descricao.LIKE("TESTE_LIKE")
                                && Dominio.Nome != null && Dominio.Nome == dominio.Nome
                            )
                            .OrderBy<CiDominio, CiItemDominio>(
                                (Dominio, ItemDominio) => Columns(
                                    Dominio.Codigo,
                                    ItemDominio.Nome
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT TOP(1) * FROM DBCi..CiDominio LEFT JOIN DBCi..CiItemDominio ON CiDominio.Codigo = CiItemDominio.Codigo_Dominio WHERE (((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL) AND CiDominio.Nome = 'Teste Nome') ORDER BY CiDominio.Codigo ASC, CiItemDominio.Nome ASC");
        }

        [TestMethod]
        public void CountMethod()
        {
            var query = _dominioRepository
                            .Select(ci => Alias(Count(), "Count"))
                            .Distinct()
                            .Where(
                                (Dominio) => Dominio.Codigo > 1
                                && Dominio.Descricao.LIKE("TESTE_LIKE") && Dominio.Nome != null
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT COUNT(*) AS Count FROM DBCi..CiDominio WHERE ((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL)");
        }

        [TestMethod]
        public void Expression()
        {
            var query = _dominioRepository
                            .Select()
                            .Distinct()
                            .Where(
                                (Dominio) => Dominio.Codigo > 1 && Dominio.Descricao.LIKE("TESTE_LIKE")
                                && Dominio.Nome != null && Dominio.Nome == dominio.Nome
                            )
                            .OrderBy(
                                (Dominio) => Dominio.Codigo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiDominio WHERE (((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL) AND CiDominio.Nome = 'Teste Nome') ORDER BY CiDominio.Codigo ASC");
        }

        [TestMethod]
        public void ComparingFunction()
        {
            var query = _dominioRepository
                            .Select()
                            .Distinct()
                            .Where(
                                (Dominio) => Dominio.Codigo > TesteFunction()
                                && Dominio.Descricao.LIKE("TESTE_LIKE")
                                && Dominio.Nome != null && Dominio.Nome == dominio.Nome
                            )
                            .OrderBy(
                                (Dominio) => Dominio.Codigo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiDominio WHERE (((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL) AND CiDominio.Nome = 'Teste Nome') ORDER BY CiDominio.Codigo ASC");
        }

        [TestMethod]
        public void UseAlias()
        {
            var query = _dominioRepository.UseAlias("d1")
                            .Select()
                            .Top(1)
                            .Join<CiDominio, CiItemDominio>(
                                (d1, i1) => (d1.Codigo == i1.Codigo_Dominio)
                            )
                            .Where<CiDominio, CiItemDominio>(
                                (d1, i1) => d1.Codigo > 1
                                && i1.Descricao.LIKE(dominio.Descricao)
                                && (d1.Nome == dominio.Nome && d1.Descricao != null)
                            )
                            .OrderBy<CiDominio, CiItemDominio>(
                                (d1, i1) => Columns(
                                    d1.Codigo,
                                    i1.Nome
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT TOP(1) * FROM DBCi..CiDominio AS d1 INNER JOIN DBCi..CiItemDominio AS i1 ON d1.Codigo = i1.Codigo_Dominio WHERE ((d1.Codigo > 1 AND i1.Descricao LIKE '%TESTE_LIKE%') AND (d1.Nome = 'Teste Nome' AND d1.Descricao IS NOT NULL)) ORDER BY d1.Codigo ASC, i1.Nome ASC");
        }

        public int TesteFunction()
        {
            return 1;
        }
    }
}