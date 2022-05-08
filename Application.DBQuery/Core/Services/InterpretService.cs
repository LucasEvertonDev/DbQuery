using DBQuery.Core.Constants;
using Application.Domains.DataAnnotatios;
using DBQuery.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DBQuery.Core.Enuns;

namespace DBQuery.Core.Services
{
    public class InterpretService<TEntity>
    {
        private Dictionary<string, string> _expressions { get; set; }
        private Expression _currentExpression { get; set; }
        private List<DBQueryLevelModel> _levelModels { get; set; }
        private bool _useAlias => _levelModels.Exists(a => a.LevelType == StepType.USE_ALIAS);
        private string _alias => _levelModels.Where(a => a.LevelType == StepType.USE_ALIAS).FirstOrDefault()?.LevelValue;
        private bool ContainsProperty(object obj, string name) => obj.GetType().GetProperty(name) != null;
        private string GetFullName(Type type) => string.Concat(GetDatabaseName(type), DBKeysConstants.T_A, GetTableName(type));
        private List<string> _defaultFunctions { get; set; }

        public InterpretService()
        {
            _defaultFunctions = new List<string>()
            {
                DBQueryConstants.SUM_FUNCTION,
                DBQueryConstants.MAX_FUNCTION,
                DBQueryConstants.MIN_FUNCTION,
                DBQueryConstants.COUNT_FUNCTION,
                DBQueryConstants.ALIAS_FUNCTION,
                DBQueryConstants.CONCAT_FUNCTION,
                DBQueryConstants.ALL_COLUMNS
            };
            _expressions = new Dictionary<string, string>();
        }

