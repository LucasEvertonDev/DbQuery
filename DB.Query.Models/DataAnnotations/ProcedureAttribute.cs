using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Query.Models.DataAnnotations
{
    public class ProcedureAttribute : Attribute
    {
        public string ProcedureName { get; set; }

        public ProcedureAttribute(string procedure)
        {
            ProcedureName = procedure;
        }
    }
}
