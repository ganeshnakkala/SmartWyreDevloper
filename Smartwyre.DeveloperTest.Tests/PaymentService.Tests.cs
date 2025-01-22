using Moq;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IncentiveCalculators;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateServiceTests
    {
        private readonly Mock<IRebateDataStore> _rebateDataStoreMock;
        private readonly Mock<IProductDataStore> _productDataStoreMock;        
        private readonly RebateService _rebateService;

        public RebateServiceTests()
        {
            _rebateDataStoreMock = new Mock<IRebateDataStore>();
            _productDataStoreMock = new Mock<IProductDataStore>();          

            // We need to instantiate RebateService with mocked dependencies
            _rebateService = new RebateService(_rebateDataStoreMock.Object, _productDataStoreMock.Object);
        }

        [Fact]
        public void CalculateRebate_FixedRateRebate_IncentiveType_ShouldReturnSuccess()
        {
            // Arrange
            var rebate = new Rebate
            {
                Identifier = "rebate1",
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 100m,
                Percentage = 0.1m
            };

            var product = new Product
            {
                Identifier = "product1",
                Price = 200m,
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate1",
                ProductIdentifier = "product1",
                Volume = 10
            };

            // Setup mock behavior for the data store
            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);           

            // Act
            var result = _rebateService.CalculateRebate(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(200m, result.RebateAmount);       
        }

        [Fact]
        public void CalculateRebate_AmountPerUom_IncentiveType_Rebate_ShouldReturnSuccess()
        {
            // Arrange
            var rebate = new Rebate
            {
                Identifier = "rebate2",
                Incentive = IncentiveType.AmountPerUom,
                Amount = 100m,
                Percentage = 0.1m
            };

            var product = new Product
            {
                Identifier = "product2",
                Price = 200m,
                SupportedIncentives = SupportedIncentiveType.AmountPerUom
            };

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate2",
                ProductIdentifier = "product2",
                Volume = 10
            };

            // Setup mock behavior for the data store
            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);

            // Act
            var result = _rebateService.CalculateRebate(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(1000m, result.RebateAmount);
        }

        [Fact]
        public void CalculateRebate_FixedCash_IncentiveType_Rebate_ShouldReturnSuccess()
        {
            // Arrange
            var rebate = new Rebate
            {
                Identifier = "rebate3",
                Incentive = IncentiveType.FixedCashAmount,
                Amount = 100m,
                Percentage = 0.1m
            };

            var product = new Product
            {
                Identifier = "product3",
                Price = 200m,
                SupportedIncentives = SupportedIncentiveType.FixedCashAmount
            };

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate3",
                ProductIdentifier = "product3",
                Volume = 10
            };

            // Setup mock behavior for the data store
            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);

            // Act
            var result = _rebateService.CalculateRebate(request);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(rebate.Amount, result.RebateAmount);
        }

        [Fact]
        public void CalculateRebate_RebateNotFound_ShouldReturnFailure()
        {
            // Arrange
            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate1",
                ProductIdentifier = "product1",
                Volume = 10
            };

            // Setup mock behavior for the data store to return null for rebate
            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns((Rebate)null);

            // Act
            var result = _rebateService.CalculateRebate(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(0m, result.RebateAmount);
        }

        [Fact]
        public void CalculateRebate_ProductNotFound_ShouldReturnFailure()
        {
            // Arrange
            var rebate = new Rebate
            {
                Identifier = "rebate1",
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 100m,
                Percentage = 0.1m
            };

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate1",
                ProductIdentifier = "product1",
                Volume = 10
            };

            // Setup mock behavior for the data store to return the rebate, but no product
            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns((Product)null);

            // Act
            var result = _rebateService.CalculateRebate(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(0m, result.RebateAmount);
        }

        [Fact]
        public void CalculateRebate_EligibilityCheckFails_ShouldReturnFailure()
        {
            // Arrange
            var rebate = new Rebate
            {
                Identifier = "rebate1",
                Incentive = IncentiveType.FixedRateRebate,
                Amount = 100m,
                Percentage = 0.1m
            };

            var product = new Product
            {
                Identifier = "product1",
                Price = 200m,
                SupportedIncentives = SupportedIncentiveType.FixedRateRebate
            };

            var request = new CalculateRebateRequest
            {
                RebateIdentifier = "rebate1",
                ProductIdentifier = "product1",
                Volume = 0
            };

            // Setup mock behavior for the data store
            _rebateDataStoreMock.Setup(r => r.GetRebate(It.IsAny<string>())).Returns(rebate);
            _productDataStoreMock.Setup(p => p.GetProduct(It.IsAny<string>())).Returns(product);           

            // Act
            var result = _rebateService.CalculateRebate(request);

            // Assert
            Assert.False(result.Success);
            Assert.Equal(0m, result.RebateAmount);
        }
    }
}
