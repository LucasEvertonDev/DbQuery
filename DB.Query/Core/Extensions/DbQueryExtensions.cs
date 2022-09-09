using System.Collections.Generic;

namespace DB.Query.Core.Extensions
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
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static bool NOT_IN(this int s, params int[] listParams)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static bool NOT_IN(this int? s, params int[] listParams)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static bool NOT_IN(this string s, params string[] listParams)
        {
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static bool IN(this int s, params int[] listParams)
        {
            return true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static bool IN(this int? s, params int[] listParams)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="listParams"></param>
        /// <returns></returns>
        public static bool IN(this string s, params string[] listParams)
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
        public static bool IN(this int? s, string srt)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="srt"></param>
        /// <returns></returns>
        public static bool NOT_IN(this int? s, string srt)
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
