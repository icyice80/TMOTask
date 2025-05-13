using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TmoTask.Application;
using TmoTask.Application.Performance;

namespace TmoTask.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceReportController : SiteApiController
    {
        #region Private Fields
        private readonly IPerformanceService _performanceService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="performanceService"></param>
        public PerformanceReportController(IPerformanceService performanceService)
        {
            this._performanceService = performanceService;
        }
        #endregion

        /// <summary>
        /// Get top performance by branch
        /// </summary>
        /// <param name="branchName"></param>
        /// <returns></returns>
        [HttpGet("top-performance"),
         ProducesResponseType(typeof(ItemsResult<PerformanceMetricsDto>), StatusCodes.Status200OK),
         ProducesResponseType(typeof(ErrorResult), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> GetPerformanceMetrics([FromQuery,Required] string branchName)
        {
            var result = await this._performanceService.GetPerformanceMetricsByBranchAsync(branchName);
            return this.AsActionResult(result);
        }
    }
}
