using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Query.Core.Examples;
using DB.Query.Core.Extensions;
using DB.Query.Models.Entities.Kaizen;
using DB.Query.Models.Entities.DBCi;
using DB.Query.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.Query.Tests.Versions.v3
{
    /// <summary>
    /// Descrição resumida para DBQueryV3
    /// </summary>
    [TestClass]
    public class DBQueryV3 : DBQueryPersistenceExample
    {
        private Repository<CiEmails_Reenvio> _ciEmails_ReenvioRepository { get; set; } = new Repository<CiEmails_Reenvio>();
        private Repository<CICadUsuario> _cICadUsuario_ReenvioRepository { get; set; } = new Repository<CICadUsuario>();
        private Repository<CiEmails_Anexos> _ciEmailAnexos { get; set; } = new Repository<CiEmails_Anexos>();

        public DBQueryV3()
        {
        }

        [TestMethod]
        public void TestNewIN()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select()
                            .Distinct()
                            .Where(a => a.EmailTo != null
                                && a.ID.IN("1", "2"))
                            .GetQuery();
            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID IN ('1', '2'))");
        }


        [TestMethod]
        public void TesteNewNotIn()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select()
                            .Distinct()
                            .Where(a => a.EmailTo != null
                                && a.ID.NOT_IN("1", "2"))
                            .GetQuery();
            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID NOT IN ('1', '2'))");
        }


        [TestMethod]
        public void SelectCustomAnonimous()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => new
                                {
                                    CiEmails_Reenvio.EmailTo,
                                    CiEmails_Reenvio.EmailFrom,
                                    EmailBCC = Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom),
                                }
                            )
                            .Distinct()
                            .Where(
                                CiEmails_Reenvio =>
                                    CiEmails_Reenvio.EmailTo == Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom)
                                    && CiEmails_Reenvio.EmailFrom.LIKE(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom))
                                    && Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom) == CiEmails_Reenvio.EmailFrom
                            )
                            .OrderBy(
                                CiEmails_Reenvio => CiEmails_Reenvio.EmailTo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo AS EmailTo, CiEmails_Reenvio.EmailFrom AS EmailFrom, CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AS EmailBCC FROM DBCi..CiEmails_Reenvio WHERE ((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }


        [TestMethod]
        public void SelectCustomOld()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => Columns
                                (
                                    Alias(CiEmails_Reenvio.EmailTo, "EmailTo"),
                                    Alias(CiEmails_Reenvio.EmailFrom, "EmailFrom"),
                                    Alias(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom), "EmailBCC")
                                )
                            )
                            .Distinct()
                            .Where(
                                CiEmails_Reenvio =>
                                    CiEmails_Reenvio.EmailTo == Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom)
                                    && CiEmails_Reenvio.EmailFrom.LIKE(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom))
                                    && Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom) == CiEmails_Reenvio.EmailFrom
                            )
                            .OrderBy(
                                CiEmails_Reenvio => CiEmails_Reenvio.EmailTo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo AS EmailTo, CiEmails_Reenvio.EmailFrom AS EmailFrom, CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AS EmailBCC FROM DBCi..CiEmails_Reenvio WHERE ((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }


        [TestMethod]
        public void SelectCustomTyped()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => new CiEmails_Reenvio
                                {
                                    EmailTo = CiEmails_Reenvio.EmailTo,
                                    EmailFrom = CiEmails_Reenvio.EmailFrom,
                                    EmailBCC = Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom),
                                }
                            )
                            .Distinct()
                            .Where(
                                CiEmails_Reenvio =>
                                    CiEmails_Reenvio.EmailTo == Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom)
                                    && CiEmails_Reenvio.EmailFrom.LIKE(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom))
                                    && Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom) == CiEmails_Reenvio.EmailFrom
                            )
                            .OrderBy(
                                CiEmails_Reenvio => CiEmails_Reenvio.EmailTo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo AS EmailTo, CiEmails_Reenvio.EmailFrom AS EmailFrom, CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AS EmailBCC FROM DBCi..CiEmails_Reenvio WHERE ((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }

        [TestMethod]
        public void SelectCustomListed()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => new
                                {
                                    CiEmails_Reenvio.EmailTo,
                                    CiEmails_Reenvio.EmailFrom,
                                    EmailBCC = "teste",
                                }
                            )
                            .Distinct()
                            .Where(
                                CiEmails_Reenvio =>
                                    CiEmails_Reenvio.EmailTo == Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom)
                                    && CiEmails_Reenvio.EmailFrom.LIKE(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom))
                                    && Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom) == CiEmails_Reenvio.EmailFrom
                            )
                            .OrderBy(
                                CiEmails_Reenvio => CiEmails_Reenvio.EmailTo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo AS EmailTo, CiEmails_Reenvio.EmailFrom AS EmailFrom, 'teste' AS EmailBCC FROM DBCi..CiEmails_Reenvio WHERE ((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }

        [TestMethod]
        public void SelectCustomListedOld()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => Columns
                                (
                                    Alias(CiEmails_Reenvio.EmailTo, "EmailTo"),
                                    Alias(CiEmails_Reenvio.EmailFrom, "EmailFrom"),
                                    Alias("teste", "EmailBCC")
                                )
                            )
                            .Distinct()
                            .Where(
                                CiEmails_Reenvio =>
                                    CiEmails_Reenvio.EmailTo == Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom)
                                    && CiEmails_Reenvio.EmailFrom.LIKE(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom))
                                    && Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom) == CiEmails_Reenvio.EmailFrom
                            )
                            .OrderBy(
                                CiEmails_Reenvio => CiEmails_Reenvio.EmailTo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo AS EmailTo, CiEmails_Reenvio.EmailFrom AS EmailFrom, 'teste' AS EmailBCC FROM DBCi..CiEmails_Reenvio WHERE ((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }

        [TestMethod]
        public void SelectCustomListedOld1()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => Columns
                                (
                                    Alias(CiEmails_Reenvio.EmailTo, "EmailTo"),
                                    Alias(CiEmails_Reenvio.EmailFrom, "EmailFrom"),
                                    Alias(1, "EmailBCC")
                                )
                            )
                            .Distinct()
                            .Where(
                                CiEmails_Reenvio =>
                                    CiEmails_Reenvio.EmailTo == Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom)
                                    && CiEmails_Reenvio.EmailFrom.LIKE(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom))
                                    && Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom) == CiEmails_Reenvio.EmailFrom
                            )
                            .OrderBy(
                                CiEmails_Reenvio => CiEmails_Reenvio.EmailTo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo AS EmailTo, CiEmails_Reenvio.EmailFrom AS EmailFrom, 1 AS EmailBCC FROM DBCi..CiEmails_Reenvio WHERE ((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SelectOrderBy()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => new
                                {
                                    Email = re.EmailFrom,
                                    Anexo = an.PATH,
                                    Teste = Upper(re.Subject)
                                }
                            )
                            .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => re.ID == an.CiEmails_Reenvio_Id
                            )
                            .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => new { re.ID, re.EmailFrom }
                            ).GetQuery();

            Assert.AreEqual(query, "SELECT CiEmails_Reenvio.EmailFrom AS Email, CiEmails_Anexos.PATH AS Anexo, Upper(CiEmails_Reenvio.Subject) AS Teste FROM DBCi..CiEmails_Reenvio INNER JOIN DBCi..CiEmails_Anexos ON CiEmails_Reenvio.ID = CiEmails_Anexos.CiEmails_Reenvio_Id ORDER BY CiEmails_Reenvio.ID ASC, CiEmails_Reenvio.EmailFrom ASC");

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SelectOrderByOld()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => Columns
                                (
                                    Alias(re.EmailFrom, "Email"),
                                    Alias(an.PATH, "Anexo"),
                                    Alias(Upper(re.Subject), "Teste")
                                )
                            )
                            .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => re.ID == an.CiEmails_Reenvio_Id
                            )
                            .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => Columns(re.ID, re.EmailFrom)
                            ).GetQuery();

            Assert.AreEqual(query, "SELECT CiEmails_Reenvio.EmailFrom AS Email, CiEmails_Anexos.PATH AS Anexo, Upper(CiEmails_Reenvio.Subject) AS Teste FROM DBCi..CiEmails_Reenvio INNER JOIN DBCi..CiEmails_Anexos ON CiEmails_Reenvio.ID = CiEmails_Anexos.CiEmails_Reenvio_Id ORDER BY CiEmails_Reenvio.ID ASC, CiEmails_Reenvio.EmailFrom ASC");

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SelectGroupBy()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => new
                                {
                                    Soma = 1 + 1,
                                    Email = re.EmailFrom,
                                    Id = an.CiEmails_Reenvio_Id,
                                    Count = Count(re.EmailFrom)
                                }
                            )
                            .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => re.Status.Equals(an.ID) && "1".Equals(an.CiEmails_Reenvio_Id) && re.ID.Equals("1")
                                && re.ID.Equals(Convert.ToUInt64("13401303603").ToString(@"000\.000\.000\-00"))
                            )
                            .GroupBy<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => new { re.ID, re.EmailFrom }
                            ).GetQuery();

            Assert.AreEqual(query, "SELECT 2 AS Soma, CiEmails_Reenvio.EmailFrom AS Email, CiEmails_Anexos.CiEmails_Reenvio_Id AS Id, COUNT(CiEmails_Reenvio.EmailFrom) AS Count FROM DBCi..CiEmails_Reenvio INNER JOIN DBCi..CiEmails_Anexos ON CiEmails_Reenvio.Status = CiEmails_Anexos.ID AND '1' = CiEmails_Anexos.CiEmails_Reenvio_Id AND CiEmails_Reenvio.ID = '1' AND CiEmails_Reenvio.ID = '134.013.036-03' GROUP BY CiEmails_Reenvio.ID, CiEmails_Reenvio.EmailFrom");
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SelectGroupByOld()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => Columns
                                (
                                    Alias(1 + 1, "Soma"),
                                    Alias(re.EmailFrom, "Email"),
                                    Alias(an.CiEmails_Reenvio_Id, "Id"),
                                    Alias(Count(re.EmailFrom), "Count")
                                )
                            )
                            .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => re.Status.Equals(an.ID) && "1".Equals(an.CiEmails_Reenvio_Id) && re.ID.Equals("1")
                                && re.ID.Equals(Convert.ToUInt64("13401303603").ToString(@"000\.000\.000\-00"))
                            )
                            .GroupBy<CiEmails_Reenvio, CiEmails_Anexos>(
                                (re, an) => Columns(re.ID, re.EmailFrom)
                            ).GetQuery();

            Assert.AreEqual(query, "SELECT 2 AS Soma, CiEmails_Reenvio.EmailFrom AS Email, CiEmails_Anexos.CiEmails_Reenvio_Id AS Id, COUNT(CiEmails_Reenvio.EmailFrom) AS Count FROM DBCi..CiEmails_Reenvio INNER JOIN DBCi..CiEmails_Anexos ON CiEmails_Reenvio.Status = CiEmails_Anexos.ID AND '1' = CiEmails_Anexos.CiEmails_Reenvio_Id AND CiEmails_Reenvio.ID = '1' AND CiEmails_Reenvio.ID = '134.013.036-03' GROUP BY CiEmails_Reenvio.ID, CiEmails_Reenvio.EmailFrom");
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void WhereWithContains()
        {
            var query = _cICadUsuario_ReenvioRepository
                            .Select()
                            .Where(
                                _cICadUsuario =>
                                    _cICadUsuario.Descricao.Contains("Pedro")
                            ).GetQuery();

            Assert.AreEqual(query, "SELECT * FROM DBCi..CICadUsuario WHERE CICadUsuario.Descricao LIKE '%Pedro%'");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void TesteKaizen()
        {
            var kaizen = new Cadastro_Kaizen();
            var repository = new Repository<ResponsavelKaizen>();
            var query = repository
                          .Select()
                    .Where(est => (kaizen.Area == est.Codigo_Area)
                    && (kaizen.Depto == est.Codigo_Depto)
                    && (kaizen.FK_Setor == null || kaizen.FK_Setor == est.Codigo_Setor)
                    && (kaizen.FK_Processo == null || kaizen.FK_Processo == est.Codigo_Processo)
                    )
                    .GetQuery();

            Assert.AreEqual(query, "SELECT * FROM Kaizen..Responsavel_Kaizen WHERE (((0 = Responsavel_Kaizen.Codigo_Area AND 0 = Responsavel_Kaizen.Codigo_Depto) AND (NULL IS NULL OR NULL = Responsavel_Kaizen.Codigo_Setor)) AND (NULL IS NULL OR NULL = Responsavel_Kaizen.Codigo_Processo))");
        }


        [TestMethod]
        public void TesteIn()
        {
            var list = new List<string>() { "1212121212212", "1212121212212" };
            var query = _ciEmailAnexos.Delete().Where(a => a.CiEmails_Reenvio_Id.IN(list.GenerateScriptIN())).GetQuery();
            Assert.AreEqual(query, "DELETE FROM DBCi..CiEmails_Anexos WHERE CiEmails_Anexos.CiEmails_Reenvio_Id IN ('1212121212212', '1212121212212')");
        }

        [TestMethod]
        public void TesteInMocked()
        {
            var list = new List<string>() { "1212121212212", "1212121212212" };
            var query = _ciEmailAnexos.Delete().Where(a => a.CiEmails_Reenvio_Id.IN("('1212121212212', '1212121212212')")).GetQuery();
            Assert.AreEqual(query, "DELETE FROM DBCi..CiEmails_Anexos WHERE CiEmails_Anexos.CiEmails_Reenvio_Id IN ('1212121212212', '1212121212212')");
        }

        [TestMethod]
        public void TesteInToArrayNovo()
        {
            var list = new List<string>() { "1212121212212", "1212121212212" };
            var query = _ciEmailAnexos.Delete().Where(a => a.CiEmails_Reenvio_Id.IN(list.ToArray())).GetQuery();
            Assert.AreEqual(query, "DELETE FROM DBCi..CiEmails_Anexos WHERE CiEmails_Anexos.CiEmails_Reenvio_Id IN ('1212121212212', '1212121212212')");
        }

        [TestMethod]
        public void TesteInInstanceArrayNovo()
        {
            var list = new string[] { "1212121212212", "1212121212212" };
            var query = _ciEmailAnexos.Delete().Where(a => a.CiEmails_Reenvio_Id.IN(list)).GetQuery();
            Assert.AreEqual(query, "DELETE FROM DBCi..CiEmails_Anexos WHERE CiEmails_Anexos.CiEmails_Reenvio_Id IN ('1212121212212', '1212121212212')");
        }
    }
}
