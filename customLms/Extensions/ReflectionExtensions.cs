using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace customLms.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetPropertyValue<T>(this T content,string propertyName)
        {
             

            return content.GetType().GetProperty(propertyName).GetValue(content, null).ToString();
        }



    }
}