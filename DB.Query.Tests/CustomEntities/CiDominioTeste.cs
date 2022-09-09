using DB.Query.Models.DataAnnotations;
using DB.Query.Models.Entities.SignCi;

namespace DB.Query.Tests.CustomEntities
{
    [Table("CiDominio")]
    public class CiDominioTeste : SignCi
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
