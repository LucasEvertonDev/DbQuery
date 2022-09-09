using System;

namespace DB.Query.Models.DataAnnotations
{
    public class TimeoutAttribute : Attribute
    {
        public int TimeOut { get; set; }
        public TimeoutAttribute(int timeout)
        {
            TimeOut = timeout;
        }
    }
}
