using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGN.Query.Domains;
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

        [TestMethod]
        public void SelectCustom()
        {
            var dominio = new Dominio() { Descricao = "TESTE_LIKE", Nome = "Teste Nome" };
            var query = _ciEmails_ReenvioRepository.SelectCustom().UseAlias("ci")
                .GetCollumns(ci => Count(ci.To), ci => ci.EmailFrom, ci => ci.Subject)
                .GetCollumns<CiEmails_Anexos>(ciRe => ciRe.CHAVE, ciRe => ciRe.Tipo)
                .Join<CiEmails_Reenvio, CiEmails_Anexos>((ci, ciRe) => ci.ID == ciRe.CiEmails_Reenvio_Id)
                .Where<CiEmails_Reenvio, CiEmails_Anexos>((ci, ciRe) => (dominio.Descricao != null || ci.ID == dominio.Descricao) && (dominio.Codigo > 0 || ci.Status >= dominio.Codigo ))
                .OrderBy(ci => ci.Subject)
                .GetQuery();

        }

        public dynamic Count(dynamic prop)
        {
            return null;
        }
    }
}
