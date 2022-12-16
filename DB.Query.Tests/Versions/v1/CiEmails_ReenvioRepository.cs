using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB.Query.Core.Extensions;
using DB.Query.Models.Entities.DBCi;
using DB.Query.Repositorys;
using System;

namespace DB.Query.Tests.Versions.v1
{
    [TestClass]
    public class CiEmails_ReenvioRepository
    {
        private Repository<CiEmails_Reenvio> _ciEmails_ReenvioRepository { get; set; } = new Repository<CiEmails_Reenvio>();
        [TestMethod]
        public void SelectIn()
        {
            var query = _ciEmails_ReenvioRepository.Select().Distinct().Where(a => a.EmailTo != null && a.ID.IN(new System.Collections.Generic.List<string> { "1", "2" }.GenerateScriptIN())).GetQuery();
            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID IN ('1', '2'))");
        }

        [TestMethod]
        public void SelectNotIn()
        {
            var query = _ciEmails_ReenvioRepository.Select().Distinct().Where(a => a.EmailTo != null && a.ID.NOT_IN(new System.Collections.Generic.List<string> { "1", "2" }.GenerateScriptIN())).GetQuery();
            Assert.AreEqual(query, "SELECT DISTINCT * FROM DBCi..CiEmails_Reenvio WHERE (CiEmails_Reenvio.EmailTo IS NOT NULL AND CiEmails_Reenvio.ID NOT IN ('1', '2'))");
        }
    }
}