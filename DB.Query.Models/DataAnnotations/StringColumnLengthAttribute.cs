using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Query.Models.DataAnnotations
{
    public class StringColumnLengthAttribute : ValidationAttribute
    {
        private static string _errorMessage { get; set; } = "A coluna {0} não pode possuir mais que {1}.";

        private int _lenght { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        public StringColumnLengthAttribute(string columnName, int lenght) : base(string.Format(_errorMessage, columnName, lenght))
        {
            _lenght = lenght;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            return new StringLengthAttribute(_lenght).IsValid(value);
        }
    }
}
