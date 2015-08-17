using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SusuCMS.Data;

namespace SusuCMS.Web.Front.Html
{
    public class MetadataBuilder
    {
        private FrontContext _frontContext;
        private IList<MetaField> _metaFields;

        public MetadataBuilder(FrontContext frontContext)
        {
            _frontContext = frontContext;

            var viewData = _frontContext.ViewData;
            if (viewData.MetaFields == null)
            {
                viewData.MetaFields = GetMetaFields();
            }

            _metaFields = viewData.MetaFields;
        }

        public IHtmlString GetHtml()
        {
            var builder = new StringBuilder();
            foreach (var item in _metaFields)
            {
                builder.AppendFormat(@"<meta name=""{0}"" content=""{1}"" />", item.Name, item.Content);
            }

            return MvcHtmlString.Create(builder.ToString());
        }

        public void SetMetaField(string name, string content)
        {
            var metaField = _metaFields.FirstOrDefault(i => i.Name == name);
            if (metaField == null)
            {
                _metaFields.Add(new MetaField { Name = name, Content = content });
            }
            else
            {
                metaField.Content = content;
            }
        }

        private IList<MetaField> GetMetaFields()
        {
            var list = _frontContext.Site.MetaFields.ToList();
            foreach (var item in _frontContext.Page.MetaFields)
            {
                var metaField = list.FirstOrDefault(i => i.Name == item.Name);
                if (metaField == null)
                {
                    list.Add(new MetaField(item.Name, item.Content));
                }
                else
                {
                    metaField.Content = item.Content;
                }
            }

            return list;
        }
    }
}
