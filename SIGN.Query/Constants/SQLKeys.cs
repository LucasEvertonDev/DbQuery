using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Constants
{
    public static class SQLKeys
    {
        public const string DELETE = "DELETE FROM {0}{1}";

        public const string SELECT = "SELECT {0} FROM {1} {2}";

        public const string INNER_JOIN = "INNER JOIN {0} ON {1}";

        public const string LEFT_JOIN = "LEFT JOIN {0} ON {1}";

        public const string INSERT = "INSERT INTO {0} ({1}) {2} VALUES ({3})";

        public const string INSERT_NOT_EXISTS = "IF NOT EXISTS(SELECT * FROM {0} {1}) BEGIN {2} END ";

        public const string UPDATE = "UPDATE {0} SET {1} {2}";

        public const string ALL_COLUMNS = "*";

        public const string ASC = "ASC";

        public const string DESC = "DESC";

        public const string SELECT_ALL = "SELECT *";

        public const string SELECT_KEY = "SELECT";

        public const string SELECT_DISTINCT = "SELECT DISTINCT";

        public const string SELECT_TOP = "SELECT TOP({0})";

        public const string SELECT_DISTINCT_TOP = "SELECT DISTINCT TOP({0})";

        public const string OUTPUT_INSERTED = "OUTPUT Inserted.";

        public const string T_A = "..";

        public const string EQUALS = "=";

        public const string AND = "AND";

        public const string AND_WITH_SPACE = " AND ";

        public const string OR = "OR";

        public const string NOT_EQUAL = "<>";

        public const string GREATER_THAN = ">";

        public const string LESS_THAN = "<";

        public const string LESS_THAN_OR_EQUAL = "<=";

        public const string GREATER_THAN_OR_EQUAL = ">=";

        public const string NULL = "NULL";

        public const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public const string DATE_OFF_SET_FORMAT = "yyyy-MM-dd HH:mm:ss.fffffffzzz";

        public const string DECIMAL_FORMAT = "0.##########";

        public const string EQUALS_WITH_SPACE = " = ";

        public const string COUNT = "COUNT(*)";

        public const string SINGLE_POINT = ".";

        public const string CONVERT_VARCHAR = "CONVERT(varchar, {0})";

        public const string CONCAT = "CONCAT({0})";

        public const string LIKE = "LIKE";

        public const string IN = "IN";

        public const string NOT_IN = "NOT IN";

        public const string IS = "IS";

        public const string IS_NOT = "IS NOT";

        public const string ORDER_BY = "ORDER BY";

        public const string ORDER_BY_WITH_SPACE = " ORDER BY ";

        public const string GROUP_BY = "GROUP BY";

        public const string GROUP_BY_WITH_SPACE = " GROUP BY ";

        public const string WHERE = "WHERE";

        public const string WHERE_WITH_SPACE = " WHERE ";

        public const string AS = "AS";

        public const string AS_WITH_SPACE = " AS ";

        public const string DISTINCT_ALL = " DISTINCT *";

        public const string DISTINCT_WITH_SPACE = " DISTINCT ";
    }
}
