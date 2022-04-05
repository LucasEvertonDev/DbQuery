using SIGN.Query.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Domains.SignCi
{
    [Table("CICadUsuario")]
    public class CICadUsuario : SignCi
    {
        public Nullable<Int32> Codigo { get; set; }
        public string CPF { get; set; }
        public string Registro { get; set; }
        public string Descricao { get; set; }
        public string SGIUser { get; set; }
        public Nullable<Int32> Codigo_Departamento { get; set; }
        public Nullable<Int32> Codigo_Cargo { get; set; }
        public Nullable<byte> ValidRCM { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Ativo { get; set; }
        public string Num_Seq { get; set; }
        public string SGID_Fixo { get; set; }
        public string User_Login { get; set; }
    }
}

