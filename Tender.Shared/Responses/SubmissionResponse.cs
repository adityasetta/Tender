namespace Tender.Shared.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// The submission response
    /// </summary>
    public class SubmissionResponse
    {
        /// <summary>
        /// The failed
        /// </summary>
        private const string Failed = "failed";

        /// <summary>
        /// The success
        /// </summary>
        private const string Success = "success";

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFitToWorkApprovalResponse"/> class.
        /// </summary>
        /// <param name="isSuccess">if set to <c>true</c> [is success].</param>
        /// <param name="errorMessage">The error message.</param>
        public SubmissionResponse(bool isSuccess, string errorMessage) : this()
        {
            SubmissionStatus = isSuccess ? Success : Failed;
            Reason = isSuccess ? string.Empty : errorMessage;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SaveFitToWorkApprovalResponse"/> class from being created.
        /// </summary>
        [JsonConstructor]
        private SubmissionResponse()
        {
        }

        /// <summary>
        /// Gets the submission status.
        /// </summary>
        /// <value>
        /// The submission status.
        /// </value>
        [JsonProperty("submission_status")]
        public string SubmissionStatus { get; set; }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        /// <value>
        /// The reason.
        /// </value>
        public string Reason { get; set; } = string.Empty;

        /// <summary>
        /// Gets a value indicating whether this instance is submission success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is submission success; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool IsSubmissionSuccess => SubmissionStatus != Failed;

        /// <summary>
        /// For the success.
        /// </summary>
        /// <returns>The submission response</returns>
        public static SubmissionResponse ForSuccess()
        {
            return new SubmissionResponse(true, string.Empty);
        }

        /// <summary>
        /// For the failure.
        /// </summary>
        /// <param name="error">The error.</param>
        /// <returns>The submission response</returns>
        public static SubmissionResponse ForFailure(string error)
        {
            return new SubmissionResponse(false, error);
        }
    }
}
