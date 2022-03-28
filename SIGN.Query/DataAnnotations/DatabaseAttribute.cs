﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.DataAnnotations
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
