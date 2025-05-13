namespace TmoTask.Application
{
    /// <summary>
    /// The result of calling an operation
    /// </summary>
    public interface IOperationResult
    {
        /// <summary>
        /// Gets whether the operation was successful or not
        /// </summary>
        /// <returns>true if the operation succeeded</returns>
        bool Succeeded();

        /// <summary>
        /// Gets any error that prevented the request from succeeding
        /// </summary>
        public OperationError? Error { get; }

    }
}
