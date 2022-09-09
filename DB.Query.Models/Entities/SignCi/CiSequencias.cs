using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.SignCi
{
    [Table("CiSequencias")]
    public class CiSequencias : SignCi
    {
        public int CodigoSistema { get; set; }

        public long Num_Seq { get; set; }
    }
}
