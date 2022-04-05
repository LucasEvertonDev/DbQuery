using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGN.Query.Domains;
using SIGN.Query.Domains.SignCi;
using SIGN.Query.Extensions;
using SIGN.Query.Repository;
using SIGN.Query.Services;
using System;

namespace SIGN.Testes.Repository
{
    [TestClass]
    public class CiEmails_ReenvioRepository : SignQueryService
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
                                    ci.Cc,
                                    ci.Bcc,
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
                                    ci.To,
                                    ci.Subject
                                )
                            )
                            .GetQuery();

        }
    }
}
