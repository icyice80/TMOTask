using Microsoft.AspNetCore.Mvc;
using TmoTask.Application;

namespace TmoTask.Api.Controllers
{
    /// <summary>
    ///  Base api controller for this site
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class SiteApiController : ControllerBase
    {

        /// <summary>
        /// Return the operation result as an <see cref="IActionResult"/>
        /// </summary>
        /// <typeparam name="T">the type of <see cref="IOperationResult"/></typeparam>
        /// <param name="result">The operation result to translate into a http action result.</param>
        /// <param name="resultAsContent">Indicates if the result should be returned as the content of the action result, otherwise
        /// no content will be returned with the http action result. The default is true.</param>
        /// <returns><paramref name="result "/>as a http action result</returns>
        protected IActionResult AsActionResult<T>(T result, bool resultAsContent = true) where T : IOperationResult
        {

            if (result.Succeeded() == false)
            {
                // if the errorcode is less than 100, than it's 500 errors
                if (result.Error?.ErrorCode < 100)
                {
                    return this.StatusCode(500, result.Error.Message);
                }

                // else it's business rule violation
                return this.Conflict(new ErrorResult(result.Error));
            }

            if (resultAsContent)
            {
                return this.Ok(result);
            }

            return this.Ok();

        }
    }
}
