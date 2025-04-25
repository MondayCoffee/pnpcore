namespace PnP.Core.Model.SharePoint
{
    /// <summary>
    /// Enumeration of the different frequencies for which to receive alerts
    /// </summary>
    public enum AlertFrequency
    {
        /// <summary>
        /// The alert is sent immediately
        /// </summary>
        Immediately = 0,

        /// <summary>
        /// The alert is sent daily
        /// </summary>
        Daily = 1,

        /// <summary>
        /// The alert is sent weekly
        /// </summary>
        Weekly = 2
    }
}
