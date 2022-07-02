using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains.DataAnnotatios
{
    public class DatabaseAttribute : Attribute
    {
        public string DatabaseName { get; set; }

        public DatabaseAttribute(string databaseName)
        {
            DatabaseName = databaseName;
        }
    }
}
