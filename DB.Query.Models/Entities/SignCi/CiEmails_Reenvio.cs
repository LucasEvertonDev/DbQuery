using DB.Query.Models.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DB.Query.Models.Entities.DBCi
{
    [Table("CiEmails_Reenvio")]
    public class CiEmails_Reenvio : DBCi
    {
        public string ID { get; set; }

        public string EmailFrom { get; set; }

        public string EmailTo { get; set; }

        public string EmailCC { get; set; }

        public string EmailBCC { get; set; }

        public string Subject { get; set; }

        public string Source { get; set; }

        public int Status { get; set; }

        public DateTime Update_At { get; set; }
        public DateTime Created_At { get; set; }

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
