﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace SusuCMS.Web.Paging
{
	///<summary>
	///	Extension methods for generating paging controls that can operate on instances of IPagedList.
	///</summary>
	public static class HtmlHelperExtensions
	{
		private static TagBuilder WrapInListItem(string text)
		{
			var li = new TagBuilder("li");
			li.SetInnerText(text);
			return li;
		}

		private static TagBuilder WrapInListItem(TagBuilder inner, params string[] classes)
		{
			var li = new TagBuilder("li");
			foreach (var @class in classes)
				li.AddCssClass(@class);
			li.InnerHtml = inner.ToString();
			return li;
		}

		private static TagBuilder First(IPagedList list, Func<int, string> generatePageUrl, string format)
		{
			const int targetPageNumber = 1;
			var first = new TagBuilder("a")
			            	{
			            		InnerHtml = string.Format(format, targetPageNumber)
			            	};

			if (list.IsFirstPage)
				return WrapInListItem(first, "first", "disabled");

			first.Attributes["href"] = generatePageUrl(targetPageNumber);
            return WrapInListItem(first, "first");
		}

		private static TagBuilder Previous(IPagedList list, Func<int, string> generatePageUrl, string format)
		{
			var targetPageNumber = list.PageNumber - 1;
			var previous = new TagBuilder("a")
			               	{
			               		InnerHtml = string.Format(format, targetPageNumber)
			               	};

			if (!list.HasPreviousPage)
				return WrapInListItem(previous, "prev", "disabled");

			previous.Attributes["href"] = generatePageUrl(targetPageNumber);
            return WrapInListItem(previous, "prev");
		}

		private static TagBuilder Page(int i, IPagedList list, Func<int, string> generatePageUrl, string format)
		{
			return Page(i, list, generatePageUrl, (pageNumber => string.Format(format, pageNumber)));
		}

		private static TagBuilder Page(int i, IPagedList list, Func<int, string> generatePageUrl, Func<int, string> format)
		{
			var targetPageNumber = i;
			var page = new TagBuilder("a");
			page.SetInnerText(format(targetPageNumber));

			if (i == list.PageNumber)
				return WrapInListItem(page, "current");

			page.Attributes["href"] = generatePageUrl(targetPageNumber);
			return WrapInListItem(page);
		}

		private static TagBuilder Next(IPagedList list, Func<int, string> generatePageUrl, string format)
		{
			var targetPageNumber = list.PageNumber + 1;
			var next = new TagBuilder("a")
			           	{
			           		InnerHtml = string.Format(format, targetPageNumber)
			           	};

			if (!list.HasNextPage)
				return WrapInListItem(next, "next", "disabled");

			next.Attributes["href"] = generatePageUrl(targetPageNumber);
            return WrapInListItem(next, "next");
		}

		private static TagBuilder Last(IPagedList list, Func<int, string> generatePageUrl, string format)
		{
			var targetPageNumber = list.PageCount;
			var last = new TagBuilder("a")
			           	{
			           		InnerHtml = string.Format(format, targetPageNumber)
			           	};

			if (list.IsLastPage)
				return WrapInListItem(last, "last", "disabled");

			last.Attributes["href"] = generatePageUrl(targetPageNumber);
			return WrapInListItem(last, "last");
		}

		private static TagBuilder PageCountAndLocationText(IPagedList list, string format)
		{
			var text = new TagBuilder("a");
			text.SetInnerText(string.Format(format, list.PageNumber, list.PageCount));

			return WrapInListItem(text, "count-location", "disabled");
		}

		private static TagBuilder ItemSliceAndTotalText(IPagedList list, string format)
		{
			var text = new TagBuilder("a");
			text.SetInnerText(string.Format(format, list.FirstItemOnPage, list.LastItemOnPage, list.TotalItemCount));

            return WrapInListItem(text, "count-location", "disabled");
		}

		private static TagBuilder Ellipses(string format)
		{
			var a = new TagBuilder("a")
			        	{
			        		InnerHtml = format
			        	};

			return WrapInListItem(a, "ellipses", "disabled");
		}

		///<summary>
		///	Displays a configurable paging control for instances of PagedList.
		///</summary>
		///<param name = "html">This method is meant to hook off HtmlHelper as an extension method.</param>
		///<param name = "list">The PagedList to use as the data source.</param>
		///<param name = "generatePageUrl">A function that takes the page number of the desired page and returns a URL-string that will load that page.</param>
		///<returns>Outputs the paging control HTML.</returns>
		public static MvcHtmlString PagedListPager(this System.Web.Mvc.HtmlHelper html,
												   IPagedList list,
												   Func<int, string> generatePageUrl)
		{
			return PagedListPager(html, list, generatePageUrl, new PagedListRenderOptions());
		}

		///<summary>
		///	Displays a configurable paging control for instances of PagedList.
		///</summary>
		///<param name = "html">This method is meant to hook off HtmlHelper as an extension method.</param>
		///<param name = "list">The PagedList to use as the data source.</param>
		///<param name = "generatePageUrl">A function that takes the page number  of the desired page and returns a URL-string that will load that page.</param>
		///<param name = "options">Formatting options.</param>
		///<returns>Outputs the paging control HTML.</returns>
		public static MvcHtmlString PagedListPager(this System.Web.Mvc.HtmlHelper html,
												   IPagedList list,
												   Func<int, string> generatePageUrl,
												   PagedListRenderOptions options)
		{
			var listItemLinks = new List<TagBuilder>();

			//first
			if (options.DisplayLinkToFirstPage)
				listItemLinks.Add(First(list, generatePageUrl, options.LinkToFirstPageFormat));

			//previous
			if (options.DisplayLinkToPreviousPage)
				listItemLinks.Add(Previous(list, generatePageUrl, options.LinkToPreviousPageFormat));

			//text
			if (options.DisplayPageCountAndCurrentLocation)
				listItemLinks.Add(PageCountAndLocationText(list, options.PageCountAndCurrentLocationFormat));

			//text
			if (options.DisplayItemSliceAndTotal)
				listItemLinks.Add(ItemSliceAndTotalText(list, options.ItemSliceAndTotalFormat));

			//page
			if (options.DisplayLinkToIndividualPages)
			{
				//calculate start and end of range of page numbers
				var start = 1;
				var end = list.PageCount;
				if (options.MaximumPageNumbersToDisplay.HasValue && list.PageCount > options.MaximumPageNumbersToDisplay)
				{
					var maxPageNumbersToDisplay = options.MaximumPageNumbersToDisplay.Value;
					start = list.PageNumber - maxPageNumbersToDisplay / 2;
					if (start < 1)
						start = 1;
					end = maxPageNumbersToDisplay;
					if ((start + end - 1) > list.PageCount)
						start = list.PageCount - maxPageNumbersToDisplay + 1;
				}

				//if there are previous page numbers not displayed, show an ellipsis
				if (options.DisplayEllipsesWhenNotShowingAllPageNumbers && start > 1)
					listItemLinks.Add(Ellipses(options.EllipsesFormat));

				foreach (var i in Enumerable.Range(start, end))
				{
					//show delimiter between page numbers
					if (i > start && !String.IsNullOrWhiteSpace(options.DelimiterBetweenPageNumbers))
						listItemLinks.Add(WrapInListItem(options.DelimiterBetweenPageNumbers));

					//show page number link
					listItemLinks.Add(options.FunctionToDisplayEachPageNumber == null
											 ? Page(i, list, generatePageUrl, options.LinkToIndividualPageFormat)
											 : Page(i, list, generatePageUrl, options.FunctionToDisplayEachPageNumber));
				}

				//if there are subsequent page numbers not displayed, show an ellipsis
				if (options.DisplayEllipsesWhenNotShowingAllPageNumbers && (start + end - 1) < list.PageCount)
					listItemLinks.Add(Ellipses(options.EllipsesFormat));
			}

			//next
			if (options.DisplayLinkToNextPage)
				listItemLinks.Add(Next(list, generatePageUrl, options.LinkToNextPageFormat));

			//last
			if (options.DisplayLinkToLastPage)
				listItemLinks.Add(Last(list, generatePageUrl, options.LinkToLastPageFormat));

			//append class to first item in list?
			if (!String.IsNullOrWhiteSpace(options.ClassToApplyToFirstListItemInPager))
				listItemLinks.First().AddCssClass(options.ClassToApplyToFirstListItemInPager);

			//append class to last item in list?
			if (!String.IsNullOrWhiteSpace(options.ClassToApplyToLastListItemInPager))
				listItemLinks.Last().AddCssClass(options.ClassToApplyToLastListItemInPager);

			//append classes to all list item links
			foreach(var li in listItemLinks)
				foreach (var c in options.LiElementClasses ?? Enumerable.Empty<string>())
					li.AddCssClass(c);

			//collapse all of the list items into one big string
			var listItemLinksString = listItemLinks.Aggregate(
				new StringBuilder(),
				(sb, listItem) => sb.Append(listItem.ToString()),
				sb=> sb.ToString()
				);

			var ul = new TagBuilder("ul")
						{
							InnerHtml = listItemLinksString
						};
			foreach (var c in options.UlElementClasses ?? Enumerable.Empty<string>())
				ul.AddCssClass(c);

			var outerDiv = new TagBuilder("div");
			foreach(var c in options.ContainerDivClasses ?? Enumerable.Empty<string>())
				outerDiv.AddCssClass(c);
			outerDiv.InnerHtml = ul.ToString();

			return new MvcHtmlString(outerDiv.ToString());
		}

		///<summary>
		/// Displays a configurable "Go To Page:" form for instances of PagedList.
		///</summary>
		///<param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
		///<param name="list">The PagedList to use as the data source.</param>
		///<param name="formAction">The URL this form should submit the GET request to.</param>
		///<returns>Outputs the "Go To Page:" form HTML.</returns>
		public static MvcHtmlString PagedListGoToPageForm(this System.Web.Mvc.HtmlHelper html,
														  IPagedList list,
														  string formAction)
		{
			return PagedListGoToPageForm(html, list, formAction, "page");
		}

		///<summary>
		/// Displays a configurable "Go To Page:" form for instances of PagedList.
		///</summary>
		///<param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
		///<param name="list">The PagedList to use as the data source.</param>
		///<param name="formAction">The URL this form should submit the GET request to.</param>
		///<param name="inputFieldName">The querystring key this form should submit the new page number as.</param>
		///<returns>Outputs the "Go To Page:" form HTML.</returns>
		public static MvcHtmlString PagedListGoToPageForm(this System.Web.Mvc.HtmlHelper html,
		                                                  IPagedList list,
		                                                  string formAction,
		                                                  string inputFieldName)
		{
			return PagedListGoToPageForm(html, list, formAction, new GoToFormRenderOptions(inputFieldName));
		}

		///<summary>
		/// Displays a configurable "Go To Page:" form for instances of PagedList.
		///</summary>
		///<param name="html">This method is meant to hook off HtmlHelper as an extension method.</param>
		///<param name="list">The PagedList to use as the data source.</param>
		///<param name="formAction">The URL this form should submit the GET request to.</param>
		///<param name="options">Formatting options.</param>
		///<returns>Outputs the "Go To Page:" form HTML.</returns>
		public static MvcHtmlString PagedListGoToPageForm(this System.Web.Mvc.HtmlHelper html,
		                                         IPagedList list,
		                                         string formAction,
		                                         GoToFormRenderOptions options)
		{
			var form = new TagBuilder("form");
			form.AddCssClass("goto");
			form.Attributes.Add("action", formAction);
			form.Attributes.Add("method", "get");

			var fieldset = new TagBuilder("fieldset");

			var label = new TagBuilder("label");
			label.Attributes.Add("for", options.InputFieldName);
			label.SetInnerText(options.LabelFormat);

			var input = new TagBuilder("input");
			input.Attributes.Add("type", options.InputFieldType);
			input.Attributes.Add("name", options.InputFieldName);
			input.Attributes.Add("value", list.PageNumber.ToString());

			var submit = new TagBuilder("input");
			submit.Attributes.Add("type", "submit");
			submit.Attributes.Add("value", options.SubmitButtonFormat);

			fieldset.InnerHtml = label.ToString();
			fieldset.InnerHtml += input.ToString(TagRenderMode.SelfClosing);
			fieldset.InnerHtml += submit.ToString(TagRenderMode.SelfClosing);
			form.InnerHtml = fieldset.ToString();
			return new MvcHtmlString(form.ToString());
		}
	}
}