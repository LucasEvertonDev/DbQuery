using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.SignCi
{
    [Table("CiDominio")]
    public class CiDominio : SignCi
    {
        [PrimaryKey(Identity = true)]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
