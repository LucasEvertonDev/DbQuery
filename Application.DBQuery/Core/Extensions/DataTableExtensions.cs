using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DBQuery.Core.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> OfType<T>(this DataTable dt)
        {
            if (dt.Columns.Count == 1)
            {
                return dt.Rows.OfType<DataRow>()
                    .Select(dr => ChangeType(dr[0], typeof(T))).OfType<T>().ToList();
            }
            else
            {
                var columnNames = dt.Columns.Cast<DataColumn>()
                       .Select(c => c.ColumnName)
                       .ToList();
                var properties = typeof(T).GetProperties();
                return dt.AsEnumerable().Select(row =>
                {
                    var objT = Activator.CreateInstance<T>();
                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name))
                        {
                            PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                            pro.SetValue(objT, ChangeType(row[pro.Name], pI.PropertyType));
                        }
                    }
                    return objT;
                }).ToList();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static object ChangeType(object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (string.IsNullOrEmpty(value?.ToString()))
                {
                    return null;
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="list"></param>
        public static DataTable AddColluns(this DataTable dt, params string[] list)
        {
            list.ToList().ForEach(a =>
            {
                dt.Columns.Add(a);
            });
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            if (table == null)
            {
                yield break;
            }

            foreach (DataRow row in table.Rows)
            {
                IDictionary<string, object> dRow = new ExpandoObject();

                foreach (DataColumn column in table.Columns)
                {
                    var value = row[column.ColumnName];
                    dRow[column.ColumnName] = Convert.IsDBNull(value) ? null : value;
                }

                yield return dRow;
            }
        }

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public static void RenameColumn(this DataTable dt, string oldName, string newName)
        {
            dt.Columns[oldName].ColumnName = newName;
        }
    }
}