using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing;
using UstaPlatform.Pricing.Rules;
using Xunit;

namespace UstaPlatform.Tests;

public class PricingEngineTests
{
    [Fact]
    public void CalculateFinalPrice_WithUrgentRequest_AppliesUrgentPricing()
    {
        // Arrange
        var engine = new PricingEngine();
        engine.AddRule(new UrgentPricingRule());

        var request = new Request
        {
            Id = 1,
            CitizenId = 1,
            Description = "Test",
            ServiceType = "Test",
            Address = "Test",
            RequestDate = DateTime.Now,
            IsUrgent = true
        };

        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = DateTime.Now.AddDays(1)
        };

        // Act
        var result = engine.CalculateFinalPrice(100m, workOrder, request);

        // Assert
        Assert.Equal(150m, result);
    }

    [Fact]
    public void CalculateFinalPrice_WithWeekendDate_AppliesWeekendPricing()
    {
        // Arrange
        var engine = new PricingEngine();
        engine.AddRule(new WeekendPricingRule());

        var request = new Request
        {
            Id = 1,
            CitizenId = 1,
            Description = "Test",
            ServiceType = "Test",
            Address = "Test",
            RequestDate = DateTime.Now,
            IsUrgent = false
        };

        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = new DateTime(2024, 10, 26) // Saturday
        };

        // Act
        var result = engine.CalculateFinalPrice(100m, workOrder, request);

        // Assert
        Assert.Equal(120m, result);
    }

    [Fact]
    public void CalculateFinalPrice_WithMultipleRules_AppliesAllRules()
    {
        // Arrange
        var engine = new PricingEngine();
        engine.AddRule(new UrgentPricingRule());
        engine.AddRule(new WeekendPricingRule());

        var request = new Request
        {
            Id = 1,
            CitizenId = 1,
            Description = "Test",
            ServiceType = "Test",
            Address = "Test",
            RequestDate = DateTime.Now,
            IsUrgent = true
        };

        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = new DateTime(2024, 10, 26) // Saturday
        };

        // Act
        var result = engine.CalculateFinalPrice(100m, workOrder, request);

        // Assert
        Assert.Equal(180m, result);
    }

    [Fact]
    public void CalculateFinalPrice_WithNoRules_ReturnsBasePrice()
    {
        // Arrange
        var engine = new PricingEngine();
        // No rules added

        var request = new Request
        {
            Id = 1,
            CitizenId = 1,
            Description = "Test",
            ServiceType = "Test",
            Address = "Test",
            RequestDate = DateTime.Now,
            IsUrgent = false
        };

        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = DateTime.Now.AddDays(1)
        };

        // Act
        var result = engine.CalculateFinalPrice(100m, workOrder, request);

        // Assert
        Assert.Equal(100m, result);
    }

    [Fact]
    public void CalculateFinalPrice_WithNormalWeekday_ReturnsBasePrice()
    {
        // Arrange
        var engine = new PricingEngine();
        engine.AddRule(new WeekendPricingRule());

        var request = new Request
        {
            Id = 1,
            CitizenId = 1,
            Description = "Test",
            ServiceType = "Test",
            Address = "Test",
            RequestDate = DateTime.Now,
            IsUrgent = false
        };

        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = new DateTime(2024, 10, 24) // Thursday (weekday)
        };

        // Act
        var result = engine.CalculateFinalPrice(100m, workOrder, request);

        // Assert
        Assert.Equal(100m, result);
    }
}