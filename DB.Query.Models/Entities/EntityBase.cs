using DB.Query.Models.DataAnnotations;

namespace DB.Query.Models.Entities
{
    [Database("Master")]
    [Table("main")]
    public class EntityBase : IEntityBase
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
