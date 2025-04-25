namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// Enumeration of the different types of events for which to receive alerts
    /// </summary>
    public enum AlertEventType
    {
        /// <summary>
        /// The alert is for all changes
        /// </summary>
        AllChanges = -1,

        /// <summary>
        /// The alert is for all additions to a list
        /// </summary>
        Add = 1,

        /// <summary>
        /// The alert is for all changes to a list
        /// </summary>
        Modified = 2,

        /// <summary>
        /// The alert is for all deletions from a list
        /// </summary>
        Deleted = 4,

        /// <summary>
        /// The alert is for all discussions
        /// </summary>
        Discussion = 0x00000FF0

    }
}
