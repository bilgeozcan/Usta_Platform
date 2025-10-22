namespace UstaPlatform.Domain.Entities;

public class Citizen
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Address { get; init; }

    public Citizen() { }
}