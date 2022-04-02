using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGN.Query.Domains.SignCi;
using SIGN.Query.Extensions;
using SIGN.Query.Repository;
using System;

namespace SIGN.Query.Test
{
    [TestClass]
    public class DominioRepository
    {
        public Func<object> Coun => () => null;
        private Repository<Dominio> _dominioRepository { get; set; } = new Repository<Dominio>();
        private Repository<ItemDominio> _itemDominioRepository { get; set; } = new Repository<ItemDominio>();
        private Dominio dominio { get; set; } = new Dominio();

        [TestInitialize]
        public void Initialize()
        {
            dominio = new Dominio() { Descricao = "TESTE_LIKE", Nome = "Teste Nome" };
        }

        [TestMethod]
        public void Insert()
        {
            var query = _dominioRepository.Insert(dominio).GetQuery();
            Assert.AreEqual(query, "INSERT INTO SignCi..CiDominio (CiDominio.Nome, CiDominio.Descricao) OUTPUT Inserted.Codigo VALUES ('Teste Nome', 'TESTE_LIKE')");
        }

        [TestMethod]
        public void InsertIfNotExists()
        {
            var query = _dominioRepository.InsertIfNotExists(dominio).GetQuery();
            Assert.AreEqual(query, "IF NOT EXISTS(SELECT * FROM SignCi..CiDominio WHERE Nome = 'Teste Nome' AND Descricao = 'TESTE_LIKE') BEGIN INSERT INTO SignCi..CiDominio (CiDominio.Nome, CiDominio.Descricao) OUTPUT Inserted.Codigo VALUES ('Teste Nome', 'TESTE_LIKE') END ");
        }

        [TestMethod]
        public void Update()
        {
            var query = _dominioRepository
                            .Update(dominio)
                            .Where(a => a.Codigo > 1 && a.Descricao.LIKE("TESTE_LIKE"))
                            .GetQuery();
            Assert.AreEqual(query, "UPDATE SignCi..CiDominio SET Nome = 'Teste Nome', Descricao = 'TESTE_LIKE' WHERE (CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%')");
        }

        [TestMethod]
        public void Select()
        {
            var query = _dominioRepository
                .Select(
                    Top(1)
                )
                .Where(
                    a => a.Codigo > 1 && a.Descricao.LIKE("TESTE_LIKE")
                )
                .OrderBy(
                    a => Collumns(
                        a.Descricao
                    )
                )
                .GetQuery();
            Assert.AreEqual(query, "SELECT TOP(1) * FROM SignCi..CiDominio WHERE (CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') ORDER BY CiDominio.Descricao ASC");
        }

        [TestMethod]
        public void Delete()
        {
            var query = _dominioRepository
                            .Delete()
                            .Where(a => a.Codigo > 1 && a.Descricao.LIKE("TESTE_LIKE"))
                            .GetQuery();
            Assert.AreEqual(query, "DELETE FROM SignCi..CiDominio WHERE (CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%')");
        }

