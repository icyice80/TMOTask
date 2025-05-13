using Microsoft.Extensions.Caching.Memory;
using TmoTask.Domain;
using TmoTask.Application;
using Microsoft.Extensions.Logging;

namespace TmoTask.Infrastructure.cache
{
    /// <summary>
    /// Default implementation of <see cref="IDataCacheService"/>
    /// </summary>
    public class DataCacheService : IDataCacheService
    {
        #region Private Fields
        private readonly IMemoryCache _cache;
        private readonly IDataService _csvDataService;
        private readonly ILogger<DataCacheService> _logger;
        private const string _cacheKeyForOrders = "orderes";
        private const string _cacheKeyForBranches = "branches";
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="csvDataService"></param>
        /// <param name="logger"></param>
        public DataCacheService(IMemoryCache cache, IDataService csvDataService, ILogger<DataCacheService> logger)
        {
            this._cache = cache;
            this._csvDataService = csvDataService;
            this._logger = logger;
        }
        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            _logger.LogInformation("Fetching orders from cache or CSV data source.");

            // Try to fetch orders from cache
            var orders = await _cache.GetOrCreateAsync(_cacheKeyForOrders, async entry =>
            {
                _logger.LogInformation("Cache miss for orders. Fetching from CSV.");

                // Fetch orders from CSV and cache it
                var fetchedOrders = await _csvDataService.GetOrdersAsync();
                _logger.LogInformation($"Fetched {fetchedOrders.Count()} orders from CSV.");

                // Return the fetched orders
                return fetchedOrders;
            });

            _logger.LogInformation($"Returning {orders.Count()} orders.");
            return orders;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> GetAllBranchesAsync()
        {
            _logger.LogInformation("Fetching branches from cache or calculating from orders.");

            // Try to fetch branches from cache
            var branches = await _cache.GetOrCreateAsync(_cacheKeyForBranches, async entry =>
            {
                _logger.LogInformation("Cache miss for branches. Calculating from orders.");

                // Fetch all orders and extract distinct branches
                var orders = await GetAllOrdersAsync();
                var distinctBranches = orders.Select(o => o.Branch).Distinct().OrderBy(o=>o).ToList();

                _logger.LogInformation($"Calculated {distinctBranches.Count} distinct branches.");

                return distinctBranches;
            });

            _logger.LogInformation($"Returning {branches.Count()} branches.");
            return branches;
        }

        /// <inheritdoc />
        public void InvalidateCaches()
        {
            _logger.LogInformation("Invalidating cache for orders and branches.");

            // Remove cache for both orders and branches
            _cache.Remove(_cacheKeyForBranches);
            _cache.Remove(_cacheKeyForOrders);

            _logger.LogInformation("Cache invalidated successfully.");
        }
        #endregion
    }
}
