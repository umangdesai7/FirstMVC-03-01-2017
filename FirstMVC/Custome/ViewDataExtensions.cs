using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirstMVC.Custome
{
    public static class ViewDataExtensions
    {
        public static TAttribute GetModelAttribute<TAttribute>(this ViewDataDictionary viewData, bool inherit = false) where TAttribute : Attribute
        {
            if (viewData == null) throw new ArgumentException("ViewData");
            var containerType = viewData.ModelMetadata.ContainerType;
            return
                ((TAttribute[])
                 containerType.GetProperty(viewData.ModelMetadata.PropertyName).
                 GetCustomAttributes(typeof(TAttribute),
                 inherit)).
                    FirstOrDefault();

        }
    }
}