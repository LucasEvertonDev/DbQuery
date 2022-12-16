using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.DBCi
{
    [Table("CiSequencias")]
    public class CiSequencias : DBCi
    {
        public int CodigoSistema { get; set; }

        public long Num_Seq { get; set; }
    }
}
