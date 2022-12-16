using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities.DBCi
{
    [Table("CICadUsuario")]
    public class CICadUsuario : DBCi
    {
        public int? Codigo { get; set; }
        public string CPF { get; set; }
        public string Registro { get; set; }
        public string Descricao { get; set; }
        public string SGIUser { get; set; }
        public int? Codigo_Departamento { get; set; }
        public int? Codigo_Cargo { get; set; }
        public byte? ValidRCM { get; set; }
        public string Email { get; set; }
        public bool? Ativo { get; set; }
        public string Num_Seq { get; set; }
        public string SGID_Fixo { get; set; }
        public string User_Login { get; set; }
    }
}

