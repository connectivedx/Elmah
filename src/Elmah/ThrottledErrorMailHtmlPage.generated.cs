﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Elmah
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 2 "..\..\ThrottledErrorMailHtmlPage.cshtml"
    using System.Web;
    
    #line default
    #line hidden
    
    #line 3 "..\..\ThrottledErrorMailHtmlPage.cshtml"
    using Elmah;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    internal partial class ThrottledErrorMailHtmlPage : RazorTemplateBase
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");





            
            #line 5 "..\..\ThrottledErrorMailHtmlPage.cshtml"
  
    // NB cast is not really required, but aids with intellisense!
    var error = (Error) this.Error;

    var about = PoweredBy.GetAbout(HttpRuntime.Cache, (version, fileVersion, product, copyright) => new
    {
        Version = version != null
                ? version.ToString()
                : fileVersion != null
                ? fileVersion.ToString()
                : "?.?.?.?",
        Product = Mask.EmptyString(product, "(product)"),
        Copyright = copyright,
    });


            
            #line default
            #line hidden
WriteLiteral("<html>\r\n    <head>\r\n        <title>Error: ");


            
            #line 22 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                 Write(error.Message);

            
            #line default
            #line hidden
WriteLiteral(@"</title>
        <style type=""text/css"">
            body { font-family: verdana, arial, helvetic; font-size: x-small; }
            td, th, pre { font-size: x-small; } 
        </style>
    </head>
    <body>
		<h1 style=""font-size: medium; font-style: italic; color: maroon;"">");


            
            #line 29 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                               Write(error.Type);

            
            #line default
            #line hidden
WriteLiteral(": ");


            
            #line 29 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                                            Write(error.Message);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n\r\n");


            
            #line 31 "..\..\ThrottledErrorMailHtmlPage.cshtml"
  		
			var rawUrl = error.ServerVariables.Get("RAW_URL");
			if(string.IsNullOrEmpty(rawUrl))
			{
				rawUrl = "no-url";
			}

			var host = error.ServerVariables.Get("HTTP_HOST");
			if (string.IsNullOrEmpty(host))
			{
				host = "no-hostname";
			}

			var protocol = error.ServerVariables.Get("HTTPS");
			if (string.IsNullOrEmpty(protocol) || protocol == "off")
			{
				protocol = "http://";
			}
			else
			{
				protocol = "https://";
			}
		

            
            #line default
            #line hidden
WriteLiteral("\t\t\r\n\t\t<ul style=\"padding: 0; margin: 0; list-style-type: none\">\r\n\t\t\t<li style=\"ma" +
"rgin:0\"><a href=\"");


            
            #line 56 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                            Write(protocol);

            
            #line default
            #line hidden

            
            #line 56 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                     Write(host);

            
            #line default
            #line hidden

            
            #line 56 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                          Write(rawUrl);

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 56 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                   Write(protocol);

            
            #line default
            #line hidden

            
            #line 56 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                            Write(host);

            
            #line default
            #line hidden

            
            #line 56 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                                 Write(rawUrl);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n");


            
            #line 57 "..\..\ThrottledErrorMailHtmlPage.cshtml"
 			if (error.Time != DateTime.MinValue)
			{

            
            #line default
            #line hidden
WriteLiteral("\t\t\t\t<li style=\"margin:0\"><strong>");


            
            #line 59 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                            Write(error.Time.ToLocalTime().ToString("h:mm:ss tt"));

            
            #line default
            #line hidden
WriteLiteral("</strong> ");


            
            #line 59 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                                                      Write(error.Time.ToLocalTime().ToString("M/d/yyyy"));

            
            #line default
            #line hidden
WriteLiteral(" (server time)</li>\r\n");


            
            #line 60 "..\..\ThrottledErrorMailHtmlPage.cshtml"
			}

            
            #line default
            #line hidden
WriteLiteral("\t\t\t<li style=\"margin:0\"><strong>");


            
            #line 61 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                           Write(ErrorCount);

            
            #line default
            #line hidden
WriteLiteral("</strong> occurrences of this error recently</li>\r\n\t\t</ul>\r\n\t\t<br />\r\n\t\t\r\n");


            
            #line 65 "..\..\ThrottledErrorMailHtmlPage.cshtml"
         if (error.Detail.Length != 0)
        {

            
            #line default
            #line hidden


WriteLiteral("<pre style=\"padding: 1em; background-color: #FFFFCC; font-family: Consolas, Couri" +
"er, monospace;\">");


            
            #line 70 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                                                                          Write(error.Detail);

            
            #line default
            #line hidden
WriteLiteral("</pre>\r\n");


            
            #line 71 "..\..\ThrottledErrorMailHtmlPage.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("\t\t\r\n");


            
            #line 73 "..\..\ThrottledErrorMailHtmlPage.cshtml"
  		
			var ignoredVarPrefixes = new List<string>();
			ignoredVarPrefixes.Add("CERT_");
			ignoredVarPrefixes.Add("ALL_");
			ignoredVarPrefixes.Add("APPL_");
			ignoredVarPrefixes.Add("GATEWAY_INTERFACE");
			ignoredVarPrefixes.Add("HTTPS_");
			ignoredVarPrefixes.Add("INSTANCE_");
			ignoredVarPrefixes.Add("SERVER_SOFTWARE");
		

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 84 "..\..\ThrottledErrorMailHtmlPage.cshtml"
 		foreach (var collection in 
            from collection in new[] 
            {
                new
                {
                    Id    = "ServerVariables",
                    Title = "Server Variables",
                    Items = error.ServerVariables,
                }
            }
            let data = collection.Items
            where data != null && data.Count > 0
            let items = from i in Enumerable.Range(0, data.Count)
                        select KeyValuePair.Create(data.GetKey(i), data[i])
            select new
            {
                collection.Id, 
                collection.Title,
                Items = items.OrderBy(e => e.Key, StringComparer.OrdinalIgnoreCase)
            }
        )
        {

            
            #line default
            #line hidden
WriteLiteral("            <div id=\"");


            
            #line 106 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                Write(collection.Id);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n                <h2 style=\"font-size: medium; font-style: italic; color: gray" +
";\">");


            
            #line 107 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                                           Write(collection.Title);

            
            #line default
            #line hidden
WriteLiteral(@"</h2>
                <table style=""border-collapse: collapse; border-spacing: 0; border: 1px solid black; width: 100%"">
                    <tr><th style=""text-align: left; border: 1px solid black; padding: 4px;"">Name</th>            
                        <th style=""text-align: left; border: 1px solid black; padding: 4px;"">Value</th></tr>
");


            
            #line 111 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                     foreach (var item in collection.Items.Where(x=>!ignoredVarPrefixes.Any(y=>x.Key.StartsWith(y))))
                    {

            
            #line default
            #line hidden
WriteLiteral("\t                    <tr style=\"vertical-align: top;\">\r\n\t\t\t\t\t\t\t<td style=\"border:" +
" 1px solid black; padding: 4px;\">");


            
            #line 114 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                     Write(item.Key);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n\t\t\t\t\t\t\t<td style=\"border: 1px solid black; padding: 4px; font-family: Cons" +
"olas, Courier, monospace;\">");


            
            #line 115 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                                                                                                Write(item.Value);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n\t\t\t\t\t\t</tr>\r\n");


            
            #line 117 "..\..\ThrottledErrorMailHtmlPage.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </table>\r\n            </div>\r\n");


            
            #line 120 "..\..\ThrottledErrorMailHtmlPage.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </body>\r\n</html>\r\n");


        }
    }
}
#pragma warning restore 1591
