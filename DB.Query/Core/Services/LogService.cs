using System;
using System.Runtime.InteropServices;

namespace DB.Query.Core.Services
{
    public class LogService
    {
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
