#pragma checksum "C:\Users\Jackie\Source\Repos\Donut_Deliverable2\Donut_Deliverable1\Views\Attendance\Verification.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b84823980a5d9a057c5fd3e789bb52376ae2f12"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Attendance_Verification), @"mvc.1.0.view", @"/Views/Attendance/Verification.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Jackie\Source\Repos\Donut_Deliverable2\Donut_Deliverable1\Views\_ViewImports.cshtml"
using Donut_Deliverable1;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Jackie\Source\Repos\Donut_Deliverable2\Donut_Deliverable1\Views\_ViewImports.cshtml"
using Donut_Deliverable1.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b84823980a5d9a057c5fd3e789bb52376ae2f12", @"/Views/Attendance/Verification.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fbbe1e595e5c7a0b0ffeb95d502ed338be12df86", @"/Views/_ViewImports.cshtml")]
    public class Views_Attendance_Verification : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Donut_Deliverable1.Controllers.AttendanceController>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Jackie\Source\Repos\Donut_Deliverable2\Donut_Deliverable1\Views\Attendance\Verification.cshtml"
  
    ViewData["Title"] = "Verification";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Verification</h1>

<div class=""text-center"">

    <h3>Is this you?</h3>
    <h5>John Lowery</h5>
    <img src=""/Images/boy1.jpg"" alt=""Green check mark"" width=""250"" height=""250"" />
    <input type=""submit"" value=""Yes"" class=""btn btn-lg btn-success""");
            BeginWriteAttribute("onclick", " onclick=\"", 368, "\"", 430, 3);
            WriteAttributeValue("", 378, "location.href=\'", 378, 15, true);
#nullable restore
#line 13 "C:\Users\Jackie\Source\Repos\Donut_Deliverable2\Donut_Deliverable1\Views\Attendance\Verification.cshtml"
WriteAttributeValue("", 393, Url.Action("Success", "Attendance"), 393, 36, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 429, "\'", 429, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n    <input type=\"submit\" value=\"No\" class=\"btn btn-lg btn-danger\"");
            BeginWriteAttribute("onclick", " onclick=\"", 501, "\"", 563, 3);
            WriteAttributeValue("", 511, "location.href=\'", 511, 15, true);
#nullable restore
#line 14 "C:\Users\Jackie\Source\Repos\Donut_Deliverable2\Donut_Deliverable1\Views\Attendance\Verification.cshtml"
WriteAttributeValue("", 526, Url.Action("CheckIn", "Attendance"), 526, 36, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 562, "\'", 562, 1, true);
            EndWriteAttribute();
            WriteLiteral(" />\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Donut_Deliverable1.Controllers.AttendanceController> Html { get; private set; }
    }
}
#pragma warning restore 1591
