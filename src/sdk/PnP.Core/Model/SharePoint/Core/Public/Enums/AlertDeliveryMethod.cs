namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// Enumeration of the different delivery methods for alerts
    /// </summary>
    public enum AlertDeliveryMethod
    {
        /// <summary>
        /// The alert is delivered by email
        /// </summary>
        Email = 1,

        /// <summary>
        /// The alert is delivered by SMS
        /// </summary>
        SMS = 2,

        /// <summary>
        /// The alert is delivered by Windows Notification Service (WNS)
        /// </summary>
        WNS = 3,

        /// <summary>
        /// The alert is delivered by the Push Notification service
        /// </summary>
        PushNotify = 4,

        /// <summary>
        /// The alert is delivered by content
        /// </summary>
        Content = 5,
    }
}
