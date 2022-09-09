using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Query.Models.DataAnnotations
{
    public class PrimaryKeyAttribute : Attribute
    {
        public bool Identity { get; set; }

        public PrimaryKeyAttribute()
        {
        }
    }
}
