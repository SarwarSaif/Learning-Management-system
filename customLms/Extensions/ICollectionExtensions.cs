using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace customLms.Extensions
{
    public static class ICollectionExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(
            this ICollection<T>contents,int  selectedValue)
        {

            return from content in contents
                   select new SelectListItem
                   {
                       Text = content.GetPropertyValue("name"),
                       Value = content.GetPropertyValue("id"),
                       Selected = content.GetPropertyValue("id").Equals(selectedValue.ToString())


                   };

        }



    }
}