using SIGN.Query.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Domains.SignCi
{
    [Table("CiEmails_BlackList")]
    public class CiEmails_BlackList : SignCi
    {
        [Identity]
        public int ID { get; set; }
        public string EmailInvalid { get; set; }
    }
}
