using TmoTask.Domain;

namespace TmoTask.Application
{
    /// <summary>
    /// Service for Cached Data 
    /// </summary>
    public interface IDataCacheService
    {
        /// <summary>
        /// Get All Branches
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<string>> GetAllBranchesAsync();

        /// <summary>
        /// Get Orders by Branch
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        /// <summary>
        /// Invalidate all caches
        /// </summary>
        void InvalidateCaches();
    }
}
