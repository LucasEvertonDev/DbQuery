using Application.Domains.DataAnnotatios;
using Application.Domains.SignEst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Domains
{
    [Table("TesteDecimal")]
    public class EstTesteDecimal : SignEst
    {
        public decimal column1 { get; set; }
        public decimal column2 { get; set; }
        public decimal column3 { get; set; }
    }
}
