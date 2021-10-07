using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PnP.Core.Services;
using PnP.Core.Transformation.Poc.Implementations;
using PnP.Core.Transformation.Services.Core;
using PnP.Core.Transformation.SharePoint;
using Microsoft.SharePoint.Client;

namespace PnP.Core.Transformation.Poc
{
    /// <summary>
    /// Function to magage the transformation of a whole site
    /// </summary>
    public class TransformSiteFunction
    {
        private readonly IPnPContextFactory pnpContextFactory;
        private readonly ITransformationExecutor transformationExecutor;
        private readonly ITransformationStateManager transformationStateManager;
        private readonly ClientContext sourceContext;

        public TransformSiteFunction(
            IPnPContextFactory pnpContextFactory, 
            ITransformationExecutor transformationExecutor, 
            ITransformationStateManager transformationStateManager,
            ClientContext sourceContext)
        {
            this.pnpContextFactory = pnpContextFactory;
            this.transformationExecutor = transformationExecutor;
            this.transformationStateManager = transformationStateManager;
            this.sourceContext = sourceContext;
        }

        [FunctionName("TransformSite")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            CancellationToken token)
        {
            ITransformationProcess process;
            string id;
            Guid processId;
            switch (req.Method)
            {
                // Trigger the transformation of a whole site
                case "POST":
                    string target = "TargetSite";

                    // Create the process
                    process = await transformationExecutor.CreateTransformationProcessAsync(token);

                    // Start to enqueue items
                    await process.StartProcessAsync(
                        pnpContextFactory,
                        sourceContext,
                        target, 
                        token);

                    return new OkObjectResult(new { process.Id });

                // Get the status of a running transformation
                case "GET":
                    id = req.Query["id"];
                    if (!Guid.TryParse(id, out processId)) return new BadRequestResult();

                    process = await transformationExecutor.LoadTransformationProcessAsync(processId, token);
                    var status = await process.GetStatusAsync(token);

                    return new OkObjectResult(status);

                // Cancel a running transformation
                case "DELETE":
                    id = req.Query["id"];
                    if (!Guid.TryParse(id, out processId)) return new BadRequestResult();

                    process = await transformationExecutor.LoadTransformationProcessAsync(processId, token);
                    await process.StopProcessAsync(token);

                    return new OkResult();
            }

            return new BadRequestResult();
        }
    }
}
