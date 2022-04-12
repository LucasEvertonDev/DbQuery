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

namespace Console
{
    public class Ajuste : SignQueryService
    {
        protected string CONEXAO = @"Data Source=LAPTOP-JGT9FNST\SQLEXPRESS;Integrated Security=True";
        protected Repository<CiEmails_Reenvio> _ciEmails_ReenvioRepository { get; set; }
        protected Repository<CiEmails_Anexos> _ciEmailsAnexos_Repository { get; set; }


        public Ajuste()
        { 
            this._ciEmailsAnexos_Repository = new Repository<CiEmails_Anexos>();

            this._ciEmails_ReenvioRepository = new Repository<CiEmails_Reenvio>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static CiEmails_Reenvio GetEmail()
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

                transaction.Commit();
            });
        }
    }
}
