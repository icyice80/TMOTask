using Microsoft.Extensions.Logging;

namespace TmoTask.Application.Performance
{
    /// <summary>
    /// Default implementation of <see cref="IPerformanceService"/>
    /// </summary>
    public class PerformanceService : IPerformanceService
    {
        #region Private Fields
        private readonly IDataCacheService _dataCacheService;
        private readonly ILogger<PerformanceService> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataCacheService"></param>
        /// <param name="logger"></param>
        public PerformanceService(IDataCacheService dataCacheService, ILogger<PerformanceService> logger)
        {
            this._dataCacheService = dataCacheService;
            this._logger = logger;
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public async Task<ItemsResult<PerformanceMetricsDto>> GetPerformanceMetricsByBranchAsync(string branchName)
        {
            // Log method entry
            _logger.LogInformation($"Starting GetTopSellersByMonthAsync for branch: {branchName}");

            var result = new ItemsResult<PerformanceMetricsDto>();

            try
            {
                // Check if branch exists
                var branches = await this._dataCacheService.GetAllBranchesAsync();
                if (!branches.Contains(branchName))
                {
                    _logger.LogWarning($"Branch {branchName} not found. Returning invalid branch result.");
                    result.Error = new OperationError(ErrorCode.InvalidBranch, "Invalid Branch Name", $"{branchName} does not exist");
                    return result;
                }

                _logger.LogInformation($"Branch {branchName} found. Fetching orders...");

                // Get orders
                var orders = await this._dataCacheService.GetAllOrdersAsync();

                // Process orders
                var topPerformantByMonth = orders
                    .Where(o => o.Branch == branchName)
                    .GroupBy(o => new { o.Seller, o.OrderDate.Month })
                    .Select(g => new PerformanceMetricsDto(
                        g.Key.Seller,
                        g.Key.Month,
                        g.Count(),
                        g.Sum(o => o.Price)
                    ))
                    .OrderByDescending(s => s.TotalPrice);

                _logger.LogInformation($"Processed {topPerformantByMonth.Count()} top performant for branch {branchName}.");

                // Return the result
                return new ItemsResult<PerformanceMetricsDto>(topPerformantByMonth);
            }
            catch(Exception ex)
            {
                // Log the error and handle it gracefully at the service level
                _logger.LogError(ex, $"An error occurred while processing top sellers for branch: {branchName}");
                result.Error = new OperationError(ErrorCode.InternalError, "An unexpected error occurred", "Please try again later");
                return result;
            }
        }
        #endregion
    }
}
