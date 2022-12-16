using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.DBCi
{
    [Table("CiEmails_BlackList")]
    public class CiEmails_BlackList : DBCi
    {
        [PrimaryKey(Identity = true)]
        public int ID { get; set; }
        public string EmailInvalid { get; set; }
    }
}
