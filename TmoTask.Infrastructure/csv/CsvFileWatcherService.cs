using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using TmoTask.Application;
using Microsoft.Extensions.Options;

namespace TmoTask.Infrastructure.csv
{
    /// <summary>
    /// CsvFile Watcher background service
    /// </summary>
    public class CsvFileWatcherService : BackgroundService
    {
        #region Private Fields
        private readonly IDataCacheService _cacheService;
        private readonly ILogger<CsvFileWatcherService> _logger;
        private FileSystemWatcher _watcher;
        private readonly AppSettingOptions _options;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cacheService"></param>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public CsvFileWatcherService(IDataCacheService cacheService, IOptions<AppSettingOptions> options,
            ILogger<CsvFileWatcherService> logger)
        {
            this._options = options.Value;
            _cacheService = cacheService;
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// Set up the watcher
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var csvFilePath = Path.Combine(AppContext.BaseDirectory, this._options.CsvFilePath);

            var directory = Path.GetDirectoryName(csvFilePath);
            var fileName = Path.GetFileName(csvFilePath);

            _watcher = new FileSystemWatcher(directory!, fileName);
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size | NotifyFilters.FileName;

            _watcher.Changed += OnFileChanged;
            _watcher.Renamed += OnFileChanged;
            _watcher.EnableRaisingEvents = true;

            _logger.LogInformation($"Watching file: {csvFilePath}");

            return Task.CompletedTask;
        }

        /// <summary>
        /// Invalidate Cache on change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            _logger.LogInformation($"CSV file changed: {e.ChangeType}");
            _cacheService.InvalidateCaches();
        }


        /// <inheritdoc />
        public override void Dispose()
        {
            base.Dispose();
            _watcher.Dispose();
        }
    }
}