        public string StartToInterpret(List<DBQueryLevelModel> levelModels)
        {
            _levelModels = levelModels;
            return RunInterpret();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string RunInterpret()
        {
            if (_levelModels.Exists(a => a.LevelType == StepType.SELECT))
            {
                return GenerateSelectScript();
            }
            else if (_levelModels.Exists(a => a.LevelType == StepType.CUSTOM_SELECT))
            {
                return GenerateSelectScript();
            }
            else if (_levelModels.Exists(a => a.LevelType == StepType.DELETE))
            {
                return GenerateDeleteScript();
            }
            else if (_levelModels.Exists(a => a.LevelType == StepType.INSERT))
            {
                return GenerateInsertScript(); ;
            }
            else if (_levelModels.Exists(a => a.LevelType == StepType.INSERT_NOT_EXISTS))
            {
                return GenerateInsertIfNotExistsScript();
            }
            else if (_levelModels.Exists(a => a.LevelType == StepType.UPDATE))
            {
                return GenerateUpdateScript();
            }
            else
            {
                return "";
            }
        }

        #region Generate Scripts

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateInsertScript()
        {
            return String.Format(
                DBKeysConstants.INSERT,
                GetFullName(typeof(TEntity)),
                string.Join(", ", GetProperties()),
                GetPrimaryKeyName(typeof(TEntity)),
                string.Join(", ", GetValuesToInsert()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateInsertIfNotExistsScript()
        {
            return string.Format(
                DBKeysConstants.INSERT_NOT_EXISTS,
                GetFullName(typeof(TEntity)),
                DBKeysConstants.WHERE_WITH_SPACE + string.Join(DBKeysConstants.AND_WITH_SPACE, GetObjectClausules()),
                GenerateInsertScript());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateDeleteScript()
        {
            var query = String.Format(
                DBKeysConstants.DELETE,
                GetFullName(typeof(TEntity)),
                string.Empty);

            var where = _levelModels.Where(step => step.LevelType == StepType.WHERE).FirstOrDefault();
            if (where != null)
            {
                query += AddWhere(where.LevelExpression);
            }
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateSelectScript()
        {
            var query = string.Empty;
            foreach (var step in _levelModels)
            {
                if (step.LevelType == StepType.SELECT || step.LevelType == StepType.CUSTOM_SELECT)
                {
                    query += string.Format(
                        DBKeysConstants.SELECT,
                        DBKeysConstants.ALL_COLUMNS,
                        GetFullName(typeof(TEntity)),
                        string.IsNullOrEmpty(_alias) ? string.Empty : DBKeysConstants.AS_WITH_SPACE + _alias + " ");
                   
                    if (step.LevelType == StepType.CUSTOM_SELECT)
                    {
                        var props = GetPropertiesExpression(step.LevelExpression, useAlias: true);
                        query = query.Replace(DBKeysConstants.SELECT_ALL, DBKeysConstants.SELECT_KEY + " " + string.Join(", ", props));
                    }
                }
                else if (step.LevelType == StepType.DISTINCT)
                {
                    query = query.Replace(DBKeysConstants.SELECT_KEY, DBKeysConstants.SELECT_DISTINCT);
                }
                else if (step.LevelType == StepType.TOP)
                {
                    if (query.Contains(DBKeysConstants.SELECT_DISTINCT))
                    {
                        query = query.Replace(DBKeysConstants.SELECT_DISTINCT, string.Format(DBKeysConstants.SELECT_DISTINCT_TOP, step.LevelValue));
                    }
                    else
                    {
                        query = query.Replace(DBKeysConstants.SELECT_KEY, string.Format(DBKeysConstants.SELECT_TOP, step.LevelValue));
                    }
                }
                else if(step.LevelType == StepType.WHERE)
                {
                    query += AddWhere(step.LevelExpression);
                }
                else if (step.LevelType == StepType.JOIN)
                {
                    query += AddJoin(step.LevelExpression, DBKeysConstants.INNER_JOIN);
                }
                else if (step.LevelType == StepType.LEFT_JOIN)
                {
                    query += AddJoin(step.LevelExpression, DBKeysConstants.LEFT_JOIN);
                }
                else if (step.LevelType == StepType.ORDER_BY_ASC)
                {
                    query = AddOrderBy(DBKeysConstants.ASC, step.LevelExpression, query);
                }
                else if (step.LevelType == StepType.ORDER_BY_DESC)
                {
                    query = AddOrderBy(DBKeysConstants.DESC, step.LevelExpression, query);
                }
                else if(step.LevelType == StepType.GROUP_BY)
                {
                    query = AddGroupBy(step.LevelExpression, query);
                }
            }
            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GenerateUpdateScript()
        {
            var query = String.Format(
                DBKeysConstants.UPDATE,
                GetFullName(typeof(TEntity)),
                string.Join(", ", GetObjectClausules()),
                string.Empty);

            var where = _levelModels.Where(step => step.LevelType == StepType.WHERE).FirstOrDefault();
            if (where != null)
            {
                query += AddWhere(where.LevelExpression);
            }

            return query;
        }
        #endregion

        #region Iterpret Expressions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tipo"></param>
        protected string AddGroupBy(Expression expressions, string _query)
        {
            bool contains = _query.Contains(DBKeysConstants.GROUP_BY);
            if (!contains)
            {
                _query += DBKeysConstants.GROUP_BY_WITH_SPACE;
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

            return _query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="tipo"></param>
        protected string AddOrderBy(string tipo, Expression expressions, string query)
        {
            bool contains = query.Contains(DBKeysConstants.ORDER_BY);
            if (!contains)
            {
                query += DBKeysConstants.ORDER_BY_WITH_SPACE;
            }
            if (expressions != null)
            {
                var properties = GetPropertiesExpression(expressions);
                if (contains && properties.Count > 0)
                {
                    query += ", " + string.Join($" {tipo}, ", properties) + $" {tipo}";
                }
                else
                {
                    query += string.Join($" {tipo}, ", properties) + $" {tipo}";
                }
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Conditions"></param>
        protected string AddWhere(Expression expression)
        {
            string condition = string.Empty;
            this._currentExpression = expression;
            if (expression != null)
            {
                var splitCriteria = new List<string> { DBQueryConstants.EXPRESSION_DIVISOR };
                string expressionText = ((Expression)((dynamic)expression).Body).ToString();
                string exp = expressionText.Split(splitCriteria.ToArray(), StringSplitOptions.RemoveEmptyEntries)[0];

                DememberExpression(((dynamic)expression).Body);

                if (_expressions.Keys.ToList().Exists(a => exp.Contains(a)))
                {
                    _expressions.Keys.ToList().ForEach(key =>
                    {
                        exp = exp.Replace(key, _expressions[key]);
                    });

                    exp = exp.Replace(DBQueryConstants.AND_ALSO, DBKeysConstants.AND)
                            .Replace(DBQueryConstants.OR_ELSE, DBKeysConstants.OR);

                    condition += string.Concat(DBKeysConstants.WHERE_WITH_SPACE, exp);
                }
                else
                {
                    condition += string.Concat(DBKeysConstants.WHERE_WITH_SPACE, exp);
                }
            }

            return condition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="P"></typeparam>
        /// <param name="expression"></param>
        /// <param name="strJoin"></param>
        /// <returns></returns>
        protected string AddJoin(Expression expression, string strJoin)
        {
            var query = string.Empty;
            this._currentExpression = expression;
            var aux = _useAlias
                ? string.Concat(GetFullName(((dynamic)expression).Parameters[1].Type), DBKeysConstants.AS_WITH_SPACE, ((dynamic)expression).Parameters[1].Name)
                : GetFullName(((dynamic)expression).Parameters[1].Type);

            query += string.Format(strJoin, aux, this.DememberExpression(expression));

            return query;
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
            if (_useAlias && exp != null && ContainsProperty(exp, "NodeType") && exp.NodeType == ExpressionType.Call && exp.Arguments.Count > 0)
            {
                return exp.Arguments[0].Expression.Name;
            }
            if (_useAlias && exp != null && ContainsProperty(exp, "NodeType") && exp.NodeType == ExpressionType.Call && exp.Arguments.Count == 0)
            {
                return exp.Object.Name;
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
            List<PropertyInfo> infs = type.GetProperties().ToList();
            infs.ForEach(prop =>
            {
                var attr = prop.GetCustomAttributes(typeof(IdentityAttribute), false).FirstOrDefault() as IdentityAttribute;
                if (attr != null)
                {
                    key = string.Concat(DBKeysConstants.OUTPUT_INSERTED, prop.Name);
                }
            });
            return key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetDatabaseName(Type type)
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
        /// <returns></returns>
        protected List<string> GetObjectClausules()
        {
            TEntity domain = _levelModels.Where(a => a.LevelType == StepType.INSERT || a.LevelType == StepType.INSERT_NOT_EXISTS || a.LevelType == StepType.UPDATE).First().LevelValue;
            var list = new List<string>();
            domain.GetType().GetProperties().ToList().ForEach(prop =>
            {
                if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0 && prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0)
                {
                    var val = TreatValue((dynamic)prop.GetValue(domain), true);
                    list.Add(string.Concat(prop.Name, DBKeysConstants.EQUALS_WITH_SPACE, val?.ToString()));
                }
            });
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<string> GetValuesToInsert()
        {
            TEntity domain = _levelModels.Where(a => a.LevelType == StepType.INSERT || a.LevelType == StepType.INSERT_NOT_EXISTS).First().LevelValue;
            var values = new List<string>();
            domain.GetType().GetProperties().ToList().ForEach(prop =>
            {
                if ((prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0))
                {
                    if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                    {
                        var val = TreatValue((dynamic)prop.GetValue(domain), true);
                        values.Add(val?.ToString());
                    }
                }
            });
            return values;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetPropretyFullName(Type type, dynamic exp, dynamic member = null)
        {
            return string.Concat(GetTableName(type, exp), DBKeysConstants.SINGLE_POINT, GetCollumnName(member == null ? exp.Member : member));
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
        /// <param name="expression"></param>
        /// <returns></returns>
        protected List<string> GetPropertiesExpression(Expression expression, bool useAlias = false)
        {
            var properties = new List<string>();
            _currentExpression = expression;
            dynamic exp = expression;
            if (expression.Type == typeof(Func<TEntity, dynamic>))
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
                    if (ContainsProperty(exp.Operand, "Expression"))
                    {
                        return GetPropretyFullName(exp.Operand.Expression.Type, exp.Operand);
                    }
                    else
                    {
                        return exp.Operand.Value?.ToString();
                    }
                }
                if (exp.NodeType == ExpressionType.Call)
                {
                    if (_defaultFunctions.Exists(f => f.Equals(exp.Method.Name)))
                    {
                        if (DBQueryConstants.ALL_COLUMNS.Equals(exp.Method.Name))
                        {
                            List<string> props = GetProperties(exp, exp.Object.Type);
                            return string.Join(", ", props);
                        }
                        else if (DBQueryConstants.ALIAS_FUNCTION.Equals(exp.Method.Name))
                        {
                            var ret = GetPropertyOfSingleExpression(exp.Arguments[0], hasParamter, false);
                            return String.Concat(ret, DBKeysConstants.AS_WITH_SPACE, exp.Arguments[1].Value);
                        }
                        else if (DBQueryConstants.COUNT_FUNCTION.Equals(exp.Method.Name) && !hasParamter)
                        {
                            return DBKeysConstants.COUNT;
                        }
                        else if (DBQueryConstants.CONCAT_FUNCTION.Equals(exp.Method.Name))
                        {
                            return ConcatFunction(exp);
                        }
                        else
                        {
                            if (ContainsProperty(exp, "Body"))
                            {
                                var d = exp.Body.Arguments[0];
                                var aux = d.Expressions[0].Arguments[0].Operand;
                                return string.Format(string.Concat(exp.Method.Name, "({0})"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member));
                            }
                            else
                            {
                                var d = exp.Arguments[0];
                                if (ContainsProperty(d, "Expressions"))
                                {
                                    var aux = d.Expressions[0].Arguments[0].Operand;
                                    return string.Format(string.Concat(exp.Method.Name, "({0})"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member));
                                }
                                else
                                {
                                    var aux = d.Operand;
                                    return string.Format(string.Concat(exp.Method.Name, "({0})"), GetPropretyFullName(aux.Expression.Type, aux.Expression, aux.Member));
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
        protected List<string> GetProperties(dynamic exp = null, Type type = null)
        {
            var list = new List<string>();
            bool insert = _levelModels.Exists(s => s.LevelType == StepType.INSERT || s.LevelType == StepType.INSERT_NOT_EXISTS);
            
            Type currentType = type != null ? type : typeof(TEntity);
            List<PropertyInfo> infs = currentType.GetProperties().ToList();

            infs.ForEach(prop =>
            {
                if (prop.GetCustomAttributes(typeof(IgnoreAttribute), false).Count() == 0)
                {
                    if ((insert && prop.GetCustomAttributes(typeof(IdentityAttribute), false).Count() == 0) || !insert)
                    {
                        var propName = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false).FirstOrDefault() as DisplayNameAttribute;
                        if (propName != null)
                        {
                            list.Add(string.IsNullOrEmpty(GetTableName(currentType, exp)) 
                                ? propName.DisplayName 
                                : GetTableName(currentType, exp) + DBKeysConstants.SINGLE_POINT + propName.DisplayName);
                        }
                        else
                        {
                            list.Add(string.IsNullOrEmpty(GetTableName(currentType, exp)) 
                                ? prop.Name 
                                : GetTableName(currentType, exp) + DBKeysConstants.SINGLE_POINT + prop.Name);
                        }
                    }
                }
            });

            return list;
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
            else if (left != null && ContainsProperty(expression, "Method") && DBQueryConstants.CONCAT_FUNCTION.Equals(((dynamic)left).Method?.Name))
            {
                oldExpression = expression.ToString();
                value = string.Format("{0} {1} {2}", ExtractMethod(left), "{0}", "{1}");
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
        /// <param name="right"></param>
        /// <param name="equality"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected string TreatExpression(Expression right, string equality, string value)
        {
            var val = TreatValue(GetValue(right), true, right);
            if (DBKeysConstants.NULL.Equals(val))
            {
                if (DBKeysConstants.EQUALS.Equals(equality))
                {
                    equality = DBKeysConstants.IS;
                }
                else if (DBKeysConstants.NOT_EQUAL.Equals(equality))
                {
                    equality = DBKeysConstants.IS_NOT;
                }
            }
            return string.Format(value, equality, val);
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
                if (DBQueryConstants.CONCAT_FUNCTION.Equals(mtd.Method.Name))
                {
                    return ConcatFunction(method);
                }
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
                                value = string.Concat(GetTableName(arg.Member.DeclaringType, mtd), DBKeysConstants.SINGLE_POINT, GetCollumnName(arg.Member));
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
                                value = string.Concat(GetTableName(arg.Expression.Type, mtd), DBKeysConstants.SINGLE_POINT, GetCollumnName(arg.Member));
                            }
                        }

                        values.Add(new { value = value, isValue = isValue });
                    }

                    if (values.Any())
                    {
                        valueA = values[0].isValue ? TreatValue(values[0].value, true) : values[0].value;
                        if (mtd.Arguments != null && mtd.Arguments.Count > 1)
                        {
                            dynamic arg = (dynamic)mtd.Arguments[1];
                            if (ContainsProperty(arg, "Method") && arg.Method != null
                                && DBQueryConstants.CONCAT_FUNCTION.Equals(arg.Method.Name))
                            {
                                valueB = ValidateValueByMethod(values[1], mtd.Method.Name, true);
                            }
                            else
                            {
                                valueB = ValidateValueByMethod(values[1], mtd.Method.Name, false);
                            }
                        }
                        else
                        {
                            valueB = ValidateValueByMethod(values[1], mtd.Method.Name, false);
                        }
                    }
                }
            }

            return string.Format("{0} {1} {2}", valueA, comparador, valueB);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exp"></param>
        private string ConcatFunction(dynamic exp)
        {
            var d = exp.Arguments[0];
            var list = new List<String>();
            foreach (var xp in d.Expressions)
            {
                string value = "";
                if (xp is MemberExpression)
                {
                    bool isValue = true;
                    isValue = IsValue(xp);

                    if (!isValue)
                    {
                        var propertyInfo1 = (PropertyInfo)xp.Member;

                        var name1 = GetCollumnName(propertyInfo1);

                        value = GetTableName(propertyInfo1.DeclaringType, xp) + "." + name1;
                    }
                }

                if (string.IsNullOrEmpty(value))
                {
                    value = TreatValue(GetValue(xp), true);
                }

                value = string.Format(DBKeysConstants.CONVERT_VARCHAR, value);
                list.Add(value);
            }

            return string.Format(DBKeysConstants.CONCAT, string.Join(",", list));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string ValidateValueByMethod(dynamic obj, string methodName, bool isConcatValue)
        {
            string valorTratado = TreatValue(obj.value, true).ToString();
            if (DBQueryConstants.LIKE_FUNCTION.Equals(methodName))
            {
                if (!obj.isValue || isConcatValue)
                {
                    return string.Format(DBKeysConstants.CONCAT, $"'%', {obj.value} ,'%'");
                }
                return $"'%{obj.value}%'";
            }
            else if (DBQueryConstants.IN_FUNCTION.Equals(methodName)
                    || DBQueryConstants.NOT_IN_FUNCTION.Equals(methodName))
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
            if (DBQueryConstants.LIKE_FUNCTION.Equals(methodName))
            {
                return DBKeysConstants.LIKE;
            }
            else if (DBQueryConstants.IN_FUNCTION.Equals(methodName))
            {
                return DBKeysConstants.IN;
            }
            else if (DBQueryConstants.NOT_IN_FUNCTION.Equals(methodName))
            {
                return DBKeysConstants.NOT_IN;
            }
            else
            {
                return DBKeysConstants.EQUALS;
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
                    || "IN".Equals(r.Method.Name) || "NOT_IN".Equals(r.Method.Name)
                    || DBQueryConstants.CONCAT_FUNCTION.Equals(r.Method.Name)))
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
            else if (right != null && ContainsProperty(right, "NodeType") && (right.NodeType == ExpressionType.ConvertChecked || right.NodeType == ExpressionType.Convert))
            {
                return Expression.Lambda(right).Compile().DynamicInvoke();
            }
            else if (right != null && ContainsProperty(right, "NodeType") && right.NodeType == ExpressionType.Lambda)
            {
                return Expression.Lambda(right).Compile().DynamicInvoke();
            }

            return string.Empty;
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
        /// <returns></returns>
        protected string GetComparador(Expression expression)
        {
            string equalty = string.Empty;
            if (expression.NodeType == ExpressionType.Equal)
            {
                equalty = DBKeysConstants.EQUALS;
            }
            if (expression.NodeType == ExpressionType.AndAlso)
            {
                equalty = DBKeysConstants.AND;
            }
            if (expression.NodeType == ExpressionType.OrElse)
            {
                equalty = DBKeysConstants.OR;
            }
            if (expression.NodeType == ExpressionType.NotEqual)
            {
                equalty = DBKeysConstants.NOT_EQUAL;
            }
            if (expression.NodeType == ExpressionType.GreaterThan)
            {
                equalty = DBKeysConstants.GREATER_THAN;
            }
            if (expression.NodeType == ExpressionType.GreaterThanOrEqual)
            {
                equalty = DBKeysConstants.GREATER_THAN_OR_EQUAL;
            }
            if (expression.NodeType == ExpressionType.LessThan)
            {
                equalty = DBKeysConstants.LESS_THAN; ;
            }
            if (expression.NodeType == ExpressionType.LessThanOrEqual)
            {
                equalty = DBKeysConstants.LESS_THAN_OR_EQUAL;
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
        protected object TreatValue(dynamic val, bool useQuotes = false, dynamic expression = null)
        {
            if (expression != null)
            {
                if (expression is MethodCallExpression)
                {
                    if (DBQueryConstants.CONCAT_FUNCTION.Equals(expression.Method.Name))
                    {
                        return val;
                    }
                }
            }

            if (val == null || (val.GetType() != typeof(string) && string.IsNullOrEmpty(val?.ToString())) && useQuotes)
            {
                return DBKeysConstants.NULL;
            }

            if (val.GetType() == typeof(DateTime))
            {
                return useQuotes ? $"'{val.ToString(DBKeysConstants.DATE_FORMAT)}'" : val.ToString(DBKeysConstants.DATE_FORMAT);
            }
            if (val.GetType() == typeof(DateTimeOffset))
            {
                return useQuotes ? $"'{val.ToString(DBKeysConstants.DATE_OFF_SET_FORMAT)}'" : val.ToString(DBKeysConstants.DATE_OFF_SET_FORMAT);
            }
            else if (val.GetType() == typeof(bool))
            {
                return val ? 1 : 0;
            }
            else if (val.GetType() == typeof(decimal))
            {
                return val?.ToString(DBKeysConstants.DECIMAL_FORMAT)?.Replace(",", ".");
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
        #endregion
    }
}
