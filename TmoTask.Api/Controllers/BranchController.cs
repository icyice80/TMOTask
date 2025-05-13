using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TmoTask.Application;
using TmoTask.Application.Branch;

namespace TmoTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : SiteApiController
    {
        #region Private Fields
        private readonly IBranchService _branchService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="branchService"></param>
        public BranchController(IBranchService branchService)
        {
            this._branchService = branchService;
        }
        #endregion

        /// <summary>
        /// Get all the branches
        /// </summary>
        /// <returns></returns>
        [HttpGet("all"),
         ProducesResponseType(typeof(ItemsResult<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var branches = await _branchService.GetBranchesAsync();
            return this.AsActionResult(branches);
        }

    }
}
