using DB.Query.Core.Models;
using DB.Query.Models.DataAnnotations;
using DB.Query.Models.Entities;
using System.Linq;
using System.Reflection;

namespace DB.Query.Core.Factorys
{
    public class EntityAttributesModelFactory<TEntity> where TEntity : EntityBase
    {
        public EntityAttributesModelFactory() { }

        public EntityAttributesModel<TEntity> InterpretEntity(TEntity entity, bool getValues, EntityAttributesModel<TEntity> entityAttributes = null)
        {
            if(entityAttributes != null)
            {
                return entityAttributes;
            }

            var retorno = new EntityAttributesModel<TEntity>();
            var currentType = typeof(TEntity);

            DatabaseAttribute databaseAttr = currentType.GetCustomAttributes<DatabaseAttribute>().FirstOrDefault();
            TableAttribute tableAttr = currentType.GetCustomAttributes<TableAttribute>().FirstOrDefault();

            retorno.Database = databaseAttr.DatabaseName;
            retorno.Name = tableAttr != null ? tableAttr.TableName : currentType.Name;

            var props = currentType.GetProperties();

            for (var i = 0; i < props.Count(); i ++)
            {
                var prop = props[i];
                if (prop.GetCustomAttributes<IgnoreAttribute>().Count() == 0)
                {
                    var propInfo = new PropsAttributesModel<TEntity>();
                    var propName = prop.GetCustomAttributes(typeof(ColumnAttribute), false).FirstOrDefault() as ColumnAttribute;
                    var primaryKey = prop.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).FirstOrDefault() as PrimaryKeyAttribute;

                    propInfo.Name = propName != null ? propName.DisplayName : prop.Name;
                    if (primaryKey != null)
                    {
                        propInfo.PrimaryKey = true;
                        propInfo.Identity = primaryKey.Identity;
                    }

                    if (getValues)
                    {
                        propInfo.Valor = prop.GetValue(entity);
                    }

                    retorno.Props.Add(propInfo);
                }
            }
            return retorno;
        }
    }
}
