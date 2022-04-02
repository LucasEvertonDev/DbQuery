using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGN.Query.Domains.SignCi;
using SIGN.Query.Extensions;
using SIGN.Query.Repository;
using SIGN.Query.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Testes.Repository.Emails
{
    [TestClass]
    public class EmailsTest : SignQueryService
    {
        protected string CONEXAO = @"Data Source=LAPTOP-JGT9FNST\SQLEXPRESS;Integrated Security=True";
        protected Repository<CiEmails_Reenvio> _ciEmails_ReenvioRepository { get; set; } = new Repository<CiEmails_Reenvio>();
        protected Repository<CiEmails_Anexos> _ciEmailsAnexos_Repository { get; set; } = new Repository<CiEmails_Anexos>();

        [TestMethod]
        public void Delete()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();

                var count = _ciEmails_ReenvioRepository
                                .Select(ci => Count())
                                .Execute()
                                .Output;

                var ret = _ciEmails_ReenvioRepository
                            .Select(Top(1), ci => ci.ID)
                            .Where(ci => ci.EmailTo.LIKE(email.EmailTo))
                            .OrderBy(ci => ci.ID)
                            .Execute()
                            .Retorno.ConvertToList<int>();

                if (ret != null)
                {
                    _ciEmailsAnexos_Repository
                        .Delete()
                        .Where(ci => ci.CiEmails_Reenvio_Id.IN(ret.GenerateScriptIN()))
                        .Execute();

                    _ciEmails_ReenvioRepository
                        .Delete()
                        .Where(ci => ci.ID.IN(ret.GenerateScriptIN()))
                        .Execute();
                }
                transaction.Commit();
            });
        }

        [TestMethod]
        public void Insert()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();
                var ret = _ciEmails_ReenvioRepository.Insert(email).Execute().Output;

                if (ret != null)
                {
                    email.ListCiEmails_Anexos.ForEach(a =>
                    {
                        a.CiEmails_Reenvio_Id = ret;
                        var idAnexo = _ciEmailsAnexos_Repository.Insert(a).Execute().Output;
                    });
                }
                transaction.Commit();
            });
        }

        [TestMethod]
        public void SimpleSelect()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();
                var itens = _ciEmails_ReenvioRepository
                    .Select(Top(1))
                    .Where(ci => email.EmailFrom.LIKE(ci.EmailFrom))
                    .OrderByDesc(ci => ci.ID)
                    .Execute()
                    .Itens;

                transaction.Commit();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CiEmails_Reenvio GetEmail()
        {
            return new CiEmails_Reenvio
            {
                EmailBCC = "bcc@gmail.com",
                EmailTo = "to@gmail.com",
                EmailFrom = "from@gmail.com",
                Subject = "Assunto",
                Source = "Corpo",
                Created_At = DateTime.Now.Date,
                ListCiEmails_Anexos = new List<CiEmails_Anexos>()
                {
                    new CiEmails_Anexos
                    {
                        CHAVE = "1",
                        VALOR = "2",
                        Tipo = 10
                    },
                    new CiEmails_Anexos
                    {
                        CHAVE = "4",
                        VALOR = "5",
                        Tipo = 15
                    }
                }
            };
        }
    }
}
