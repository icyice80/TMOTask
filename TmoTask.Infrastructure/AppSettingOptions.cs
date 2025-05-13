namespace TmoTask.Infrastructure
{
 
    /// <summary>
    /// Options for AppSettings.
    /// </summary>
    public class AppSettingOptions
    {
        /// <summary>
        /// Options Name
        /// </summary>
        public static string Name = "AppSettings";

        /// <summary>
        /// Gets / Sets CsvFilePath
        /// </summary>
        public required string CsvFilePath { get; set; }

        /// <summary>
        /// Gets / Sets MaxDegreeOfParallelismForCsv
        /// </summary>
        public int MaxDegreeOfParallelismForCsv { get; set; } = 2;
    }
}
