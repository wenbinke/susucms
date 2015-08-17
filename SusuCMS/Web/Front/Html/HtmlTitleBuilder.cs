using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SusuCMS.Web.Front.Html
{
    public class HtmlTitleBuilder
    {
        private FrontContext _frontContext;

        public HtmlTitleBuilder(FrontContext frontContext)
        {
            _frontContext = frontContext;
        }

        public void SetTitle(string title)
        {
            _frontContext.ViewData.Title = title;
        }
        
        public string GetTitle()
        {
            return _frontContext.ViewData.Title;
        }
    }
}
