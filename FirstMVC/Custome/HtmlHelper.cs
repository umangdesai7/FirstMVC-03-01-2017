using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FirstMVC.Custome
{
    public static class HtmlHelper
    {
        public static IHtmlString RenderScripts(this FirstMVC.Custome.HtmlHelper htmlhelper)
        {
            var scripts = htmlhelper.Viewcontext.HttpContext.Items["MaskSripts"] as IList<string>;
            if (scripts != null)
            {
                var builder = new StringBuilder();
                foreach (var script in scripts)
                {
                    builder.AppendLine(script);
                }
                return new MvcHtmlString(builder.ToString());
            }
            return null;
        }
    }
}