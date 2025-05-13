namespace TmoTask.Application.Performance
{
    /// <summary>
    /// Service for performance operations.
    /// </summary>
    public interface IPerformanceService
    {
        /// <summary>
        /// Get performance metrics based on the selected branch name
        /// </summary>
        /// <param name="branchName"></param>
        /// <returns></returns>
        Task<ItemsResult<PerformanceMetricsDto>> GetPerformanceMetricsByBranchAsync(string branchName);
    }
}
