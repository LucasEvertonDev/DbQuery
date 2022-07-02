using Application.Domains.DataAnnotatios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains
{
    [Table("CiEmails_Anexos")]
    public class CiEmails_Anexos : SignCi
    {
        [Identity]
        public int ID { get; set; }
        public int CiEmails_Reenvio_Id { get; set; }
        public string CHAVE { get; set; }
        public string VALOR { get; set; }
        public string PATH { get; set; }
        public int Tipo { get; set; }
    }
}
