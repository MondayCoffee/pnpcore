﻿using PnP.Core.Transformation.Services.MappingProviders;
using PnP.Core.Transformation.SharePoint.MappingFiles.Publishing;
using System;
using System.Collections.Generic;
using System.Text;

namespace PnP.Core.Transformation.SharePoint.MappingProviders
{
    /// <summary>
    /// Custom version of the PageLayoutMappingProviderOutput to manage a SharePoint specific output
    /// </summary>
    internal class SharePointPageLayoutMappingProviderOutput : PageLayoutMappingProviderOutput
    {
        public PageLayout PageLayout { get; set; }
    }
}
