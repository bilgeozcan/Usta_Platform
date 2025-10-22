namespace UstaPlatform.Domain.Entities;

public class WorkOrder
{
    public int Id { get; init; }
    public int RequestId { get; init; }
    public int MasterId { get; init; }
    public decimal BasePrice { get; set; }
    public decimal FinalPrice { get; set; }
    public DateTime ScheduledDate { get; init; }
    public string Status { get; set; } = "Planlandı";

    public WorkOrder() { }
}