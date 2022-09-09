using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Query.Models.DataAnnotations
{
    public class ColumnAttribute : DisplayNameAttribute
    {
        public ColumnAttribute(string displayName) : base(displayName)
        { 
        
        }
    }
}
