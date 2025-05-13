namespace TmoTask.Application
{
    /// <summary>
    /// A result containing a list of items
    /// </summary>
    /// <typeparam name="T">The type of item contained in the results list</typeparam>
    public class ItemsResult<T> : OperationResultBase where T : class
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of <see cref="ItemsResult{T}"/>
        /// </summary>
        public ItemsResult() { }

        /// <summary>
        /// Initializes a new instance of <see cref="ItemsResult{T}"/>
        /// </summary>
        /// <param name="items">the items the result is for</param>
        public ItemsResult(IEnumerable<T> items)
        {
            this.Items = items;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the items that were requested
        /// </summary>
        public IEnumerable<T> Items { get; set; }
        #endregion
    }
}
