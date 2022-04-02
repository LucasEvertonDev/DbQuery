using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIGN.Query.Extensions
{
    public static class DbQueryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="srt"></param>
        /// <returns></returns>
        public static bool IN(this string s, string srt)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="srt"></param>
        /// <returns></returns>
        public static bool NOT_IN(this string s, string srt)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool LIKE(this string s, string str)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GenerateScriptIN(this List<string> list)
        {
            var aux = new List<string>();
            foreach (var item in list)
            {
                aux.Add("'" + item.ToString() + "'");
            }
            return "(" + string.Join(", ", aux) + ")";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="srt"></param>
        /// <returns></returns>
        public static bool IN(this int s, string srt)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="srt"></param>
        /// <returns></returns>
        public static bool NOT_IN(this int s, string srt)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GenerateScriptIN(this List<int> list)
        {
            var aux = new List<string>();
            foreach (var item in list)
            {
                aux.Add(item.ToString());
            }
            return "(" + string.Join(", ", aux) + ")";
        }
    }
}
