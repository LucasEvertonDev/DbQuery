using SIGN.Query.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Domains.SignCi
{
    [Table("CiEmails_Anexos")]
    public class CiEmails_Anexos : SignCi
    {
        [Identity]
        public int ID { get; set; }
        public string CiEmails_Reenvio_Id { get; set; }
        public string CHAVE { get; set; }
        public string VALOR { get; set; }
        public string PATH { get; set; }
        public int Tipo { get; set; }
    }
}
