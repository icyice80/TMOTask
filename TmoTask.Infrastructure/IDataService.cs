using TmoTask.Domain;

namespace TmoTask.Infrastructure
{
    /// <summary>
    /// Service for getting data from external sources.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Get Orders
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetOrdersAsync();
    }
}
