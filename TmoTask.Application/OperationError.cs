namespace TmoTask.Application
{

    /// <summary>
    /// Identifies an error that prevented an operation from succeeding.
    /// </summary>
    /// <param name="ErrorCode">The code that identifies the error that occurred.</param>
    /// <param name="Title">A title for the error, which can act as a short description.</param>
    /// <param name="Message">A message that describes the error.</param>
    public record OperationError(int ErrorCode, string Title, string Message);
}
