using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.SignCi
{
    [Table("CiEmails_BlackList")]
    public class CiEmails_BlackList : SignCi
    {
        [PrimaryKey(Identity = true)]
        public int ID { get; set; }
        public string EmailInvalid { get; set; }
    }
}
