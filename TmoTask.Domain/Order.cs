namespace TmoTask.Domain
{
    /// <summary>
    /// Poco
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets / Sets Seller
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        /// Gets / Sets Product
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Gets / Sets Price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets / Sets OrderDate
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets / Sets Branch
        /// </summary>
        public string Branch { get; set; }

    }
}
