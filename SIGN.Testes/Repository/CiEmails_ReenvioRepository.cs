using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGN.Query.Domains.SignCi;
using SIGN.Query.Extensions;
using SIGN.Query.Repository;
using System;

namespace SIGN.Testes.Repository
{
    [TestClass]
    public class CiEmails_ReenvioRepository
    {
        private Repository<CiEmails_Reenvio> _ciEmails_ReenvioRepository { get; set; } = new Repository<CiEmails_Reenvio>();
        [TestMethod]
        public void SelectIn()
        {
             var query = _ciEmails_ReenvioRepository.Select().Where(a => a.EmailTo != null && a.ID.IN(new System.Collections.Generic.List<string> { "1", "2" }.GenerateScriptIN())).GetQuery();
             Assert.AreEqual(query, "SELECT DISTINCT * FROM SignCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID IN ('1', '2'))");
        }

        [TestMethod]
        public void SelectNotIn()
        {
            var query = _ciEmails_ReenvioRepository.Select().Where(a => a.EmailTo != null && a.ID.NOT_IN(new System.Collections.Generic.List<string> { "1", "2" }.GenerateScriptIN())).GetQuery();
            Assert.AreEqual(query, "SELECT DISTINCT * FROM SignCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID NOT IN ('1', '2'))");
        }
    }
}
