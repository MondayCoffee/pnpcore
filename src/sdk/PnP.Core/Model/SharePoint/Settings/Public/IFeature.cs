﻿using System;

namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// Represents a Feature in SharePoint Online
    /// </summary>
    [ConcreteType(typeof(Feature))]
    public interface IFeature : IDataModel<IFeature>, IDataModelGet<IFeature>
    {
        /// <summary>
        /// The ID of the Feature
        /// </summary>
        public Guid DefinitionId { get; }

        /// <summary>
        /// The name of the feature
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// A special property used to add an asterisk to a $select statement
        /// </summary>
        public object All { get; }
    }
}
