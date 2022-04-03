using SIGN.Query.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Domains.SignEst
{
    [Table("TesteDecimal")]
    public class EstTesteDecimal : SignEst
    {
        public decimal column1 { get; set; }
        public decimal column2 { get; set; }
        public decimal column3 { get; set; }
    }
}
