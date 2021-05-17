using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GbWebApp.TagHelpers
{
    [HtmlTargetElement(Attributes = TagAttrName)]
    public class ActiveRouteTag : TagHelper
    {
        private const string TagAttrName = "is-active";

        private const string IgnoreAction = "ignore-action";

        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }

        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RouteValues { get; set; } = new(StringComparer.OrdinalIgnoreCase);

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var ignoreAction = output.Attributes.ContainsName(IgnoreAction);

            if (IsActive(ignoreAction)) MakeActive(output);

            output.Attributes.RemoveAll(TagAttrName);
            output.Attributes.RemoveAll(IgnoreAction);
        }

        private bool IsActive(bool ignoreAction)
        {
            var routeValues = ViewContext.RouteData.Values;

            var currentController = routeValues["controller"]?.ToString();
            var currentAction = routeValues["action"]?.ToString();

            if (!string.IsNullOrEmpty(Controller) && !string.Equals(currentController, Controller, StringComparison.OrdinalIgnoreCase))
                return false;

            if (!ignoreAction && !string.IsNullOrEmpty(Action) && !string.Equals(currentAction, Action, StringComparison.OrdinalIgnoreCase))
                return false;

            foreach (var (key, value) in RouteValues)
                if (!routeValues.ContainsKey(key) || routeValues[key]?.ToString() != value) return false;

            return true;
        }

        private static void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(attr => attr.Name == "class");

            if (classAttr is null)
                output.Attributes.Add("class", "active");
            else
            {
                if (classAttr.Value.ToString()?.Contains("active") ?? false) return;
                output.Attributes.SetAttribute("class", classAttr.Value + " active");
            }
        }
    }
}
