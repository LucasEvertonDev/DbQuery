using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.DBCi
{
    [Table("CiDominio")]
    public class CiDominio : DBCi
    {
        [PrimaryKey(Identity = true)]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
