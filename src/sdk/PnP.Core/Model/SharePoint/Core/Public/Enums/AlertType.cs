namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// Enumeration of the different types of alerts
    /// </summary>
    public enum AlertType
    {
        /// <summary>
        /// The alert is for a list
        /// </summary>
        List = 0,

        /// <summary>
        /// The alert is for a list item
        /// </summary>
        ListItem = 1,

        /// <summary>
        /// The alert is a custom alert
        /// </summary>
        Custom = 2
    }
}
