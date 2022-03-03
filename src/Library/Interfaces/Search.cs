using System.Collections.Generic;

namespace PathFinder.Interfaces
{
    public interface Search<T>
    {
        IEnumerable<T> GetNode();
    }
}