using Microsoft.Extensions.Logging;
using Moq;
using TmoTask.Application.Branch;
using TmoTask.Application;

namespace TmoTask.Tests.Application
{
    /// <summary>
    /// Unit tests for <see cref="BranchService"/>
    /// </summary>
    [TestFixture]
    public class BranchServiceTests
    {
        private Mock<IDataCacheService> _dataCacheServiceMock;
        private Mock<ILogger<BranchService>> _loggerMock;
        private BranchService _branchService;

        [SetUp]
        public void SetUp()
        {
            _dataCacheServiceMock = new Mock<IDataCacheService>();
            _loggerMock = new Mock<ILogger<BranchService>>();
            _branchService = new BranchService(_dataCacheServiceMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetBranchesAsync_ReturnsBranches_WhenDataExists()
        {
            // Arrange
            var branches = new List<string> { "Branch 1", "Branch 2" };
            _dataCacheServiceMock.Setup(s => s.GetAllBranchesAsync())
                .ReturnsAsync(branches);

            // Act
            var result = await _branchService.GetBranchesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
            Assert.AreEqual(branches.Count, result.Items.Count());
            CollectionAssert.AreEqual(branches, result.Items);
        }

        [Test]
        public async Task GetBranchesAsync_ReturnsEmpty_WhenNoDataFound()
        {
            // Arrange
            var branches = new List<string>(); // empty
            _dataCacheServiceMock.Setup(s => s.GetAllBranchesAsync())
                .ReturnsAsync(branches);

            // Act
            var result = await _branchService.GetBranchesAsync();

            // Assert
            Assert.AreEqual(0,result.Items.Count());
        }

        [Test]
        public async Task GetBranchesAsync_ReturnsError_WhenExceptionThrown()
        {
            // Arrange
            _dataCacheServiceMock.Setup(s => s.GetAllBranchesAsync())
                .ThrowsAsync(new Exception("Something went wrong"));

            // Act
            var result = await _branchService.GetBranchesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(ErrorCode.InternalError, result.Error.ErrorCode);
        }
    }
}
