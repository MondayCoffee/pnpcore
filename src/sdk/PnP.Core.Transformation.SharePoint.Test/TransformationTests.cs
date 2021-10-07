﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SharePoint.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PnP.Core.Services;
using PnP.Core.Transformation.Services.Core;
using PnP.Core.Transformation.Test.Utilities;

namespace PnP.Core.Transformation.SharePoint.Test
{
    [TestClass]
    public class TransformationTests
    {

        [TestMethod]
        public async Task SharePointTransformAsync()
        {
            var config = TestCommon.GetConfigurationSettings();

            var services = new ServiceCollection();
            services.AddTestPnPCore();

            // You can use the default settings
            // services.AddPnPSharePointTransformation();

            // Or you can provide a set of custom settings
            services.AddPnPSharePointTransformation(
                pnpOptions => // Global settings
                {
                    pnpOptions.DisableTelemetry = false;
                    pnpOptions.PersistenceProviderConnectionString = config["PersistenceProviderConnectionString"];
                },
                pageOptions => // Target modern page creation settings
                {
                    pageOptions.CopyPageMetadata = true;
                    pageOptions.KeepPageCreationModificationInformation = true;
                    pageOptions.PostAsNews = false;
                    pageOptions.PublishPage = false;
                    pageOptions.DisablePageComments = false;
                    pageOptions.KeepPageSpecificPermissions = true;
                    pageOptions.Overwrite = true;
                    pageOptions.ReplaceHomePageWithDefaultHomePage = true;
                    pageOptions.SetAuthorInPageHeader = true;
                    pageOptions.TargetPageFolder = "";
                    pageOptions.TargetPageName = "";
                    pageOptions.TargetPagePrefix = "Migrated_";
                    pageOptions.TargetPageTakesSourcePageName = true;
                },
                spOptions => // SharePoint classic source settings
                {
                    // spOptions.WebPartMappingFile = config["WebPartMappingFile"];
                    // spOptions.PageLayoutMappingFile = config["PageLayoutMappingFile"];
                    spOptions.RemoveEmptySectionsAndColumns = true;
                    spOptions.ShouldMapUsers = true;
                    spOptions.HandleWikiImagesAndVideos = true;
                    spOptions.AddTableListImageAsImageWebPart = true;
                    spOptions.IncludeTitleBarWebPart = true;
                    spOptions.MappingProperties = null;
                    spOptions.SkipHiddenWebParts = true;
                    spOptions.SkipUrlRewrite = true;
                    spOptions.UrlMappings = null;
                    spOptions.UserMappings = null;
                }
            );

            var provider = services.BuildServiceProvider();

            var pnpContextFactory = provider.GetRequiredService<IPnPContextFactory>();
            var pageTransformator = provider.GetRequiredService<IPageTransformator>();

            var sourceContext = provider.GetRequiredService<ClientContext>();
            var targetContext = await pnpContextFactory.CreateAsync(TestCommon.TargetTestSite);
            var sourceUri = new Uri(config["SourceUri"]);

            var result = await pageTransformator.TransformSharePointAsync(sourceContext, targetContext, sourceUri);

            Assert.IsNotNull(result);
            var expectedUri = new Uri($"{targetContext.Web.Url}/SitePages/Migrated_{sourceUri.Segments[sourceUri.Segments.Length - 1]}");
            Assert.AreEqual(expectedUri, result);
        }

        [TestMethod]
        public async Task InMemoryExecutorSharePointTransformAsync()
        {
            var services = new ServiceCollection();
            services.AddTestPnPCore();
            services.AddPnPSharePointTransformation();

            var provider = services.BuildServiceProvider();

            var transformationExecutor = provider.GetRequiredService<ITransformationExecutor>();
            var pnpContextFactory = provider.GetRequiredService<IPnPContextFactory>();

            var sourceContext = provider.GetRequiredService<ClientContext>();

            var result = await transformationExecutor.TransformSharePointAsync(
                pnpContextFactory,
                sourceContext,
                TestCommon.TargetTestSite);

            Assert.IsNotNull(result);
            Assert.AreEqual(TransformationExecutionState.Completed, result.State);
        }

    }
}
