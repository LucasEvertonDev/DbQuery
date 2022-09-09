using DB.Query.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Query.Models.DataAnnotations
{
    public class ConfigurationAttribute : Attribute
    {
        public ConfigurationInputs Configuration { get; set; }
        public ConfigurationAttribute(ConfigurationInputs configurationInputs)
        {
            Configuration = configurationInputs;
        }
    }
}
