using Microsoft.Extensions.Logging;

namespace TmoTask.Application.Branch
{
    /// <summary>
    /// Default implementation of <see cref="IBranchService"/>
    /// </summary>
    public class BranchService : IBranchService
    {
        #region Private Fields
        private readonly IDataCacheService _dataCacheService;
        private readonly ILogger<BranchService> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataCacheService"></param>
        /// <param name="logger"></param>
        public BranchService(IDataCacheService dataCacheService, ILogger<BranchService> logger)
        {
            this._dataCacheService = dataCacheService;
            this._logger = logger;
        }
        #endregion

        #region Public Methods
        /// <inheritdoc />
        public async Task<ItemsResult<string>> GetBranchesAsync()
        {
            var result = new ItemsResult<string>();

            try
            {
                _logger.LogInformation("Fetching all branches...");

                var branches = await _dataCacheService.GetAllBranchesAsync();

                if (branches != null && !branches.Any())
                {
                    _logger.LogInformation("No branches found.");
                    result.Items = new List<string>();
                    return result;
                }

                _logger.LogInformation($"Retrieved {branches.Count()} branches.");
                return new ItemsResult<string>(branches);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving branches.");
                result.Error = new OperationError(ErrorCode.InternalError, "An unexpected error occurred",
                    "Please try again later");
                return result;
            }

        }
        #endregion
    }
}
