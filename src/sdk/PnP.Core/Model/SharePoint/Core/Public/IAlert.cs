using PnP.Core.Model.Security;
using System;

namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// SharePoint Alert definition
    /// </summary>
    [ConcreteType(typeof(Alert))]
    public interface IAlert : IDataModel<IAlert>, IDataModelGet<IAlert>, IDataModelLoad<IAlert>
    {

        /// <summary>
        /// Frequency of the alert
        /// </summary>
        public AlertFrequency AlertFrequency { get; set; }

        /// <summary>
        /// Template name of the alert
        /// </summary>
        public string AlertTemplateName { get; set; }

        /// <summary>
        /// Time to send out the alert (for alerts with frequency set to <see cref="AlertFrequency.Weekly"/>)
        /// </summary>
        public DateTime AlertTime { get; set; }

        /// <summary>
        /// Type of the alert
        /// </summary>
        public AlertType AlertType { get; set; }

        /// <summary>
        /// Indicates if the alert should always be sent out
        /// </summary>
        public bool AlwaysNotify { get; set; }

        /// <summary>
        /// Delivery channels for the alert
        /// </summary>
        public AlertDeliveryMethod DeliveryChannels { get; set; }

        /// <summary>
        /// Event type of the alert
        /// </summary>
        public AlertEventType EventType { get; set; }

        /// <summary>
        /// CAML filter applied for the alert
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Unique identifier of the alert 
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ID of the item the alert is related to
        /// </summary>
        public int ItemID { get; set; }

        /// <summary>
        /// ID of the list the alert is related to
        /// </summary>
        public Guid ListId { get; set; }

        /// <summary>
        /// Server relativer URL of the list the alert is related to
        /// </summary>
        public string ListUrl { get; set; }

        /// <summary>
        /// Status of the alert
        /// </summary>
        public AlertStatus Status { get; set; }

        /// <summary>
        /// Title of the alert
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Id of the user that will receive the alert
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Collection of alert properties
        /// </summary>
        public IPropertyValues AllProperties { get; }

        /// <summary>
        /// List item the alert is related to
        /// </summary>
        public IListItem Item { get; }

        /// <summary>
        /// List the alert is related to
        /// </summary>
        public IList List { get; }

        /// <summary>
        /// User that will receive the alert
        /// </summary>
        public ISharePointUser User { get; }

        /// <summary>
        /// A special property used to add an asterisk to a $select statement
        /// </summary>
        public object All { get; }
    }
}
