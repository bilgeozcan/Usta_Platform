namespace UstaPlatform.Domain.Entities;

public class Request
{
    public int Id { get; init; }
    public int CitizenId { get; init; }
    public string Description { get; init; }
    public string ServiceType { get; init; }
    public string Address { get; init; }
    public DateTime RequestDate { get; init; }
    public bool IsUrgent { get; init; }

    public Request() { }
}