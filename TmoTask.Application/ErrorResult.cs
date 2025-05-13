namespace TmoTask.Application
{
    /// <summary>
    /// Operation Result that did not succeed
    /// </summary>
    public class ErrorResult : IOperationResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ErrorResult"/>
        /// </summary>
        /// <param name="error">error that caused the operation not to succeed</param>
        public ErrorResult(OperationError error)
        {
            this.Error = error;
        }

        /// <inheritdoc />
        public bool Succeeded()
        {
            return false;
        }

        /// <inheritdoc />
        public OperationError Error { get; set; }

    }
}
