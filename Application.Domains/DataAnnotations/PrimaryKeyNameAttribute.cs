using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains.DataAnnotatios
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
