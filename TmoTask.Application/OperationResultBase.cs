using System.Text.Json.Serialization;

namespace TmoTask.Application
{
    /// <summary>
    /// Base operation result 
    /// </summary>
    public class OperationResultBase : IOperationResult
    {

        /// <summary>
        /// Gets any errors that occurred which prevented an operation from succeeding
        /// </summary>
        [JsonIgnore]
        public OperationError? Error { get; set; }

        /// <inheritdoc />
        public virtual bool Succeeded()
        {
            return Error == null;
        }
    }
}
