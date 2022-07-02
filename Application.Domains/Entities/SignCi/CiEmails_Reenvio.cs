using Application.Domains.DataAnnotatios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains
{
    [Table("CiEmails_Reenvio")]
    public class CiEmails_Reenvio : SignCi
    {
        [Identity]
        public int ID { get; set; }

        public string EmailFrom { get; set; }

        public string EmailTo { get; set; }

        public string EmailCC { get; set; }
       
        public string EmailBCC { get; set; }

        public string Subject { get; set; }

        public string Source { get; set; }

        public Nullable<int> Status { get; set; }
    
        public Nullable<DateTime> Update_At { get; set; }

        public Nullable<DateTime> Created_At { get; set; }

        [Ignore]
        public List<string> Bcc
        {
            get
            {
                return EmailBCC?.Split(';').Distinct().ToList();
            }
        }

        [Ignore]
        public List<string> Cc
        {
            get
            {
                return EmailCC?.Split(';').Distinct().ToList();
            }
        }

        [Ignore]
        public List<string> To
        {
            get
            {
                return EmailTo?.Split(';').Distinct().ToList();
            }
        }

        [Ignore]
        public List<CiEmails_Anexos> ListCiEmails_Anexos { get; set; }
    }
}
