using PnP.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// Public interface to define a collection of Alert objects
    /// </summary>
    [ConcreteType(typeof(AlertCollection))]
    public interface IAlertCollection : IQueryable<IAlert>, IAsyncEnumerable<IAlert>, IDataModelCollection<IAlert>, IDataModelCollectionLoad<IAlert>, IDataModelCollectionDeleteByGuidId, ISupportModules<IAlert>
    {
    }
}