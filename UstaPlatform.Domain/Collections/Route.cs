using System.Collections;

namespace UstaPlatform.Domain.Collections;

public class Route : IEnumerable<(int X, int Y)>
{
    private readonly List<(int X, int Y)> _coordinates = new();

    public void Add(int x, int y)
    {
        _coordinates.Add((x, y));
    }

    public IEnumerator<(int X, int Y)> GetEnumerator()
    {
        return _coordinates.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}