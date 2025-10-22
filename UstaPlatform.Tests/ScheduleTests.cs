using UstaPlatform.Domain;
using UstaPlatform.Domain.Entities;
using Xunit;

namespace UstaPlatform.Tests;

public class ScheduleTests
{
    [Fact]
    public void ScheduleIndexer_WithNoWorkOrders_ReturnsEmptyList()
    {
        // Arrange
        var schedule = new Schedule();
        var date = new DateOnly(2024, 10, 22);

        // Act
        var workOrders = schedule[date];

        // Assert
        Assert.Empty(workOrders);
    }

    [Fact]
    public void ScheduleIndexer_WithSingleWorkOrder_ReturnsCorrectList()
    {
        // Arrange
        var schedule = new Schedule();
        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = new DateTime(2024, 10, 22, 10, 0, 0)
        };

        // Act
        schedule.AddWorkOrder(workOrder);
        var workOrders = schedule[new DateOnly(2024, 10, 22)];

        // Assert
        Assert.Single(workOrders);
        Assert.Equal(workOrder, workOrders[0]);
    }

    [Fact]
    public void ScheduleIndexer_WithMultipleWorkOrders_ReturnsAllOrders()
    {
        // Arrange
        var schedule = new Schedule();
        var date = new DateTime(2024, 10, 22);

        var workOrder1 = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = date
        };

        var workOrder2 = new WorkOrder
        {
            Id = 2,
            RequestId = 2,
            MasterId = 1,
            BasePrice = 150m,
            ScheduledDate = date
        };

        // Act
        schedule.AddWorkOrder(workOrder1);
        schedule.AddWorkOrder(workOrder2);
        var workOrders = schedule[DateOnly.FromDateTime(date)];

        // Assert
        Assert.Equal(2, workOrders.Count);
        Assert.Contains(workOrder1, workOrders);
        Assert.Contains(workOrder2, workOrders);
    }

    [Fact]
    public void ScheduleIndexer_WithDifferentDates_ReturnsSeparateLists()
    {
        // Arrange
        var schedule = new Schedule();

        var workOrder1 = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = new DateTime(2024, 10, 22)
        };

        var workOrder2 = new WorkOrder
        {
            Id = 2,
            RequestId = 2,
            MasterId = 1,
            BasePrice = 150m,
            ScheduledDate = new DateTime(2024, 10, 23)
        };

        // Act
        schedule.AddWorkOrder(workOrder1);
        schedule.AddWorkOrder(workOrder2);

        var workOrders22 = schedule[new DateOnly(2024, 10, 22)];
        var workOrders23 = schedule[new DateOnly(2024, 10, 23)];

        // Assert
        Assert.Single(workOrders22);
        Assert.Single(workOrders23);
        Assert.Equal(workOrder1, workOrders22[0]);
        Assert.Equal(workOrder2, workOrders23[0]);
    }

    [Fact]
    public void GetWorkOrdersForDate_ReturnsCorrectEnumerable()
    {
        // Arrange
        var schedule = new Schedule();
        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = new DateTime(2024, 10, 22)
        };

        // Act
        schedule.AddWorkOrder(workOrder);
        var workOrders = schedule.GetWorkOrdersForDate(new DateOnly(2024, 10, 22));

        // Assert
        Assert.Single(workOrders);
    }

    [Fact]
    public void ScheduleIndexer_WithNonExistingDate_ReturnsEmptyList()
    {
        // Arrange
        var schedule = new Schedule();
        var existingDate = new DateOnly(2024, 10, 22);
        var nonExistingDate = new DateOnly(2024, 10, 23);

        var workOrder = new WorkOrder
        {
            Id = 1,
            RequestId = 1,
            MasterId = 1,
            BasePrice = 100m,
            ScheduledDate = existingDate.ToDateTime(TimeOnly.MinValue)
        };

        // Act
        schedule.AddWorkOrder(workOrder);
        var existingWorkOrders = schedule[existingDate];
        var nonExistingWorkOrders = schedule[nonExistingDate];

        // Assert
        Assert.Single(existingWorkOrders);
        Assert.Empty(nonExistingWorkOrders);
    }
}