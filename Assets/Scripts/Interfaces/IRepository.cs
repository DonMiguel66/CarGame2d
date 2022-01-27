using System.Collections.Generic;

namespace CarGame2D
{
    public interface IRepository<TKey, TValue>
    {
        IReadOnlyDictionary<TKey, TValue> Collection { get; }
    }
}
