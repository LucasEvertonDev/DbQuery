using Application.Domains.DataAnnotatios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains
{
    [Table("CiDominio")]
    public class CiDominio : SignCi
    {
        [Identity]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
