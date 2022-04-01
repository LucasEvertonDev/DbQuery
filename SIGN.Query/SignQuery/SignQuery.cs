using SIGN.Query.DataAnnotations;
using SIGN.Query.Domains;
using SIGN.Query.Extensions;
using SIGN.Query.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.SignQuery
{
    public class SignQuery<T> : ISignQuery<T> where T : SignQueryBase
    {
        public Dictionary<string, string> expressions { get; set; }
        protected Expression CurrentExpression { get; set; }
        public Type Origin { get; set; }
        protected SignTransaction _signTransaction { get; set; }
        protected List<Type> TypesSemAspas { get; set; }
        public T Domain { get; set; }
        protected string _query { get; set; }
        protected string DataBase { get; set; } = "";
        protected bool _useAlias { get; set; }
        public SignQuery()
        {
            Domain = null;
            _query = string.Empty;
            Origin = null;
            DataBase = GetDatabaseName(typeof(T));
            expressions = new Dictionary<string, string>();
            TypesSemAspas = new List<Type>()
            {
                typeof(decimal),
                typeof(decimal?),
                typeof(bool),
                typeof(bool?),
                typeof(int),
                typeof(int?),
                typeof(Int16),
                typeof(Int32),
                typeof(Int64),
                typeof(Int16?),
                typeof(Int32?),
                typeof(Int64?),
                typeof(double?),
                typeof(double),
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            _query = _query.Replace(", SELECT_CONCAT", "");
            return _query.Replace("  ", " ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetTableName(Type type, dynamic exp = null)
        {
            if (_useAlias && exp != null && VerificaPropriedade(exp, "Expression"))
            {
                return exp.Expression.Name;
            }
            if (_useAlias && exp != null && VerificaPropriedade(exp, "NodeType") && exp.NodeType == ExpressionType.Call)
            {
                return exp.Arguments[0].Expression.Name;
            }
            var displayName = type.GetCustomAttributes(typeof(TableAttribute), true).FirstOrDefault() as TableAttribute;

            return displayName != null ? displayName.TableName : type.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetPrimaryKeyName(Type type)
        {
            string key = string.Empty;
            List<PropertyInfo> infs = typeof(T).GetProperties().ToList();
            infs.ForEach(prop =>
            {
                var attr = prop.GetCustomAttributes(typeof(IdentityAttribute), false).FirstOrDefault() as IdentityAttribute;
                if (attr != null)
                {
                    key = string.Concat("OUTPUT Inserted.", prop.Name);
                }
            });
            return key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetDatabaseName(Type type)
        {
            var displayName = type.GetCustomAttributes(typeof(DatabaseAttribute), true).FirstOrDefault() as DatabaseAttribute;
            if (displayName != null)
            {
                return displayName.DatabaseName;
            }
            else
            {
                throw new Exception("Informe o nome da database");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetFullName(Type type)
        {
            return GetDatabaseName(type) + ".." + GetTableName(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<string> GetProperties()
        {
            bool insert = typeof(InsertQuery<T>) == Origin;

            List<PropertyInfo> infs = typeof(T).GetProperties().ToList();
            var list = new List<string>();

            infs.ForEach(prop =>
            {
                if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                {
                    if ((insert && prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0) || !insert)
                    {
                        var propName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute;
                        if (propName != null)
                        {
                            list.Add(string.IsNullOrEmpty(GetTableName(typeof(T))) ? propName.DisplayName : GetTableName(typeof(T)) + "." + propName.DisplayName);
                        }
                        else
                        {
                            list.Add(string.IsNullOrEmpty(GetTableName(typeof(T))) ? prop.Name : GetTableName(typeof(T)) + "." + prop.Name);
                        }
                    }
                }
            });

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetComparador(Expression expression)
        {
            string equalty = "";
            if (expression.NodeType == ExpressionType.Equal)
            {
                equalty = "=";
            }
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                equalty = "AND";
            }
            if (expression.NodeType == ExpressionType.OrElse)
            {
                equalty = "OR";
            }
            if (expression.NodeType == ExpressionType.NotEqual)
            {
                equalty = "<>";
            }
            if (expression.NodeType == ExpressionType.GreaterThan)
            {
                equalty = ">";
            }
            if (expression.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                equalty = ">=";
            }
            if (expression.NodeType == ExpressionType.LessThan)
            {
                equalty = "<";
            }
            if (expression.NodeType == ExpressionType.LessThanOrEqual)
            {
                equalty = "<=";
            }
            return equalty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected Expression GetLeftNode(Expression expression)
        {
            dynamic exp = expression;
            return ((Expression)exp.Left);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected Expression GetRightNode(Expression expression)
        {
            dynamic exp = expression;
            return ((Expression)exp.Right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        protected object TratarValor(dynamic val, bool includeAspas = false)
        {
            if (val == null || string.IsNullOrEmpty(val?.ToString()) && includeAspas)
            {
                return "NULL";
            }

            if (val.GetType() == typeof(DateTime))
            {
                return includeAspas ? $"'{val.ToString("yyyy-MM-dd HH:mm:ss")}'" : val.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (val.GetType() == typeof(bool))
            {
                return val ? 1 : 0;
            }
            else if (val.GetType() == typeof(decimal))
            {
                return val?.ToString("0.##")?.Replace(",", ".");
            }
            else if (val.GetType() == typeof(int) || val.GetType() == typeof(decimal))
            {
                return val;
            }
            else
            {
                return includeAspas ? $"'{val}'" : val;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="right"></param>
        /// <returns></returns>
        protected dynamic GetValue(Expression right)
        {
            if (right is ConstantExpression)
            {
                var rightConst = right as ConstantExpression;
                return rightConst.Value;
            }
            if (right is MemberExpression)
            {
                var objectMember = Expression.Convert(right, typeof(object));

                var getterLambda = Expression.Lambda<Func<object>>(objectMember);

                var getter = getterLambda.Compile();

                return getter();
            }
            if (right is MethodCallExpression)
            {
                dynamic r = right;
                if (VerificaPropriedade(r, "Method") && ("LIKE".Equals(r.Method.Name)
                    || "IN".Equals(r.Method.Name) || "NOT_IN".Equals(r.Method.Name)))
                {
                    return ExtractMethod(right);
                }
                else
                {
                    return Expression.Lambda(right).Compile().DynamicInvoke();
                }
            }
            if (right is UnaryExpression)
            {
                var uExp = right as UnaryExpression;
                if (uExp.Operand is ConstantExpression)
                {
                    var rightConst = uExp.Operand as ConstantExpression;
                    return rightConst.Value;
                }
                if (uExp.Operand is MemberExpression)
                {
                    var objectMember = Expression.Convert(right, typeof(object));

                    var getterLambda = Expression.Lambda<Func<object>>(objectMember);

                    var getter = getterLambda.Compile();

                    return getter();
                }
                if (uExp.Operand is MethodCallExpression)
                {
                    object result = Expression.Lambda(right).Compile().DynamicInvoke();
                    return result;
                }
            }
            if (right != null && VerificaPropriedade(right, "NodeType") && right.NodeType == ExpressionType.Call)
            {
                return Expression.Lambda(right).Compile().DynamicInvoke();
            }

            return "";
        }

        /// <summary>
        /// /
        /// </summary>
        protected void VerifyChangeDataBase()
        {
            if (_signTransaction != null)
            {
                var databaseName = GetDatabaseName(typeof(T));
                if (!databaseName.Equals(_signTransaction.GetConnection().Database))
                {
                    _signTransaction.ChangeDatabase(databaseName);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataBaseService"></param>
        public void BindTransaction(SignTransaction dataBaseService)
        {
            this._signTransaction = dataBaseService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected P CreateDbQuery<P>() where P : SignQuery<T>
        {
            P obj = Activator.CreateInstance<P>();
            obj.SetDefaultFields(this.Domain, this.Origin);
            if (!string.IsNullOrEmpty(this._query))
            {
                obj._query = this._query;
            }
            VerifyChangeDataBase();
            obj._useAlias = this._useAlias;
            obj._signTransaction = this._signTransaction;
            return obj;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ResultQuery<T> Execute()
        {
            var exec = CreateDbQuery<ExecuteQuery<T>>();
            return exec.Execute();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void SetDefaultFields(T domain, Type origin)
        {
            this.Domain = domain;
            this.Origin = origin;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<string> GetObjectClausules()
        {
            var list = new List<string>();
            Domain.GetType().GetProperties().ToList().ForEach(prop =>
            {
                if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0 && prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0)
                {
                    var val = TratarValor((dynamic)prop.GetValue(Domain), true);
                    list.Add(prop.Name + " = " + val?.ToString());
                }
            });

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected List<string> GetPropertiesExpression<P>(params Expression<Func<P, dynamic>>[] expression)
        {
            var properties = new List<string>();
            if (expression != null)
            {
                expression.ToList().ForEach(a =>
                {
                    if (a.Body.NodeType == ExpressionType.MemberAccess)
                    {
                        dynamic d = a.Body;
                        properties.Add(GetTableName(d.Expression.Type, d) + "." + GetCollumnName(d.Member));
                    }
                    if (a.Body.NodeType == ExpressionType.Convert)
                    {
                        dynamic body = a;
                        dynamic d = body.Body.Operand;
                        properties.Add(GetTableName(d.Expression.Type, d) + "." + GetCollumnName(d.Member));
                    }
                    if (a.Body.NodeType == ExpressionType.Call)
                    {
                        dynamic exp = a;
                        if (exp.Body.Method.Name == "Count")
                        {
                            var d = exp.Body.Arguments[0];
                            properties.Add(String.Format("Count({0})", GetTableName(d.Expression.Type, d) + "." + GetCollumnName(d.Member)));
                        }
                    }
                });
            }
            return properties;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        protected string GetCollumnName(PropertyInfo prop)
        {
            var att1 = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            var name1 = att1.Length > 0 ? (att1[0] as DisplayNameAttribute).DisplayName : prop.Name;
            return name1;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        protected string ExtractMethod(Expression method)
        {
            string valueA = "";
            string valueB = "";
            string comparador = "";
            if (method is MethodCallExpression)
            {
                var mtd = (MethodCallExpression)method;
                if ("LIKE".Equals(mtd.Method.Name))
                {
                    comparador = "LIKE";
                }
                else if (mtd.Method.Name.Equals("IN"))
                {
                    comparador = "IN";
                }
                else if (mtd.Method.Name.Equals("NOT_IN"))
                {
                    comparador = "NOT IN";
                }

                if (mtd.Arguments.Count > 0)
                {
                    dynamic arg = mtd.Arguments[1];
                    if (arg != null && VerificaPropriedade(arg, "Member") && VerificaPropriedade(arg, "NodeType") && arg.NodeType == ExpressionType.MemberAccess)
                    {
                        if ("LIKE".Equals(mtd.Method.Name))
                        {
                            valueB = $"'%{GetValue(arg)}%'";
                        }
                        else if ("IN".Equals(mtd.Method.Name) || "NOT_IN".Equals(mtd.Method.Name))
                        {
                            valueB = $"{GetValue(arg)}";
                        }
                        else
                        {
                            valueB = $"'{GetValue(arg)}'";
                        }
                    }
                    else if (arg.GetType().Name.Equals("PropertyExpression"))
                    {
                        valueB = GetTableName(arg.Member.DeclaringType, mtd) + "." + GetCollumnName(arg.Member);
                    }
                    else if (arg.GetType().Name.Equals("ConstantExpression"))
                    {
                        valueB = "LIKE".Equals(mtd.Method.Name) ? $"'%{arg.Value}%'" : $"'{arg.Value}'";
                    }
                    else if (VerificaPropriedade(arg, "NodeType") && arg.NodeType == ExpressionType.Call)
                    {
                        valueB = ("IN".Equals(comparador) || "NOT IN".Equals(comparador)) ? $"{GetValue(arg)}" : $"'%{GetValue(arg)}%'";
                    }

                    arg = mtd.Arguments[0];
                    if (VerificaPropriedade(arg, "Expression") && VerificaPropriedade(arg, "Type"))
                    {
                        valueA = GetTableName(arg.Expression.Type, mtd) + "." + GetCollumnName(arg.Member);
                    }
                }
            }

            return string.Format("{0} {1} {2}", valueA, comparador, valueB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected string GetConditionsJoin(Expression expression)
        {
            if ((expression is LambdaExpression) && ((dynamic)expression).Parameters.Count > 1)
            {
                expression = ((dynamic)expression).Body;
            }

            bool ignore = false;
            string value = "", oldExpression = "", equalty = "";
            Expression left = null, right = null;

            if (VerificaPropriedade(expression, "Left"))
            {
                left = GetLeftNode(expression);
            }

            if (VerificaPropriedade(expression, "Right"))
            {
                right = GetRightNode(expression);
            }

            if (VerificaPropriedade(expression, "NodeType"))
            {
                equalty = GetComparador(expression);
            }

            if (left is MemberExpression)
            {
                var isValue = IsValue(left);
                if (isValue)
                {
                    var val = TratarValor(GetValue(left), true);
                    oldExpression = expression.ToString();
                    value = string.Format("{0} {1} {2}", val, "{0}", "{1}");
                }
                else
                {
                    var leftMem = left as MemberExpression;
                    var propertyInfo = (PropertyInfo)leftMem.Member;
                    var name = GetCollumnName(propertyInfo);

                    if (propertyInfo.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                    {
                        oldExpression = expression.ToString();
                        value = string.Format("{0} {1} {2}", GetTableName(propertyInfo.DeclaringType, left) + "." + name, "{0}", "{1}");
                    }
                    else
                    {
                        ignore = true;
                    }
                }
            }
            else if (left == null)
            {
                oldExpression = expression.ToString();
                value = ExtractMethod(expression);
            }

            if (value == "" && !ignore)
            {
                var leftVal = GetConditionsJoin(left);
                var rigthVal = GetConditionsJoin(right);

                value = string.Format("{0} {1} {2}", leftVal, equalty, rigthVal);
            }
            else
            {
                if (right is MemberExpression)
                {
                    var rightMen1 = right as MemberExpression;
                    dynamic r = right;
                    bool isValue = true;

                    isValue = IsValue(r);

                    if (!isValue)
                    {
                        var propertyInfo1 = (PropertyInfo)rightMen1.Member;

                        var name1 = GetCollumnName(propertyInfo1);

                        var val = GetTableName(propertyInfo1.DeclaringType, rightMen1) + "." + name1;

                        value = string.Format(value, equalty, val);
                    }
                    else
                    {
                        value = ValidateValue(right, equalty, value);
                    }
                }
                else
                {
                    value = ValidateValue(right, equalty, value);
                }

                if (!string.IsNullOrEmpty(oldExpression) && !string.IsNullOrEmpty(value) && !expressions.ContainsKey(oldExpression))
                {
                    expressions.Add(oldExpression, value);
                }
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool IsValue(dynamic expression)
        {
            var right = expression;
            dynamic r = right;
            dynamic exp = CurrentExpression;
            bool isValue = true;

            if (CurrentExpression != null && VerificaPropriedade(exp, "Parameters") && exp.Parameters != null)
            {
                foreach (var e in exp.Parameters)
                {
                    if (VerificaPropriedade(r, "Expression") && r.Expression != null && VerificaPropriedade(r.Expression, "Member"))
                    {
                        if (r.Expression.Member != null && e.Name == r.Expression.Member.Name)
                        {
                            isValue = false;
                            break;
                        }
                    }
                    else if (VerificaPropriedade(r, "Expression") && r.Expression != null && VerificaPropriedade(r.Expression, "Name"))
                    {
                        if (e.Name == r.Expression.Name)
                        {
                            isValue = false;
                            break;
                        }
                    }
                }
            }

            return isValue;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="right"></param>
        /// <param name="equality"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string ValidateValue(Expression right, string equality, string value)
        {
            var val = TratarValor(GetValue(right), true);
            if ("NULL".Equals(val))
            {
                if ("=".Equals(equality))
                {
                    equality = "IS";
                }
                else if ("<>".Equals(equality))
                {
                    equality = "IS NOT";
                }
            }
            return string.Format(value, equality, val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="NomeDaPropriedade"></param>
        /// <returns></returns>
        protected bool VerificaPropriedade(object obj, string NomeDaPropriedade)
        {
            return obj.GetType().GetProperty(NomeDaPropriedade) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tipo"></param>
        protected SelectExecuteQuery<T> AddOrderBy<P>(string tipo, params Expression<Func<P, dynamic>>[] expressions)
        {
            bool contains = _query.Contains("ORDER BY");
            if (!contains)
            {
                _query += " ORDER BY ";
            }
            if (expressions != null)
            {
                var properties = GetPropertiesExpression<P>(expressions);
                if (contains && properties.Count > 0)
                {
                    _query += ", " + string.Join($" {tipo}, ", properties) + $" {tipo}";
                }
                else
                {
                    _query += string.Join($" {tipo}, ", properties) + $" {tipo}";
                }
            }

            return CreateDbQuery<SelectExecuteQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conditions"></param>
        protected SelectExecuteQuery<T> IncludeWhereConditions(Expression expression)
        {
            string condition = "";
            this.CurrentExpression = expression;
            if (expression != null)
            {
                var x = new List<string> { "=>" };
                string aux = ((Expression)((dynamic)expression).Body).ToString();
                string exp = aux.Split(x.ToArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                condition = GetConditionsJoin(((dynamic)expression).Body);

                if (expressions.Keys.ToList().Exists(a => exp.Contains(a)))
                {
                    expressions.Keys.ToList().ForEach(key =>
                    {
                        exp = exp.Replace(key, expressions[key]);
                    });

                    exp = exp.Replace("AndAlso", "AND").Replace("OrElse", "OR");

                    _query += " WHERE " + exp;
                }
                else
                {
                    _query += " WHERE " + condition;
                }
            }

            return CreateDbQuery<SelectExecuteQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <param name="strJoin"></param>
        /// <returns></returns>
        public JoinQuery<T> IncludeJoinOnQuery<J, P>(Expression expression, string strJoin)
        {
            this.Origin = typeof(JoinQuery<T>);
            this.CurrentExpression = expression;
            var aux = _useAlias ? GetFullName(typeof(P)) + " AS " + ((dynamic)expression).Parameters[1].Name : GetFullName(typeof(P));
            _query += string.Format(strJoin, aux, this.GetConditionsJoin(expression));
            return CreateDbQuery<JoinQuery<T>>();
        }
    }
}
