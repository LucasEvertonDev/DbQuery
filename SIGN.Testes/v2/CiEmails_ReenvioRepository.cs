using Application.Domains;
using DBQuery;
using DBQuery.Core.Extensions;
using DBQuery.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SIGN.Testes.V2
{
    [TestClass]
    public class CiEmails_ReenvioRepository : DbQueryService
    {
        private Repository<CICadUsuario> _ciCadUsuario = new Repository<CICadUsuario>();
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
            Assert.AreEqual(query, "SELECT DISTINCT * FROM SignCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID IN (1, 2))");
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
            Assert.AreEqual(query, "SELECT DISTINCT * FROM SignCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID NOT IN (1, 2))");
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
                    (ci, ciA) => (ci.ID == ciA.CiEmails_Reenvio_Id)
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

            Assert.AreEqual(query, "SELECT TOP(1) ci.EmailFrom, ci.EmailTo, ci.EmailCC, ci.EmailBCC, ci.Subject, ciA.CHAVE, ciA.ID FROM SignCi..CiEmails_Reenvio AS ci INNER JOIN SignCi..CiEmails_Anexos AS ciA ON ci.ID = ciA.CiEmails_Reenvio_Id WHERE (ci.EmailFrom IS NOT NULL AND ciA.CHAVE IS NOT NULL) ORDER BY ci.EmailTo ASC, ci.Subject ASC");

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
                    (em, anx) => em.ID != 0
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

            Assert.AreEqual(query, "SELECT COUNT(*) AS Count, Sum(CiEmails_Anexos.Tipo) AS Sum_Tipo, CiEmails_Anexos.CiEmails_Reenvio_Id FROM SignCi..CiEmails_Reenvio INNER JOIN SignCi..CiEmails_Anexos ON CiEmails_Reenvio.ID = CiEmails_Anexos.CiEmails_Reenvio_Id WHERE CiEmails_Reenvio.ID <> 0 GROUP BY CiEmails_Anexos.CiEmails_Reenvio_Id ORDER BY Sum(CiEmails_Anexos.Tipo) ASC");
        }


        [TestMethod]
        public void LeftJoin()
        {
            var query = _ciEmails_ReenvioRepository
                .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                            em.EmailTo,
                            em.EmailFrom,
                            anx.CiEmails_Reenvio_Id
                        )
                )
                .LeftJoin<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => (em.ID == anx.CiEmails_Reenvio_Id)
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID != 0
                )
                .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                                anx.CiEmails_Reenvio_Id
                        )
                )
                .GetQuery();

            Assert.AreEqual(query, "SELECT CiEmails_Reenvio.EmailTo, CiEmails_Reenvio.EmailFrom, CiEmails_Anexos.CiEmails_Reenvio_Id FROM SignCi..CiEmails_Reenvio LEFT JOIN SignCi..CiEmails_Anexos ON CiEmails_Reenvio.ID = CiEmails_Anexos.CiEmails_Reenvio_Id WHERE CiEmails_Reenvio.ID <> 0 ORDER BY CiEmails_Anexos.CiEmails_Reenvio_Id ASC");
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
                    (em, anx) => (em.ID == anx.CiEmails_Reenvio_Id)
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID != 0
                )
                .OrderBy<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) =>
                        Columns(
                             anx.CiEmails_Reenvio_Id
                        )
                )
                .GetQuery();

            Assert.AreEqual(query, "SELECT em.EmailTo, em.EmailFrom, anx.CiEmails_Reenvio_Id FROM SignCi..CiEmails_Reenvio AS em INNER JOIN SignCi..CiEmails_Anexos AS anx ON em.ID = anx.CiEmails_Reenvio_Id WHERE em.ID <> 0 ORDER BY anx.CiEmails_Reenvio_Id ASC");
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
                    (em, anx) => (em.ID == anx.CiEmails_Reenvio_Id)
                )
                .Where<CiEmails_Reenvio, CiEmails_Anexos>(
                    (em, anx) => em.ID != 0
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

            Assert.AreEqual(query, "SELECT Min(CiEmails_Anexos.ID) AS Min_ID, CiEmails_Anexos.CiEmails_Reenvio_Id FROM SignCi..CiEmails_Reenvio LEFT JOIN SignCi..CiEmails_Anexos ON CiEmails_Reenvio.ID = CiEmails_Anexos.CiEmails_Reenvio_Id WHERE CiEmails_Reenvio.ID <> 0 GROUP BY CiEmails_Anexos.CiEmails_Reenvio_Id ORDER BY Min(CiEmails_Anexos.Tipo) ASC");
        }

        [TestMethod]
        public void SelectUsuario()
        {
            var query = _ciCadUsuario
                .UseAlias("u")
                .Select(
                    u => Columns(
                        u.Codigo,
                        u.Descricao,
                        u.CPF,
                        u.Email
                    )
                )
                .Distinct()
                .Top(1)
                .Where(u => u.Ativo == true)
                .OrderBy(u => u.Descricao)
                .OrderByDesc(u => u.Codigo)
                .GetQuery();
        }
    }
}
