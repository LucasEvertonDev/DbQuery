using DB.Query.Core.Constants;
using DB.Query.Models.Entities;

namespace DB.Query.Core.Models
{
    public class PropsAttributesModel<TEntity> where TEntity : EntityBase
    {
        public PropsAttributesModel()
        {
        }

        public string Name { get; set; }

        public bool Identity { get; set; }

        public bool PrimaryKey { get; set; }

        public object Valor { get; set; }

        public string GetFullName(string TableName)
        {
            return string.Concat(TableName, ".", Name);
        }
    }
}
