using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.DBCi
{
    [Table("CiItemDominio")]
    public class CiItemDominio : DBCi
    {
        [PrimaryKey(Identity = true)]
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Sigla { get; set; }
        public int Codigo_Dominio { get; set; }
    }
}
