using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.DBCi
{
    [Table("CiEmails_Anexos")]
    public class CiEmails_Anexos : DBCi
    {
        [PrimaryKey(Identity = true)]
        public int ID { get; set; }
        public string CiEmails_Reenvio_Id { get; set; }
        public string CHAVE { get; set; }
        public string VALOR { get; set; }
        public string PATH { get; set; }
        public int Tipo { get; set; }
    }
}
