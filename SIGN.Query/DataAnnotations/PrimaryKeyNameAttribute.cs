using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.DataAnnotations
{ 
    public class PrimaryKeyNameAttribute : Attribute
    {
        public string PrimaryKeyName { get; set; }

        public PrimaryKeyNameAttribute(string primaryKey)
        {
            this.PrimaryKeyName = primaryKey;
        }
    }
}
