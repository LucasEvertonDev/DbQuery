using Microsoft.CSharp;
using SIGN.Query.Domains;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Retorna o primeiro Data row da datatable
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataRow First(this DataTable dt)
        {
            return dt.AsEnumerable().First();
        }

        /// <summary>
        /// Retorna uma lista de DataRows
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<DataRow> ToList(this DataTable dt)
        {
            return dt.AsEnumerable().ToList();
        }

        /// <summary>
        /// Realiza o update do table name e retorna a datatable 
        /// </summary>
        /// <returns></returns>
        public static DataTable SetTableName(this DataTable dt, string Name)
        {
            if (dt != null)
                dt.TableName = Name;
            return dt;
        }

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
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> self)
        {
            var properties = typeof(T).GetProperties();
            var dataTable = new DataTable();
            foreach (var info in properties)
            {
                dataTable.Columns.Add(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType);
            }

            foreach (var entity in self)
            {
                dataTable.Rows.Add(properties.Select(p => p.GetValue(entity)).ToArray());
            }

            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable CreateColumns(this DataTable dataTable, params DataColumn[] list)
        {
            foreach (var d in list)
            {
                dataTable.Columns.Add(d.ColumnName, d.DataType);
            }
            return dataTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="list"></param>
        public static DataTable CreateColluns(this DataTable dt, params string[] list)
        {
            list.ToList().ForEach(a =>
            {
                dt.Columns.Add(a);
            });
            return dt;
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this DataTable dt)
        {
            return (dt?.Rows?.Count ?? 0) <= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Table"></param>
        /// <returns></returns>
        public static string DataTableToCode(this DataTable Table, string TableName = "")
        {
            string className = TableName;
            if (string.IsNullOrWhiteSpace(className))
            {   // Default name
                className = "Unnamed";
            }
            className += "TableAsClass";

            // Create the class
            CodeTypeDeclaration codeClass = CreateClass(className);

            // Add public properties
            foreach (DataColumn column in Table.Columns)
            {
                codeClass.Members.Add(CreateProperty(column.ColumnName, column.DataType));
            }

            // Add Class to Namespace
            string namespaceName = NameOfCallingClass();
            CodeNamespace codeNamespace = new CodeNamespace(namespaceName);
            codeNamespace.Types.Add(codeClass);

            StringWriter writer = new StringWriter();
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();

            codeProvider.GenerateCodeFromNamespace(codeNamespace, writer, new System.CodeDom.Compiler.CodeGeneratorOptions() { BracingStyle = "C", BlankLinesBetweenMembers = false });

            // Return filename
            return writer.ToString().Replace("};", "}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static CodeTypeDeclaration CreateClass(string name)
        {
            /// https://docs.microsoft.com/en-us/dotnet/framework/reflection-and-codedom/how-to-create-a-class-using-codedom
            CodeTypeDeclaration result = new CodeTypeDeclaration(name);
            result.Attributes = MemberAttributes.Public;
            result.Members.Add(CreateConstructor(name)); // Add class constructor
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <returns></returns>
        static CodeConstructor CreateConstructor(string className)
        {
            CodeConstructor result = new CodeConstructor();
            result.Attributes = MemberAttributes.Public;
            result.Name = className;
            return result;
        }

        static string NameOfCallingClass()
        {
            string fullName;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static CodeMemberField CreateProperty(string name, Type type)
        {
            if (type == typeof(int))
            {
                type = typeof(int?);
            }
            else if (type == typeof(decimal))
            {
                type = typeof(decimal?);
            }
            else if (type == typeof(long))
            {
                type = typeof(long?);
            }
            else if (type == typeof(DateTime))
            {
                type = typeof(DateTime?);
            }
            else if (type == typeof(double))
            {
                type = typeof(double?);
            }
            else if (type == typeof(bool))
            {
                type = typeof(bool?);
            }

            string memberName = name + " { get; set; }";

            CodeMemberField result = new CodeMemberField(type, memberName);
            result.Comments.Add(new CodeCommentStatement("<summary>", true));
            result.Comments.Add(new CodeCommentStatement("Propiedade mapeada para a definição de " + name, true));
            result.Comments.Add(new CodeCommentStatement("</summary>", true));
            result.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            return result;
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
    }
}