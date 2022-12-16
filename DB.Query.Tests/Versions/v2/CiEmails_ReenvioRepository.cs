using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Query.Core.Examples;
using DB.Query.Core.Extensions;
using DB.Query.Models.Entities.DBCi;
using DB.Query.Repositorys;
using DB.Query.Services;
using System;

namespace DB.Query.Tests.DBQuery.v2
{
    [TestClass]
    public class CiEmails_ReenvioRepository : DBQueryPersistenceExample
    {
        private Repository<CiEmails_Reenvio> _ciEmails_ReenvioRepository { get; set; } = new Repository<CiEmails_Reenvio>();
        [TestMethod]
        public void SelectIn()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select()
                            .Distinct()
                            .Where(a => a.EmailTo != null
                                && a.ID.IN(new System.Collections.Generic.List<int> { 1, 2 }.GenerateScriptIN()))
                            .GetQuery();
            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID IN (1, 2))");
        }

        [TestMethod]
        public void SelectNotIn()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select()
                            .Distinct()
                            .Where(a => a.EmailTo != null
                                && a.ID.NOT_IN(new System.Collections.Generic.List<int> { 1, 2 }.GenerateScriptIN()))
                            .GetQuery();
            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID NOT IN (1, 2))");
        }

        [TestMethod]
        public void SelectCustom()
        {
            var query = _ciEmails_ReenvioRepository.UseAlias("ci")
                .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                    (ci, ciA) => Columns(
                        ci.EmailFrom,
                        ci.EmailTo,
                        ci.EmailCC,
                        ci.EmailBCC,
                        ci.Subject,
                        ciA.CHAVE,
                        ciA.ID
                    )
                    )
                .Top(1)
                .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                    (ci, ciA) => ci.ID == ciA.CiEmails_Reenvio_Id
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (ci, ciA) => ci.EmailFrom != null && ciA.CHAVE != null
                )
                .OrderBy(
                    (ci) => Columns(
                        ci.EmailTo,
                        ci.Subject
                    )
                )
                .GetQuery();

            Assert.AreEqual(query, "SELECT TOP(1) ci.EmailFrom, ci.EmailTo, ci.EmailCC, ci.EmailBCC, ci.Subject, ciA.CHAVE, ciA.ID FROM DBCi..CiEmails_Reenvio AS ci INNER JOIN DBCi..CiEmails_Anexos AS ciA ON ci.ID = ciA.CiEmails_Reenvio_Id WHERE (ci.EmailFrom IS NOT NULL AND ciA.CHAVE IS NOT NULL) ORDER BY ci.EmailTo ASC, ci.Subject ASC");

        }

        [TestMethod]
        public void Sum()
        {
            var query = _ciEmails_ReenvioRepository
                .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                            Alias(Count(anx.CiEmails_Reenvio_Id), "Count"),
                            Alias(Sum(anx.Tipo), "Sum_Tipo"),
                            anx.CiEmails_Reenvio_Id
                        )
                )
                .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => (em.ID == anx.CiEmails_Reenvio_Id)
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID != "0"
                )
                .GroupBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                            anx.CiEmails_Reenvio_Id
                        )
                )
                .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                                Sum(anx.Tipo)
                        )
                )
                .GetQuery();

            Assert.AreEqual(query, "SELECT COUNT(CiEmails_Anexos.CiEmails_Reenvio_Id) AS Count, Sum(CiEmails_Anexos.Tipo) AS Sum_Tipo, CiEmails_Anexos.CiEmails_Reenvio_Id FROM DBCi..CiEmails_Reenvio INNER JOIN DBCi..CiEmails_Anexos ON CiEmails_Reenvio.ID = CiEmails_Anexos.CiEmails_Reenvio_Id WHERE CiEmails_Reenvio.ID <> '0' GROUP BY CiEmails_Anexos.CiEmails_Reenvio_Id ORDER BY Sum(CiEmails_Anexos.Tipo) ASC");
        }

        [TestMethod]
        public void INNERJOIN()
        {
            var query = _ciEmails_ReenvioRepository.UseAlias("em")
                .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                            em.EmailTo,
                            em.EmailFrom,
                            anx.CiEmails_Reenvio_Id
                        )
                )
                .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID == anx.CiEmails_Reenvio_Id
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID != "0"
                )
                .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                             anx.CiEmails_Reenvio_Id
                        )
                )
                .GetQuery();

            Assert.AreEqual(query, "SELECT em.EmailTo, em.EmailFrom, anx.CiEmails_Reenvio_Id FROM DBCi..CiEmails_Reenvio AS em INNER JOIN DBCi..CiEmails_Anexos AS anx ON em.ID = anx.CiEmails_Reenvio_Id WHERE em.ID <> '0' ORDER BY anx.CiEmails_Reenvio_Id ASC");
        }



        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Min()
        {
            var query = _ciEmails_ReenvioRepository
                .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                            Alias(Min(anx.ID), "Min_ID"),
                            anx.CiEmails_Reenvio_Id
                        )
                )
                .LeftJoin<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID == anx.CiEmails_Reenvio_Id
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID != "0"
                )
                .GroupBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                            anx.CiEmails_Reenvio_Id
                        )
                )
                .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                             Min(anx.Tipo)
                        )
                )
                .GetQuery();

            Assert.AreEqual(query, "SELECT Min(CiEmails_Anexos.ID) AS Min_ID, CiEmails_Anexos.CiEmails_Reenvio_Id FROM DBCi..CiEmails_Reenvio LEFT JOIN DBCi..CiEmails_Anexos ON CiEmails_Reenvio.ID = CiEmails_Anexos.CiEmails_Reenvio_Id WHERE CiEmails_Reenvio.ID <> '0' GROUP BY CiEmails_Anexos.CiEmails_Reenvio_Id ORDER BY Min(CiEmails_Anexos.Tipo) ASC");
        }

        [TestMethod]
        public void Concat()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => Columns(
                                    CiEmails_Reenvio.EmailTo,
                                    CiEmails_Reenvio.EmailFrom,
                                    Alias(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom), "ConcatEmails")
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

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo, CiEmails_Reenvio.EmailFrom, CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AS ConcatEmails FROM DBCi..CiEmails_Reenvio WHERE ((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }


        [TestMethod]
        public void ConcatLike()
        {
            var query = _ciEmails_ReenvioRepository
                            .Select(
                                CiEmails_Reenvio => Columns(
                                    CiEmails_Reenvio.EmailTo,
                                    CiEmails_Reenvio.EmailFrom,
                                    Alias(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom), "ConcatEmails")
                                )
                            )
                            .Distinct()
                            .Where(
                                CiEmails_Reenvio =>
                                    CiEmails_Reenvio.EmailTo == Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom)
                                    && CiEmails_Reenvio.EmailFrom.LIKE(Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom))
                                    && Concat(CiEmails_Reenvio.EmailTo, "_", CiEmails_Reenvio.EmailFrom) == CiEmails_Reenvio.EmailFrom
                                    && CiEmails_Reenvio.EmailFrom.LIKE(CiEmails_Reenvio.EmailTo)
                            )
                            .OrderBy(
                                CiEmails_Reenvio => CiEmails_Reenvio.EmailTo
                            )
                            .GetQuery();

            Assert.AreEqual(query, "SELECT DISTINCT CiEmails_Reenvio.EmailTo, CiEmails_Reenvio.EmailFrom, CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AS ConcatEmails FROM DBCi..CiEmails_Reenvio WHERE (((CiEmails_Reenvio.EmailTo = CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) ,'%')) AND CONCAT(CONVERT(varchar(Max), CiEmails_Reenvio.EmailTo),CONVERT(varchar(Max), '_'),CONVERT(varchar(Max), CiEmails_Reenvio.EmailFrom)) = CiEmails_Reenvio.EmailFrom) AND CiEmails_Reenvio.EmailFrom LIKE CONCAT('%', CiEmails_Reenvio.EmailTo ,'%')) ORDER BY CiEmails_Reenvio.EmailTo ASC");
        }

        [TestMethod]
        public void AllCollumnsJoin()
        {
            var query = _ciEmails_ReenvioRepository.UseAlias("em")
                .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                            em.AllColumns(),
                            em.EmailTo,
                            em.EmailFrom,
                            anx.CiEmails_Reenvio_Id,
                            anx.AllColumns()
                        )
                )
                .Join<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID == anx.CiEmails_Reenvio_Id
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID != "0"
                )
                .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                             anx.CiEmails_Reenvio_Id
                        )
                )
                .GetQuery();

            Assert.AreEqual(query, "SELECT em.ID, em.EmailFrom, em.EmailTo, em.EmailCC, em.EmailBCC, em.Subject, em.Source, em.Status, em.Update_At, em.Created_At, em.EmailTo, em.EmailFrom, anx.CiEmails_Reenvio_Id, anx.ID, anx.CiEmails_Reenvio_Id, anx.CHAVE, anx.VALOR, anx.PATH, anx.Tipo FROM DBCi..CiEmails_Reenvio AS em INNER JOIN DBCi..CiEmails_Anexos AS anx ON em.ID = anx.CiEmails_Reenvio_Id WHERE em.ID <> '0' ORDER BY anx.CiEmails_Reenvio_Id ASC");
        }

        [TestMethod]
        public void TestInsertOrUpdate()
        {
            var ciemails = new CiEmails_Reenvio
            {
                ID = "1",
                EmailBCC = "Email bcc",
                EmailCC = "Email cc",
                EmailFrom = "Email from",
                EmailTo = "Email to",
                Status = 4,
                Subject = "subkect",
                Source = "source",
                Update_At = new DateTime(2022, 10, 1),
                Created_At = new DateTime(2022, 10, 1)
            };

            var query = _ciEmails_ReenvioRepository.InsertOrUpdate(ciemails).Where(a => a.ID == "1").GetQuery();

            Assert.AreEqual(query, "IF NOT EXISTS(SELECT * FROM DBCi..CiEmails_Reenvio WHERE CiEmails_Reenvio.ID = '1') BEGIN INSERT INTO DBCi..CiEmails_Reenvio (CiEmails_Reenvio.ID, CiEmails_Reenvio.EmailFrom, CiEmails_Reenvio.EmailTo, CiEmails_Reenvio.EmailCC, CiEmails_Reenvio.EmailBCC, CiEmails_Reenvio.Subject, CiEmails_Reenvio.Source, CiEmails_Reenvio.Status, CiEmails_Reenvio.Update_At, CiEmails_Reenvio.Created_At) VALUES ('1', 'Email from', 'Email to', 'Email cc', 'Email bcc', 'subkect', 'source', 4, '2022-10-01 00:00:00', '2022-10-01 00:00:00') END ELSE BEGIN UPDATE DBCi..CiEmails_Reenvio SET ID = '1', EmailFrom = 'Email from', EmailTo = 'Email to', EmailCC = 'Email cc', EmailBCC = 'Email bcc', Subject = 'subkect', Source = 'source', Status = 4, Update_At = '2022-10-01 00:00:00', Created_At = '2022-10-01 00:00:00' WHERE CiEmails_Reenvio.ID = '1' END");
        }
    }
}