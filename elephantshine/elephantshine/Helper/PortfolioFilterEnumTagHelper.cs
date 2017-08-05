using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using elephantshine.Models.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace elephantshine.Helper
{
    public class PortfolioFilterEnumTagHelper : TagHelper
    {
        public List<EnumCategory> EnableCategorys { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var list = GetEnumSelectListItem();

            output.Content.AppendHtml("<li><a href='#' class='current' data-filter='*'>全部</a></li>");
            
            foreach (var item in list)
            {
                output.Content.AppendHtml("<li>")
                    .AppendHtml("<span>&frasl;</span>")
                    .AppendHtml($"<a href='#' data-filter='.{item.Value}'>{item.Text}</a>")
                    .AppendHtml("</li>");
            }
        }

        public List<SelectListItem> GetEnumSelectListItem()
        {
            var list = new List<SelectListItem>();
            var typeInfo = typeof(EnumCategory).GetTypeInfo();
            var enumValues = typeInfo.GetEnumValues();

            foreach (var value in enumValues)
            {
                if (!EnableCategorys.Contains((EnumCategory)value))
                {
                    continue;
                }
                
                var memberInfo = typeInfo.GetMember(value.ToString()).First();
                var descriptionAttribute = memberInfo.GetCustomAttribute<DisplayNameAttribute>();

                list.Add(new SelectListItem
                {
                    Text = descriptionAttribute.DisplayName,
                    Value = value.ToString()
                });
            }

            return list;
        }
    }
}
