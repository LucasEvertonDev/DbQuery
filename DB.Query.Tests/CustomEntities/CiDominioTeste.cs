using DB.Query.Models.DataAnnotations;
using DB.Query.Models.Entities.DBCi;

namespace DB.Query.Tests.CustomEntities
{
    [Table("CiDominio")]
    public class CiDominioTeste : DBCi
    {
        [PrimaryKey(Identity = true)]
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ok { get; set; }
        public bool Ok1 { get; set; }
        public bool? Ok2 { get; set; }
        public bool? OK3 { get; set; }
    }
}
