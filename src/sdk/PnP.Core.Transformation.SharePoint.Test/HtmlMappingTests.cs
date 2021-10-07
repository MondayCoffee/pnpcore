﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PnP.Core.Services;
using PnP.Core.Transformation.Services.Core;
using PnP.Core.Transformation.Services.MappingProviders;
using PnP.Core.Transformation.SharePoint.MappingProviders;
using PnP.Core.Transformation.Test.Utilities;

namespace PnP.Core.Transformation.SharePoint.Test
{
    [TestClass]
    public class HtmlMappingTests
    {
        [TestMethod]
        public async Task MapHtmlAsync()
        {
            var services = new ServiceCollection();
            services.AddTestPnPCore();
            services.AddPnPSharePointTransformation();

            var provider = services.BuildServiceProvider();

            var pnpContextFactory = provider.GetRequiredService<IPnPContextFactory>();
            var mappingProvider = provider.GetRequiredService<IHtmlMappingProvider>();

            // Prepare contexts
            var sourceContext = provider.GetRequiredService<ClientContext>();
            var targetContext = await pnpContextFactory.CreateAsync(TestCommon.TargetTestSite);

            var sourceUri = new Uri("http://site/item");
            var targetPageUri = new Uri("http://site/item");

            SharePointSourceItem sourceItem = new SharePointSourceItem(sourceUri, sourceContext);

            // Prepare task
            var task = new PageTransformationTask(new SharePointSourceProvider(sourceContext), sourceItem.Id, targetContext);
            var context = new PageTransformationContext(task, sourceItem, targetPageUri);
            var htmlContent = "<p><span class=\"ms-rteStyle-Normal\">Norm</span>​al<br></p><h1>Hea​​ding1<br></h1><h2>hea​​​ding2<br></h2><h3>Hea​​ding3<br></h3><h4>​heading4​​​​<br></h4><p>Quote<br></p><p>Text in <strong>bold</strong>, in <em>italic</em>, in <span style=\"text-decoration&#58;underline;\">underline</span>, in <span class=\"ms-rteThemeForeColor-5-0\" style=\"\">red</span> with <span class=\"ms-rteBackColor-4\">yellow</span> highlight<br></p><p>with <span style=\"text-decoration&#58;line-through;\">striked</span>, with <sup>superscript</sup> with <sub>lowerscript</sub> and with a different <span class=\"ms-rteFontSize-5\">size</span><br></p><p>left centered<br></p><p style=\"text-align&#58;center;\">Middle centered</p><p style=\"text-align&#58;right;\">Right centered<br></p><p style=\"text-align&#58;justify;\">spread<br></p><blockquote style=\"margin&#58;0px 0px 0px 40px;border&#58;none;padding&#58;0px;\"><p>Indent1</p></blockquote><blockquote style=\"margin&#58;0px 0px 0px 40px;border&#58;none;padding&#58;0px;\"><blockquote style=\"margin&#58;0px 0px 0px 40px;border&#58;none;padding&#58;0px;\"><p>Indent2</p></blockquote></blockquote><ul><li>Bullet 1<br></li><li>Bullet2<br></li><ul><li>Bullet 2.1</li></ul></ul><ol><li>Numbered1<br></li><li>Numbered2<br></li></ol><p>with a link to <a href=\"https&#58;//www.microsoft.com/\">microsoft.com​</a></p><p>table centered</p><p></p><table cellspacing=\"0\" class=\"ms-rteTable-1 \" style=\"width&#58;100%;\"><tbody><tr class=\"ms-rteTableHeaderRow-1\"><th class=\"ms-rteTableHeaderEvenCol-1\" rowspan=\"1\" colspan=\"1\" style=\"width&#58;33.3333%;\">​​H1<br></th><th class=\"ms-rteTableHeaderOddCol-1\" rowspan=\"1\" colspan=\"1\" style=\"width&#58;33.3333%;\">​H2<br></th><th class=\"ms-rteTableHeaderEvenCol-1\" rowspan=\"1\" colspan=\"1\" style=\"width&#58;33.3333%;\">​H3<br></th></tr><tr class=\"ms-rteTableOddRow-1\"><td class=\"ms-rteTableEvenCol-1\">​v1<br></td><td class=\"ms-rteTableOddCol-1\">​v2<br></td><td class=\"ms-rteTableEvenCol-1\">​v3<br></td></tr><tr class=\"ms-rteTableEvenRow-1\"><td class=\"ms-rteTableEvenCol-1\">​v12<br></td><td class=\"ms-rteTableOddCol-1\">​v22<br></td><td class=\"ms-rteTableEvenCol-1\">​v32<br></td></tr><tr class=\"ms-rteTableOddRow-1\"><td class=\"ms-rteTableEvenCol-1\">​v13<br></td><td class=\"ms-rteTableOddCol-1\">​​v23<br></td><td class=\"ms-rteTableEvenCol-1\">​v33<br></td></tr></tbody></table><p><br><br></p><p><br></p>";
            
            // Map html
            var input = new HtmlMappingProviderInput(context, htmlContent);
            var result = await mappingProvider.MapHtmlAsync(input);

            Assert.AreEqual(
                "<span><p>Normal<br></p></span><h2>Heading1<br></h2><h3>heading2<br></h3><h4>Heading3<br></h4><div>heading4<br></div><span><p>Quote<br></p></span><span><p>Text in <span><strong>bold</strong></span>, in <span><em>italic</em></span>, in <u>underline</u>, in red with <span class=\"highlightColorYellow\">yellow</span> highlight<br></p></span><span><p>with <s>striked</s>, with <span><sup>superscript</sup></span> with <span><sub>lowerscript</sub></span> and with a different <span class=\"fontSizeXxLarge\">size</span><br></p></span><span><p>left centered<br></p></span><span><p style=\"text-align: center\">Middle centered</p></span><span><p style=\"text-align: right\">Right centered<br></p></span><span><p style=\"text-align: justify\">spread<br></p></span><span><span><p style=\"margin-left: 40px\">Indent1</p></span></span><span><span><p style=\"margin-left: 80px\">Indent2</p></span></span><ul><li>Bullet 1<br></li><li>Bullet2<br></li><ul><li>Bullet 2.1</li></ul></ul><ol><li>Numbered1<br></li><li>Numbered2<br></li></ol><span><p>with a link to <a href=\"https://www.microsoft.com/\">microsoft.com</a></p></span><span><p>table centered</p></span><span><p></p></span><div class=\"canvasRteResponsiveTable\"><div class=\"tableLeftAlign tableWrapper\"><table class=\"bandedRowTableStyleNeutral\" title=\"Table\"><tbody><tr><td style=\"width: 266px\"><strong>H1</strong></td><td style=\"width: 266px\"><strong>H2</strong></td><td style=\"width: 268px\"><strong>H3</strong></td></tr><tr><td style=\"width: 266px\">v1<br></td><td style=\"width: 266px\">v2<br></td><td style=\"width: 268px\">v3<br></td></tr><tr><td style=\"width: 266px\">v12<br></td><td style=\"width: 266px\">v22<br></td><td style=\"width: 268px\">v32<br></td></tr><tr><td style=\"width: 266px\">v13<br></td><td style=\"width: 266px\">v23<br></td><td style=\"width: 268px\">v33<br></td></tr></tbody></table></div></div><span><p><br><br></p></span><span><p><br></p></span>",
                result.HtmlContent);
        }

    }
}
