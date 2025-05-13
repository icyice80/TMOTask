namespace TmoTask.Application
{
    /// <summary>
    /// ErrorCode:
    /// System errors below: it ranges between 1 - 100
    /// Business logic errors below: make sure the error code is above 100
    /// </summary>
    public static class ErrorCode
    {
        /// <summary>
        /// System Error: InternalError
        /// </summary>
        public const int InternalError = 1;

        /// <summary>
        /// Business Logic Error: Invalid Branch
        /// </summary>

        public const int InvalidBranch = 101;
    }
}
