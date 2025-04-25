using PnP.Core.QueryModel;
using PnP.Core.Services;

namespace PnP.Core.Model.SharePoint
{
    internal sealed class AlertCollection : QueryableDataModelCollection<IAlert>, IAlertCollection
    {
        public AlertCollection(PnPContext context, IDataModelParent parent, string memberName) : base(context, parent, memberName)
        {
            PnPContext = context;
            Parent = parent;
        }
    }
}
