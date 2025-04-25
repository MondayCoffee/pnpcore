using PnP.Core.Model.Security;
using System;

namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// Alert class, write your custom code here
    /// </summary>
    [SharePointType("SP.Alert", Target = typeof(Web), Uri = "_api/Web/Alerts(guid'{Id}')", Get = "_api/Web/Alerts", LinqGet = "_api/Web/Alerts")]
    internal sealed class Alert : BaseDataModel<IAlert>, IAlert
    {
        #region Construction
        public Alert()
        {
        }
        #endregion

        #region Properties

        public AlertFrequency AlertFrequency { get => GetValue<AlertFrequency>(); set => SetValue(value); }

        public string AlertTemplateName { get => GetValue<string>(); set => SetValue(value); }

        public DateTime AlertTime { get => GetValue<DateTime>(); set => SetValue(value); }

        public AlertType AlertType { get => GetValue<AlertType>(); set => SetValue(value); }

        public bool AlwaysNotify { get => GetValue<bool>(); set => SetValue(value); }

        public AlertDeliveryMethod DeliveryChannels { get => GetValue<AlertDeliveryMethod>(); set => SetValue(value); }

        public AlertEventType EventType { get => GetValue<AlertEventType>(); set => SetValue(value); }

        public string Filter { get => GetValue<string>(); set => SetValue(value); }

        public Guid Id { get => GetValue<Guid>(); set => SetValue(value); }

        public int ItemID { get => GetValue<int>(); set => SetValue(value); }

        public Guid ListId { get => GetValue<Guid>(); set => SetValue(value); }

        public string ListUrl { get => GetValue<string>(); set => SetValue(value); }

        public AlertStatus Status { get => GetValue<AlertStatus>(); set => SetValue(value); }

        public string Title { get => GetValue<string>(); set => SetValue(value); }

        public int UserId { get => GetValue<int>(); set => SetValue(value); }

        public IPropertyValues AllProperties { get => GetModelValue<IPropertyValues>(); }

        public IListItem Item { get => GetModelValue<IListItem>(); }

        public IList List { get => GetModelValue<IList>(); }

        public ISharePointUser User { get => GetModelValue<ISharePointUser>(); }

        [KeyProperty(nameof(Id))]
        public override object Key { get => Id; set => Id = Guid.Parse(value.ToString()); }

        [SharePointProperty("*")]
        public object All { get => null; }

        #endregion

    }
}
