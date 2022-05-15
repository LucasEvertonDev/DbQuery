using Application.Domains.DataAnnotatios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domains.Entities
{
    [Database("Master")]
    [Table("main")]
    public class EntityBase
    {
        /// <summary>
        /// *
        /// </summary>
        /// <returns></returns>
        [Ignore]
        public object AllColumns()
        {
            return null;
        }
    }
}
