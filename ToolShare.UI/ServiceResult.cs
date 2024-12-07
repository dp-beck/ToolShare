namespace ToolShare.UI
{
    /// <summary>
    /// General Object for Result of Call to a Service
    /// </summary>
    public class ServiceResult
    {
         /// <summary>
        /// Gets or sets a value indicating whether the action was successful.
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// On failure, the problem details are parsed and returned in this array.
        /// </summary>
        public string[] ErrorList { get; set; } = [];
    }
}