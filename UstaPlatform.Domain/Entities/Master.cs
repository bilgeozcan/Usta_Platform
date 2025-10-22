namespace UstaPlatform.Domain.Entities;

public class Master
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Expertise { get; init; }
    public double Rating { get; set; }
    public int Workload { get; set; }
    public string Location { get; init; }

    public Master() { }
}