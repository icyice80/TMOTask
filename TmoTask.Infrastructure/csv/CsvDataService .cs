using System.Collections.Concurrent;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TmoTask.Domain;

namespace TmoTask.Infrastructure.csv
{
    /// <summary>
    /// Default implementation of <see cref="IDataService"/>
    /// </summary>
    public class CsvDataService : IDataService
    {
        #region Private Fields
        private readonly AppSettingOptions _options;
        private readonly ILogger<CsvDataService> _logger;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public CsvDataService(IOptions<AppSettingOptions> options, ILogger<CsvDataService> logger)
        {
            this._options = options.Value;
            this._logger = logger;
        }
        #endregion

        #region Public Methods

        /// <inheritdoc />
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            var csvFilePath = Path.Combine(AppContext.BaseDirectory, this._options.CsvFilePath);

            // Initialize the orders list
            var orders = new ConcurrentBag<Order>(); 

            // Check if the file exists
            if (!File.Exists(csvFilePath))
            {
                _logger.LogError($"CSV file not found at path: {csvFilePath}");
                return orders; // Return an empty list if the file does not exist
            }

            try
            {
                using var reader = new StreamReader(csvFilePath);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true // as it has header.
                });

                var tasks = new List<Task>();
                var semaphore = new SemaphoreSlim(this._options.MaxDegreeOfParallelismForCsv);

                // Process records in parallel to improve the speed when the file gets too big
                await foreach (var order in csv.GetRecordsAsync<Order>())
                {
                    await semaphore.WaitAsync(); // Limit parallelism

                    var task = ProcessOrderAsync(order, semaphore, orders);
                    tasks.Add(task);
                }

                // Wait for all tasks to complete
                await Task.WhenAll(tasks);


                // Check if no records were found after processing
                if (orders.Count == 0)
                {
                    _logger.LogWarning("No orders found in the CSV file.");
                }

                return orders;
            }
            catch (CsvHelperException ex) // Catch any CSV-specific exceptions
            {
                _logger.LogError(ex, "Error occurred while reading the CSV file.");
                return Enumerable.Empty<Order>(); ; // Return the empty list in case of error
            }
            catch (Exception ex) // Catch general exceptions
            {
                _logger.LogError(ex, "Unexpected error occurred while processing the CSV file.");
                return Enumerable.Empty<Order>(); ; // Return the empty list in case of error
            }
        }

        #endregion

        #region Private Methods



        /// <summary>
        /// Process Order
        /// Note: Using async Task keeps things consistent
        /// </summary>
        /// <param name="order"></param>
        /// <param name="semaphore"></param>
        /// <param name="orders"></param>
        /// <returns></returns>
        private async Task ProcessOrderAsync(Order order, SemaphoreSlim semaphore, ConcurrentBag<Order> orders)
        {
            try
            {
                orders.Add(order);
            }
            finally
            {
                // Release the semaphore after the task is complete
                semaphore.Release(); 
            }

        }
        #endregion 
    }
}
