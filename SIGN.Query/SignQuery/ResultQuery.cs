using SIGN.Utilitarios.Dominios.Mapeamentos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class ResultQuery<T>
    {
        public ResultQuery()
        {
            this.Status = DbStatus.OK;
        }

        public DataTable Retorno { get; set; }
        public List<T> Itens { get; set; }
        public dynamic Output { get; set; }
        public DbStatus Status { get; set; }
        public string Message { get; set; }
    }
}
