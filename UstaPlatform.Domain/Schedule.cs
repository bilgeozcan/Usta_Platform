using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Domain;

public class Schedule
{
    private readonly Dictionary<DateOnly, List<WorkOrder>> _dailySchedule = new();

    // Dizinleyici (Indexer) - Bu çok önemli!
    public List<WorkOrder> this[DateOnly date]
    {
        get
        {
            if (!_dailySchedule.ContainsKey(date))
                _dailySchedule[date] = new List<WorkOrder>();
            return _dailySchedule[date];
        }
    }

    public void AddWorkOrder(WorkOrder workOrder)
    {
        var date = DateOnly.FromDateTime(workOrder.ScheduledDate);
        this[date].Add(workOrder);
    }

    public IEnumerable<WorkOrder> GetWorkOrdersForDate(DateOnly date)
    {
        return this[date];
    }
}