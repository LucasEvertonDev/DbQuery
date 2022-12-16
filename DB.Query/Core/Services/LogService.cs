using System;
using System.Runtime.InteropServices;

namespace DB.Query.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class LogService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        /// <summary>
        /// 
        /// </summary>
        public static void PrintQuery(string query)
        {
            AllocConsole();
            Console.WriteLine(query);
            Console.WriteLine("");
        }
    }
}
