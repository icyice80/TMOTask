namespace TmoTask.Application.Performance
{
    /// <summary>
    /// Dto for performance metrics
    /// </summary>
    /// <param name="SellerName"></param>
    /// <param name="Month"></param>
    /// <param name="OrderCount"></param>
    /// <param name="TotalPrice"></param>
    public record PerformanceMetricsDto(
        string SellerName,
        int Month,
        int OrderCount,
        decimal TotalPrice
    );
}
