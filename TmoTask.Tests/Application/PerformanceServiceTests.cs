using Microsoft.Extensions.Logging;
using Moq;
using TmoTask.Domain;
using TmoTask.Application.Performance;
using TmoTask.Application;

namespace TmoTask.Tests.Application
{
    /// <summary>
    /// Unit tests for <see cref="PerformanceService"/>
    /// </summary>
    [TestFixture]
    public class PerformanceServiceTests
    {
        private Mock<IDataCacheService> _dataCacheMock;
        private Mock<ILogger<PerformanceService>> _loggerMock;
        private IPerformanceService _performanceService;

        [SetUp]
        public void Setup()
        {
            _dataCacheMock = new Mock<IDataCacheService>();
            _loggerMock = new Mock<ILogger<PerformanceService>>();
            _performanceService = new PerformanceService(_dataCacheMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetTopSellersByMonthAsync_ValidBranch_ReturnsTopSellers()
        {
            // Arrange
            string branch = "Branch 1";
            var branches = new List<string> { "Branch 1", "Branch 2" };
            var orders = new List<Order>
            {
                new Order() {Branch = "Branch 1", OrderDate = new DateTime(2024, 1, 10), Seller = "Seller A", Price = 100},
                new Order() {Branch = "Branch 1", OrderDate = new DateTime(2024, 1, 10), Seller = "Seller A", Price = 50},
                new Order() {Branch = "Branch 1", OrderDate = new DateTime(2024, 3, 10), Seller = "Seller B", Price = 60},
                new Order() {Branch = "Branch 2", OrderDate = new DateTime(2024, 4, 10), Seller = "Seller B", Price = 30}
            };

            _dataCacheMock.Setup(m => m.GetAllBranchesAsync()).ReturnsAsync(branches);
            _dataCacheMock.Setup(m => m.GetAllOrdersAsync()).ReturnsAsync(orders);

            // Act
            var result = await _performanceService.GetPerformanceMetricsByBranchAsync(branch);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.AreEqual(2, result.Items.Count());

            var sellerA = result.Items.FirstOrDefault(x => x.SellerName == "Seller A");
            Assert.NotNull(sellerA);
            Assert.AreEqual(2, sellerA.OrderCount);
            Assert.AreEqual(150, sellerA.TotalPrice);
        }

        [Test]
        public async Task GetTopSellersByMonthAsync_InvalidBranch_ReturnsError()
        {
            // Arrange
            string branch = "Branch 3";
            var branches = new List<string> { "Branch 1", "Branch 2" };

            _dataCacheMock.Setup(m => m.GetAllBranchesAsync()).ReturnsAsync(branches);

            // Act
            var result = await _performanceService.GetPerformanceMetricsByBranchAsync(branch);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ErrorCode.InvalidBranch, result.Error.ErrorCode);
        }

        [Test]
        public async Task GetTopSellersByMonthAsync_ExceptionThrown_ReturnsInternalError()
        {
            // Arrange
            string branch = "Branch 1";
            _dataCacheMock.Setup(m => m.GetAllBranchesAsync()).ThrowsAsync(new Exception("Error on Processing"));

            // Act
            var result = await _performanceService.GetPerformanceMetricsByBranchAsync(branch);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ErrorCode.InternalError, result.Error.ErrorCode);
        }
    }
}
