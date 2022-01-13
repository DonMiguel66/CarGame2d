using System.Collections.Generic;

namespace CarGame2D
{
    public interface IItemsRepository
    {
        IReadOnlyDictionary<int,IItem> Items { get; }
    }
}