        [TestMethod]
        public void Join()
        {
            var query = _dominioRepository
                            .Select(
                                Top(1)
                            )
                            .Join<Dominio, ItemDominio>(
                                (Dominio, ItemDominio) => (Dominio.Codigo == ItemDominio.Codigo_Dominio)
                            )
                            .Where<Dominio, ItemDominio>(
                                (Dominio, ItemDominio) => Dominio.Codigo > 1
                                && Dominio.Descricao.LIKE(dominio.Descricao)
                                && (Dominio.Nome == dominio.Nome && Dominio.Descricao != null)
                            )
                            .OrderBy<Dominio, ItemDominio>(
                                (Dominio, ItemDominio) => Collumns(
                                    Dominio.Codigo,
                                    ItemDominio.Nome
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT TOP(1) * FROM SignCi..CiDominio INNER JOIN SignCi..CiItemDominio ON CiDominio.Codigo = CiItemDominio.Codigo_Dominio WHERE ((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND (CiDominio.Nome = 'Teste Nome' AND CiDominio.Descricao IS NOT NULL)) ORDER BY CiDominio.Codigo ASC, CiItemDominio.Nome ASC");
        }

        [TestMethod]
        public void LeftJoin()
        {
            var query = _dominioRepository
                            .Select(
                                Top(1)
                            )
                            .LeftJoin<Dominio, ItemDominio>(
                                (Dominio, ItemDominio) => Dominio.Codigo == ItemDominio.Codigo_Dominio
                            )
                            .Where<Dominio, ItemDominio>(
                                (Dominio, ItemDominio) => Dominio.Codigo > 1 && Dominio.Descricao.LIKE("TESTE_LIKE")
                                && Dominio.Nome != null && Dominio.Nome == dominio.Nome
                            )
                            .OrderBy<Dominio, ItemDominio>(
                                (Dominio, ItemDominio) => Collumns(
                                    Dominio.Codigo,
                                    ItemDominio.Nome
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT TOP(1) * FROM SignCi..CiDominio LEFT JOIN SignCi..CiItemDominio ON CiDominio.Codigo = CiItemDominio.Codigo_Dominio WHERE (((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL) AND CiDominio.Nome = 'Teste Nome') ORDER BY CiDominio.Codigo ASC, CiItemDominio.Nome ASC");
        }

        [TestMethod]
        public void CountMethod()
        {
            var query = _dominioRepository
                            .Select(Count)
                            .Where(
                                (Dominio) => Dominio.Codigo > 1 
                                && Dominio.Descricao.LIKE("TESTE_LIKE") && Dominio.Nome != null
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT COUNT(*) FROM SignCi..CiDominio WHERE ((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL)");
        }

        [TestMethod]
        public void Expression()
        {
            var query = _dominioRepository
                            .Select()
                            .Where(
                                (Dominio) => Dominio.Codigo > 1 && Dominio.Descricao.LIKE("TESTE_LIKE") 
                                && Dominio.Nome != null && Dominio.Nome == dominio.Nome
                            )
                            .OrderBy(
                                (Dominio) => Collumns(
                                    Dominio.Codigo
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT * FROM SignCi..CiDominio WHERE (((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL) AND CiDominio.Nome = 'Teste Nome') ORDER BY CiDominio.Codigo ASC");
        }

        [TestMethod]
        public void ComparingFunction()
        {
            var query = _dominioRepository
                            .Select()
                            .Where(
                                (Dominio) => Dominio.Codigo > TesteFunction() 
                                && Dominio.Descricao.LIKE("TESTE_LIKE") 
                                && Dominio.Nome != null && Dominio.Nome == dominio.Nome
                            )
                            .OrderBy(
                                (Dominio) => Collumns(
                                    Dominio.Codigo
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT * FROM SignCi..CiDominio WHERE (((CiDominio.Codigo > 1 AND CiDominio.Descricao LIKE '%TESTE_LIKE%') AND CiDominio.Nome IS NOT NULL) AND CiDominio.Nome = 'Teste Nome') ORDER BY CiDominio.Codigo ASC");
        }


        [TestMethod]
        public void UseAlias()
        {
            var query = _dominioRepository
                            .Select(
                                Top(1)
                            )
                            .UseAlias("d1")
                            .Join<Dominio, ItemDominio>(
                                (d1, i1) => (d1.Codigo == i1.Codigo_Dominio)
                            )
                            .Where<Dominio, ItemDominio>(
                                (d1, i1) => d1.Codigo > 1
                                && i1.Descricao.LIKE(dominio.Descricao)
                                && (d1.Nome == dominio.Nome && d1.Descricao != null)
                            )
                            .OrderBy<Dominio, ItemDominio>(
                                (d1, i1) => Collumns(
                                    d1.Codigo,
                                    i1.Nome
                                )
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT TOP(1) * FROM SignCi..CiDominio AS d1 INNER JOIN SignCi..CiItemDominio AS i1 ON d1.Codigo = i1.Codigo_Dominio WHERE ((d1.Codigo > 1 AND i1.Descricao LIKE '%TESTE_LIKE%') AND (d1.Nome = 'Teste Nome' AND d1.Descricao IS NOT NULL)) ORDER BY d1.Codigo ASC, i1.Nome ASC");
        }


        public dynamic[] Collumns(params dynamic[] array)
        {
            return array;
        }

        public object Count()
        {
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int? Top(int i)
        {
            return i;
        }

        public int TesteFunction()
        {
            return 1;
        }
    }
}