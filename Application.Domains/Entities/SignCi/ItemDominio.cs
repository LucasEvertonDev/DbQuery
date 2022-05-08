using Application.Domains.DataAnnotatios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains
{
    [Table("CiItemDominio")]
    public class ItemDominio : SignCi
    {
        [Identity]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public int Codigo_Dominio { get; set; }
    }
}
