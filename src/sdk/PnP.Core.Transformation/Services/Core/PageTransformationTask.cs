﻿using System;
using PnP.Core.Services;

namespace PnP.Core.Transformation.Services.Core
{
    /// <summary>
    /// Defines a page transformation task
    /// </summary>
    public class PageTransformationTask
    {
        /// <summary>
        /// Creates an instance of the task with a new id
        /// </summary>
        public PageTransformationTask(ISourceProvider sourceProvider, ISourceItemId sourceItemId, PnPContext targetContext) : this(Guid.NewGuid(), sourceProvider, sourceItemId, targetContext)
        {
        }

        /// <summary>
        /// Creates an instance of the task
        /// </summary>
        public PageTransformationTask(Guid id, ISourceProvider sourceProvider, ISourceItemId sourceItemId, PnPContext targetContext)
        {
            Id = id;
            SourceProvider = sourceProvider ?? throw new ArgumentNullException(nameof(sourceProvider));
            SourceItemId = sourceItemId ?? throw new ArgumentNullException(nameof(sourceItemId));
            TargetContext = targetContext ?? throw new ArgumentNullException(nameof(targetContext));
        }

        /// <summary>
        /// The source provider to use for the task
        /// </summary>
        public ISourceProvider SourceProvider { get; }

        /// <summary>
        /// The source item id to process
        /// </summary>
        public ISourceItemId SourceItemId { get; }

        /// <summary>
        /// The target PnP context
        /// </summary>
        public PnPContext TargetContext { get; }

        /// <summary>
        /// The unique ID of the transformation task
        /// </summary>
        public Guid Id { get; }
    }
}