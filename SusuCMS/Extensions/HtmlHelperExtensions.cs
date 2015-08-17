using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Mvc.Html;
using SusuCMS;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensions
    {
        #region LabelFor

        public static IHtmlString LabelFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, string>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            var name = memberExpression.Member.Name;

            foreach (var item in htmlHelper.ViewData.ModelMetadata.Properties)
            {
                if (item.PropertyName == name && item.IsRequired == true)
                {
                    return new MvcHtmlString(htmlHelper.LabelFor<TModel, string>(expression).ToString() + "<span class=\"required\">*</span>");
                }
            }

            return htmlHelper.LabelFor<TModel, string>(expression);
        }

        public static IHtmlString LabelFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, int>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            var name = memberExpression.Member.Name;

            foreach (var item in htmlHelper.ViewData.ModelMetadata.Properties)
            {
                if (item.PropertyName == name && item.IsRequired == true)
                {
                    return new MvcHtmlString(htmlHelper.LabelFor<TModel, int>(expression).ToString() + "<span class=\"required\">*</span>");
                }
            }

            return htmlHelper.LabelFor<TModel, int>(expression);
        }

        #endregion

        #region HelpTip

        public static IHtmlString HelpTip(this HtmlHelper htmlHelper, string content, string text = "[?]")
        {
            var tagBuilder = new TagBuilder("a");
            tagBuilder.SetInnerText(text);
            tagBuilder.Attributes.Add("class", "helptip");
            tagBuilder.Attributes.Add("href", "javascript://");
            tagBuilder.Attributes.Add("title", content);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        #endregion

        #region Buttons

        public static IHtmlString LinkButton(this HtmlHelper htmlHelper, string linkText, string url)
        {
            return LinkButton(htmlHelper, linkText, new { href = url });
        }

        public static IHtmlString LinkButton(this HtmlHelper htmlHelper, string linkText, object htmlAttributes, string cssClass = "button blue")
        {
            var tagBuilder = new TagBuilder("a")
            {
                InnerHtml = string.Format("<span>{0}</span>", linkText)
            };

            tagBuilder.AddCssClass(cssClass);
            tagBuilder.MergeAttributes<string, object>(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString ReturnButton(this HtmlHelper htmlHelper)
        {
            var returnUrl = HttpContext.Current.Request.QueryString["returnUrl"];
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            if (url.IsLocalUrl(returnUrl))
            {
                return ReturnButton(htmlHelper, returnUrl);
            }

            return ReturnButton(htmlHelper, url.Action("Index"));
        }

        public static IHtmlString ReturnButton(this HtmlHelper htmlHelper, string url)
        {
            return LinkButton(htmlHelper, DisplayResource.Return, new { href = url }, "button");
        }

        public static IHtmlString SaveButton(this HtmlHelper htmlHelper)
        {
            return new MvcHtmlString(string.Format(@"<button type=""submit"" class=""button blue""><span>{0}</span></button>", DisplayResource.Save));
        }

        public static IHtmlString DeleteButton(this HtmlHelper htmlHelper, string name, object value)
        {
            return new MvcHtmlString(string.Format(@"<button type=""submit"" name=""{0}"" value=""{1}"" class=""delete"">{2}</button>",
                name, value, DisplayResource.Delete));
        }

        #endregion

        public static IHtmlString ReturnUrl(this HtmlHelper htmlHelper)
        {
            return htmlHelper.Hidden("ReturnUrl", HttpContext.Current.Request.RawUrl);
        }

        public static IEnumerable<SelectListItem> GetCultureNames(this HtmlHelper htmlHelper)
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (var item in cultures.OrderBy(i => i.Name))
            {
                yield return new SelectListItem
                {
                    Text = item.DisplayName,
                    Value = item.Name,
                    Selected = item.Equals(Thread.CurrentThread.CurrentCulture)
                };
            }
        }
    }
}