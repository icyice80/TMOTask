namespace TmoTask.Application.Branch
{
    /// <summary>
    /// Service for branch related operations.
    /// </summary>
    public interface IBranchService
    {
        /// <summary>
        /// Get all branches
        /// </summary>
        /// <returns></returns>
        Task<ItemsResult<string>> GetBranchesAsync();
    }
}
