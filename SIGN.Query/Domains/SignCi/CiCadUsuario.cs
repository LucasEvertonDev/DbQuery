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
        public Int32 Codigo { get; set; }
        public string CPF { get; set; }
        public string Registro { get; set; }
        public string Descricao { get; set; }
        public string SGIUser { get; set; }
        public Int32 Codigo_Departamento { get; set; }
        public Int32 Codigo_Cargo { get; set; }
        public byte ValidRCM { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string Num_Seq { get; set; }
        public string SGID_Fixo { get; set; }
        public string User_Login { get; set; }
    }
}

