using SIGN.Query.Constants;
using SIGN.Query.DataAnnotations;
using SIGN.Query.Domains;
using SIGN.Query.Extensions;
using SIGN.Query.Repository;
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
        private Dictionary<string, string> _expressions { get; set; }
        private Expression _currentExpression { get; set; }
        protected bool _isScalar { get; set; }
        protected SignTransaction _signTransaction { get; set; }
        protected List<Type> _typesWithOutQuotes { get; set; }
        protected T _domain { get; set; }
        protected string _query { get; set; }
        protected string _dataBase { get; set; }
        protected bool _useAlias { get; set; }
        protected List<string> _defaultFunctions { get; set; }
        protected string _alias { get; set; }
        protected Expression _customExpression { get; set; }
        public SignQuery()
        {
            _domain = null;
            _query = string.Empty;
            _dataBase = GetDatabaseName(typeof(T));
            _expressions = new Dictionary<string, string>();
            _typesWithOutQuotes = new List<Type>()
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
            _defaultFunctions = new List<string>()
            {
                DbQueryConstants.SUM_FUNCTION,
                DbQueryConstants.MAX_FUNCTION, 
                DbQueryConstants.MIN_FUNCTION,
                DbQueryConstants.COUNT_FUNCTION
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetQuery()
        {
            while (_query.Contains("  "))
            {
                _query = _query.Replace("  ", " ");
            }
            return _query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetTableName(Type type, dynamic exp = null)
        {
            if (_useAlias && exp != null && ContainsProperty(exp, "Expression"))
            {
                return exp.Expression.Name;
            }
            if (_useAlias && exp != null && ContainsProperty(exp, "NodeType") && exp.NodeType == ExpressionType.Call)
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
                    key = string.Concat(SQLKeys.OUTPUT_INSERTED, prop.Name);
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
            return string.Concat(GetDatabaseName(type), SQLKeys.T_A, GetTableName(type));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<string> GetProperties()
        {
            bool insert = this.GetType() == typeof(InsertQuery<T>) || this.GetType() == typeof(InsertNotExistsQuery<T>); ;

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
                            list.Add(string.IsNullOrEmpty(GetTableName(typeof(T))) ? propName.DisplayName : GetTableName(typeof(T)) + SQLKeys.SINGLE_POINT + propName.DisplayName);
                        }
                        else
                        {
                            list.Add(string.IsNullOrEmpty(GetTableName(typeof(T))) ? prop.Name : GetTableName(typeof(T)) + SQLKeys.SINGLE_POINT + prop.Name);
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
            string equalty = string.Empty;
            if (expression.NodeType == ExpressionType.Equal)
            {
                equalty = SQLKeys.EQUALS;
            }
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                equalty = SQLKeys.AND;
            }
            if (expression.NodeType == ExpressionType.OrElse)
            {
                equalty = SQLKeys.OR;
            }
            if (expression.NodeType == ExpressionType.NotEqual)
            {
                equalty = SQLKeys.NOT_EQUAL;
            }
            if (expression.NodeType == ExpressionType.GreaterThan)
            {
                equalty = SQLKeys.GREATER_THAN;
            }
            if (expression.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                equalty = SQLKeys.GREATER_THAN_OR_EQUAL;
            }
            if (expression.NodeType == ExpressionType.LessThan)
            {
                equalty = SQLKeys.LESS_THAN; ;
            }
            if (expression.NodeType == ExpressionType.LessThanOrEqual)
            {
                equalty = SQLKeys.LESS_THAN_OR_EQUAL;
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
        protected object TreatValue(dynamic val, bool useQuotes = false)
        {
            if (val == null || string.IsNullOrEmpty(val?.ToString()) && useQuotes)
            {
                return SQLKeys.NULL;
            }

            if (val.GetType() == typeof(DateTime))
            {
                return useQuotes ? $"'{val.ToString(SQLKeys.DATE_FORMAT)}'" : val.ToString(SQLKeys.DATE_FORMAT);
            }
            else if (val.GetType() == typeof(bool))
            {
                return val ? 1 : 0;
            }
            else if (val.GetType() == typeof(decimal))
            {
                return val?.ToString(SQLKeys.DECIMAL_FORMAT)?.Replace(",", ".");
            }
            else if (val.GetType() == typeof(int) || val.GetType() == typeof(decimal))
            {
                return val;
            }
            else
            {
                return useQuotes ? $"'{val}'" : val;
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
                if (ContainsProperty(r, "Method") && ("LIKE".Equals(r.Method.Name)
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
            if (right != null && ContainsProperty(right, "NodeType") && right.NodeType == ExpressionType.Call)
            {
                return Expression.Lambda(right).Compile().DynamicInvoke();
            }

            return string.Empty;
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
            obj.SetDefaultFields(this._domain, this._isScalar);
            if (!string.IsNullOrEmpty(this._query))
            {
                obj._query = this._query;
            }
            VerifyChangeDataBase();
            obj._alias = this._alias;
            obj._useAlias = this._useAlias;
            obj._signTransaction = this._signTransaction;
            if (typeof(Repository<T>) == this.GetType())
            {
                this._alias = null;
            }
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
        protected virtual void SetDefaultFields(T domain, bool isScalar)
        {
            this._domain = domain;
            this._isScalar = isScalar;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<string> GetObjectClausules()
        {
            var list = new List<string>();
            _domain.GetType().GetProperties().ToList().ForEach(prop =>
            {
                if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0 && prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0)
                {
                    var val = TreatValue((dynamic)prop.GetValue(_domain), true);
                    list.Add(string.Concat(prop.Name, SQLKeys.EQUALS_WITH_SPACE, val?.ToString()));
                }
            });

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected List<string> GetPropertiesExpression(Expression expression, bool useAlias = false)
        {
            var properties = new List<string>();
            dynamic exp = expression;
            if (expression.Type == typeof(Func<T, dynamic>))
            {
                properties.Add(GetPropertyOfSingleExpression(expression, false, useAlias));
            }
            else
            {
                if (ContainsProperty(exp.Body, "Arguments"))
                {
                    foreach (var a in exp.Body.Arguments[0].Expressions)
                    {
                        properties.Add(GetPropertyOfSingleExpression(a, false, useAlias));
                    }
                }
                else
                {
                    properties.Add(GetPropertyOfSingleExpression(exp, false, useAlias));
                }
            }
           
            return properties;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="hasParamter"></param>
        /// <returns></returns>
        protected string GetPropertyOfSingleExpression(dynamic expression, bool hasParamter, bool useAlias)
        {
            if (expression != null)
            {
                var exp = ContainsProperty(expression, "Body") ? expression?.Body : expression;
                if (exp.NodeType == ExpressionType.MemberAccess)
                {
                    return GetPropretyFullName(exp.Expression.Type, exp);
                }
                if (exp.NodeType == ExpressionType.Convert)
                {
                    return GetPropretyFullName(exp.Operand.Expression.Type, exp.Operand);
                }
                if (exp.NodeType == ExpressionType.Call)
                {
                    if (_defaultFunctions.Exists(f => f.Equals(exp.Method.Name)))
                    {
                        if (DbQueryConstants.COUNT_FUNCTION.Equals(exp.Method.Name) && !hasParamter)
                        {
                            return useAlias 
                                    ? string.Concat(SQLKeys.COUNT, SQLKeys.AS_WITH_SPACE, DbQueryConstants.COUNT_FUNCTION)
                                    : SQLKeys.COUNT;
                        }
                        else
                        {
                            if (ContainsProperty(exp, "Body"))
                            {
                                var d = exp.Body.Arguments[0];
                                var aux = d.Expressions[0].Arguments[0].Operand;
                                return useAlias
                                    ? string.Format(string.Concat(exp.Method.Name, "({0}) {1} {2}"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member), SQLKeys.AS, exp.Method.Name + "_" + GetCollumnName(aux.Member))
                                    : string.Format(string.Concat(exp.Method.Name, "({0})"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member));
                            }
                            else
                            {
                                var d = exp.Arguments[0];
                                if (ContainsProperty(d, "Expressions"))
                                {
                                    var aux = d.Expressions[0].Arguments[0].Operand;
                                    return useAlias
                                        ? string.Format(string.Concat(exp.Method.Name, "({0}) {1} {2}"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member), SQLKeys.AS, exp.Method.Name + "_" + GetCollumnName(aux.Member))
                                        : string.Format(string.Concat(exp.Method.Name, "({0})"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member));
                                }
                                else 
                                {
                                    var aux = d.Operand;
                                    return useAlias
                                        ? string.Format(string.Concat(exp.Method.Name, "({0}) {1} {2}"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member), SQLKeys.AS, exp.Method.Name + "_" + GetCollumnName(aux.Member))
                                        : string.Format(string.Concat(exp.Method.Name, "({0})"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member));
                                }
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetPropretyFullName(Type type, dynamic exp, dynamic member = null)
        {
            return string.Concat(GetTableName(type, exp), SQLKeys.SINGLE_POINT, GetCollumnName(member == null ? exp.Member : member));
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
            string valueA = string.Empty;
            string valueB = string.Empty;
            string comparador = string.Empty;
            if (method is MethodCallExpression)
            {
                var mtd = (MethodCallExpression)method;
                comparador = TreatComparerByMethod(mtd.Method.Name);

                if (mtd.Arguments.Count > 0)
                {
                    List<dynamic> values = new List<dynamic>();
                    for (var i = 0; i < mtd.Arguments.Count; i++)
                    {
                        bool isValue = true;
                        string value = string.Empty;
                        if (IsValue(mtd.Arguments[i]))
                        {
                            dynamic arg = mtd.Arguments[i];
                            if (arg != null && ContainsProperty(arg, "Member") && ContainsProperty(arg, "NodeType") && arg.NodeType == ExpressionType.MemberAccess)
                            {
                                value = GetValue(arg);
                            }
                            else if (arg.GetType().Name.Equals("PropertyExpression"))
                            {
                                value = string.Concat(GetTableName(arg.Member.DeclaringType, mtd), SQLKeys.SINGLE_POINT, GetCollumnName(arg.Member));
                            }
                            else if (arg.GetType().Name.Equals("ConstantExpression"))
                            {
                                value = arg.Value;
                            }
                            else if (ContainsProperty(arg, "NodeType") && arg.NodeType == ExpressionType.Call)
                            {
                                value = GetValue(arg);
                            }
                        }
                        else
                        {
                            isValue = false;
                            dynamic arg = mtd.Arguments[i];
                            if (ContainsProperty(arg, "Expression") && ContainsProperty(arg, "Type"))
                            {
                                value = string.Concat(GetTableName(arg.Expression.Type, mtd), SQLKeys.SINGLE_POINT, GetCollumnName(arg.Member));
                            }
                        }

                        values.Add(new { value = value, isValue = isValue });
                    }

                    if (values.Any())
                    {
                        valueA = values[0].isValue ? TreatValue(values[0].value, true) : values[0].value;
                        valueB = ValidateValueByMethod(values[1], mtd.Method.Name);
                    }
                }
            }

            return string.Format("{0} {1} {2}", valueA, comparador, valueB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ValidateValueByMethod(dynamic obj, string methodName)
        {
            string valorTratado = TreatValue(obj.value, true).ToString();
            if (DbQueryConstants.LIKE_FUNCTION.Equals(methodName))
            {
                if (!obj.isValue)
                {
                    return string.Format(SQLKeys.CONCAT, $"'%', {obj.value} ,'%'");
                }
                return $"'%{obj.value}%'";
            }
            else if (DbQueryConstants.IN_FUNCTION.Equals(methodName) 
                    || DbQueryConstants.NOT_IN_FUNCTION.Equals(methodName))
            {
                return obj.value.ToString();
            }
            else 
            {
                return valorTratado;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        protected string TreatComparerByMethod(string methodName)
        {
            if (DbQueryConstants.LIKE_FUNCTION.Equals(methodName))
            {
                return SQLKeys.LIKE;
            }
            else if (DbQueryConstants.IN_FUNCTION.Equals(methodName))
            {
                return SQLKeys.IN;
            }
            else if (DbQueryConstants.NOT_IN_FUNCTION.Equals(methodName))
            {
                return SQLKeys.NOT_IN;
            }
            else
            {
                return SQLKeys.EQUALS;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected string DememberExpression(Expression expression)
        {
            if ((expression is LambdaExpression) && ((dynamic)expression).Parameters.Count > 1)
            {
                expression = ((dynamic)expression).Body;
            }

            bool ignore = false;
            string value = String.Empty, oldExpression = String.Empty, equalty = string.Empty;
            Expression left = null, right = null;

            if (ContainsProperty(expression, "Left"))
            {
                left = GetLeftNode(expression);
            }

            if (ContainsProperty(expression, "Right"))
            {
                right = GetRightNode(expression);
            }

            if (ContainsProperty(expression, "NodeType"))
            {
                equalty = GetComparador(expression);
            }

            if (left is MemberExpression)
            {
                var isValue = IsValue(left);
                if (isValue)
                {
                    var val = TreatValue(GetValue(left), true);
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
                var leftVal = DememberExpression(left);
                var rigthVal = DememberExpression(right);

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
                        value = TreatExpression(right, equalty, value);
                    }
                }
                else
                {
                    value = TreatExpression(right, equalty, value);
                }

                if (!string.IsNullOrEmpty(oldExpression) && !string.IsNullOrEmpty(value) && !_expressions.ContainsKey(oldExpression))
                {
                    _expressions.Add(oldExpression, value);
                }
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected bool IsValue(dynamic expression)
        {
            var right = expression;
            dynamic r = right;
            dynamic exp = _currentExpression;
            bool isValue = true;

            if (_currentExpression != null && ContainsProperty(exp, "Parameters") && exp.Parameters != null)
            {
                foreach (var e in exp.Parameters)
                {
                    if (ContainsProperty(r, "Expression") && r.Expression != null && ContainsProperty(r.Expression, "Member"))
                    {
                        if (r.Expression.Member != null && e.Name == r.Expression.Member.Name)
                        {
                            isValue = false;
                            break;
                        }
                    }
                    else if (ContainsProperty(r, "Expression") && r.Expression != null && ContainsProperty(r.Expression, "Name"))
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
        protected string TreatExpression(Expression right, string equality, string value)
        {
            var val = TreatValue(GetValue(right), true);
            if (SQLKeys.NULL.Equals(val))
            {
                if (SQLKeys.EQUALS.Equals(equality))
                {
                    equality = SQLKeys.IS;
                }
                else if (SQLKeys.NOT_EQUAL.Equals(equality))
                {
                    equality = SQLKeys.IS_NOT;
                }
            }
            return string.Format(value, equality, val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected bool ContainsProperty(object obj, string name)
        {
            return obj.GetType().GetProperty(name) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tipo"></param>
        protected OrderByQuery<T> AddOrderBy(string tipo, Expression expressions)
        {
            bool contains = _query.Contains(SQLKeys.ORDER_BY);
            if (!contains)
            {
                _query += SQLKeys.ORDER_BY_WITH_SPACE;
            }
            if (expressions != null)
            {
                var properties = GetPropertiesExpression(expressions);
                if (contains && properties.Count > 0)
                {
                    _query += ", " + string.Join($" {tipo}, ", properties) + $" {tipo}";
                }
                else
                {
                    _query += string.Join($" {tipo}, ", properties) + $" {tipo}";
                }
            }

            return CreateDbQuery<OrderByQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tipo"></param>
        protected OrderByQuery<T> AddGroupBy(Expression expressions)
        {
            bool contains = _query.Contains(SQLKeys.GROUP_BY);
            if (!contains)
            {
                _query += SQLKeys.GROUP_BY_WITH_SPACE;
            }
            if (expressions != null)
            {
                var properties = GetPropertiesExpression(expressions);
                if (contains && properties.Count > 0)
                {
                    _query += ", " + string.Join(", ", properties);
                }
                else
                {
                    _query += string.Join(", ", properties);
                }
            }

            return CreateDbQuery<OrderByQuery<T>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conditions"></param>
        protected SelectExecuteQuery<T> IncludeWhereConditions(Expression expression)
        {
            string condition = string.Empty;
            this._currentExpression = expression;
            if (expression != null)
            {
                var x = new List<string> { DbQueryConstants.EXPRESSION_DIVISOR };
                string aux = ((Expression)((dynamic)expression).Body).ToString();
                string exp = aux.Split(x.ToArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                condition = DememberExpression(((dynamic)expression).Body);

                if (_expressions.Keys.ToList().Exists(a => exp.Contains(a)))
                {
                    _expressions.Keys.ToList().ForEach(key =>
                    {
                        exp = exp.Replace(key, _expressions[key]);
                    });

                    exp = exp.Replace(DbQueryConstants.AND_ALSO, SQLKeys.AND)
                            .Replace(DbQueryConstants.OR_ELSE, SQLKeys.OR);

                    _query += string.Concat(SQLKeys.WHERE_WITH_SPACE, exp);
                }
                else
                {
                    _query += string.Concat(SQLKeys.WHERE_WITH_SPACE, exp);
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
        protected JoinQuery<T> IncludeJoinOnQuery<J, P>(Expression expression, string strJoin)
        {
            this._currentExpression = expression;
            var aux = _useAlias 
                ? string.Concat(GetFullName(typeof(P)), SQLKeys.AS_WITH_SPACE, ((dynamic)expression).Parameters[1].Name)
                : GetFullName(typeof(P));
            _query += string.Format(strJoin, aux, this.DememberExpression(expression));
            return CreateDbQuery<JoinQuery<T>>();
        }
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public void SetExpression(Expression expression)
        {
            _customExpression = expression;
        }
    }
}
