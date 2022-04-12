using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIGN.Query.Domains.SignCi;
using SIGN.Query.Domains.SignEst;
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
        protected Repository<EstTesteDecimal> _estTesteDecimalRepository { get; set; } = new Repository<EstTesteDecimal>();

        [TestMethod]
        public void Delete()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();

                var count = _ciEmails_ReenvioRepository
                                .Select(ci => Count())
                                .Execute()
                                .GetOutput();

                var ret = _ciEmails_ReenvioRepository
                            .Select(ci => ci.ID)
                            .Top(1)
                            .Where(ci => ci.EmailTo.LIKE(email.EmailTo))
                            .OrderByDesc(ci => ci.ID)
                            .Execute()
                            .GetItens<int>();

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

                Insert();

                transaction.Commit();
            });
        }

        [TestMethod]
        public void Insert()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();
                var ret = _ciEmails_ReenvioRepository.Insert(email).Execute().GetOutput();

                if (ret != null)
                {
                    email.ListCiEmails_Anexos.ForEach(a =>
                    {
                        a.CiEmails_Reenvio_Id = ret;
                        var idAnexo = _ciEmailsAnexos_Repository.Insert(a).Execute().GetOutput();
                    });
                }

                // deve trocar a database automático
                _estTesteDecimalRepository.Insert(new EstTesteDecimal
                {
                    column1 = decimal.Parse("18"),
                    column2 = decimal.Parse("18,9999"),
                    column3 = decimal.Parse("18,1")
                }).Execute();

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
                    .Select()
                    .Top(1)
                    .Where(ci => email.EmailFrom.LIKE(ci.EmailFrom))
                    .OrderByDesc(ci => ci.ID)
                    .Execute()
                    .GetItens();

                transaction.Commit();
            });
        }

        [TestMethod]
        public void Sum()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();
                var itens = _ciEmails_ReenvioRepository
                    .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                        (em, anx) => 
                            Columns(
                                Count(anx.CiEmails_Reenvio_Id),
                                Sum(anx.Tipo),
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
                    .Execute()
                    .GetDtRetorno();

                transaction.Commit();
            });
        }

        [TestMethod]
        public void TestNovo()
        {
            ////var query = _ciEmails_ReenvioRepository
            ////        .Select(
            ////            a => Columns(
            ////                    Alias(Count(a), "Count"),
            ////                    Alias(a.Created_At, "Data")
            ////            )
            ////        )
            ////        .Where(a => a.Status == 1)
            ////        .GetQuery();


            var query2 = _ciEmails_ReenvioRepository
                .Select(
                        a => Columns(
                                Alias(Count(a), "Count"),
                                a.Created_At,
                                Alias(Concat("1 Email", a.EmailTo, "2 email", a.EmailFrom), "Concat")
                        )
                    )
                        .Where(a => a.Status == 1
                            && a.EmailTo == Concat("1 Email", a.EmailTo, "2 email", a.EmailFrom))
                    .GetQuery();
        }


        [TestMethod]
        public void LeftJoin()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();
                var itens = _ciEmails_ReenvioRepository
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
                    .Execute()
                    .GetDtRetorno();

                transaction.Commit();
            });
        }


        [TestMethod]
        public void INNERJOIN()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();
                var itens = _ciEmails_ReenvioRepository.UseAlias("em")
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
                    .Execute()
                    .GetDtRetorno();

                transaction.Commit();
            });
        }



        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void Min()
        {
            OnTransaction(CONEXAO, (SignTransaction transaction) =>
            {
                var email = GetEmail();
                var itens = _ciEmails_ReenvioRepository
                    .Select<CiEmails_Reenvio, CiEmails_Anexos>(
                        (em, anx) =>
                            Columns(
                                Min(anx.ID),
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
                    .Execute()
                    .GetDtRetorno();

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
